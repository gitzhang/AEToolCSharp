using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AEToolLib;
using System.Collections;
using System.Threading;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;

namespace GDBProcessWin
{
    public partial class HKDataProcessForm : Form
    {
        public delegate void ReportProcessInfo(string info, int percent);//实现Background的ProcessChange事件
        public delegate void DoneFeatureProcess(string info);//实现Background的RunWorkerCompleted事件
        public delegate void FeatureProcessDetail(string info); //实现要素处理的细节信息

        public delegate void StartRoadCheck();//开始进行接边检查

        ReportProcessInfo reportProcessInfo;
        DoneFeatureProcess doneFeatureProcess;
        FeatureProcessDetail featureProcessDetail;

        StartRoadCheck startRoadCheck;

        /// <summary>
        /// gdb文件路径
        /// </summary>
        private String gdbFilePath;

        /// <summary>
        /// GDB数据库工具
        /// </summary>
        private GDBTools gdbTool;
        private string originFeatureTxt;

        public HKDataProcessForm()
        {
            InitializeComponent();
            reportProcessInfo = new ReportProcessInfo(UpdatePercentInfo);
            doneFeatureProcess = new DoneFeatureProcess(UpdateFeatureComplete);
            featureProcessDetail = new FeatureProcessDetail(UpdateFeatureDetail);
            startRoadCheck = new StartRoadCheck(startRoadCheckMethod);
            gdbTool = new GDBTools();
        }



        /*
         * 单击GDB文件选择框，弹出对话框进行选择。
         * 
         */
        private void FindGDBFile(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "请选择GDB文件路径.";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                gdbFilePath = folder.SelectedPath;
                gdbFileTxt.Text = gdbFilePath;
                FillFeaturesToBox(targerFeatures, originFeature);
            }
        }

        /// <summary>
        /// 填充要素到CheckedListBox
        /// </summary>
        private void FillFeaturesToBox(CheckedListBox clb, ComboBox cbx)
        {
            ArrayList features = gdbTool.GetFeatures(gdbFilePath);
            if (features != null)
            {
                clb.Items.Clear();
                cbx.Items.Clear();
                clb.Items.AddRange(features.ToArray());
                cbx.Items.AddRange(features.ToArray());
                processResult.Text = "";
            }
            else
            {
                processResult.Text = "GDB数据库出错！请重新选择。";
            }

        }

        /// <summary>
        /// 程序开始运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled = false;
            gdbFileTxt.Enabled = false;
            targerFeatures.Enabled = false;
            originFeature.Enabled = false;
            HLevel.Enabled = false;
            buffer.Enabled = false;
            Thread process = new Thread(new ThreadStart(this.threadProcess));
            originFeatureTxt = originFeature.Text;
            process.Start();
        }

        private void threadProcess()
        {
            IEnumerator features = targerFeatures.CheckedItems.GetEnumerator();

            ArrayList featureList = new ArrayList();
            while (features.MoveNext())
            {
                featureList.Add((String)features.Current);
            }

            int currFeature = 1;

            String[] HL = HLevel.Lines;

            int total = featureList.Count * HL.Length;

            foreach (String featureName in featureList)
            {
                for (int i = 0, length = HL.Length; i < length; i++)
                {
                    reportProcessInfo(String.Format("当前处理要素：{0} 。级别：{1}", featureName, HL[i]), GetPercent(currFeature, total));
                    UpdateFeature(featureName, originFeatureTxt, HL[i], Double.Parse(buffer.Text));

                    currFeature++;
                }
            }
            doneFeatureProcess("完成");
        }

        /// <summary>
        /// 计算进度
        /// </summary>
        /// <param name="currFeature"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private int GetPercent(int currFeature, int total)
        {
            return (int)currFeature * 100 / total;
        }

        /// <summary>
        /// 更新进度信息
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="percent">进度</param>
        private void UpdatePercentInfo(string info, int percent)
        {
            if (InvokeRequired)
            {
                Invoke(new ReportProcessInfo(UpdatePercentInfo), info, percent);
            }
            else
            {
                updateProcessPercent.Value = percent;
                processResult.Text = info;
            }

        }

        /// <summary>
        /// 更新要素结束信息
        /// </summary>
        /// <param name="info">结束信息</param>
        private void UpdateFeatureComplete(String info)
        {
            if (InvokeRequired)
            {
                Invoke(new DoneFeatureProcess(UpdateFeatureComplete), info);
            }
            else
            {
                processResult.Text = info;
                startBtn.Enabled = true;
                gdbFileTxt.Enabled = true;
                targerFeatures.Enabled = true;
                originFeature.Enabled = true;
                HLevel.Enabled = true;
                buffer.Enabled = true;
            }
        }

        /// <summary>
        /// 更新要素详情
        /// </summary>
        private void UpdateFeatureDetail(string info)
        {
            if (InvokeRequired)
            {
                Invoke(new FeatureProcessDetail(UpdateFeatureDetail), info);
            }
            else
            {
                details.Text = info;
            }
        }

        /// <summary>
        /// 更新要素
        /// </summary>
        /// <param name="fws">要素工作空间</param>
        /// <param name="targetFeature">目标要素</param>
        /// <param name="originFeature">源要素</param>
        /// <param name="hlevel">h级别</param>
        /// <param name="buffer">缓冲区范围</param>
        public void UpdateFeature(String targetFeature, String originFeature, String hlevel, double buffer)
        {
            gdbTool.InitWorkspace();
            IFeatureWorkspace fws = (IFeatureWorkspace)gdbTool.GetWorkspace();
            IFeatureClass features = fws.OpenFeatureClass(targetFeature);
            IFeatureClass origin = fws.OpenFeatureClass(originFeature);
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = "HLevel = " + hlevel;
            ICursor countCorsor = (ICursor)features.Search(filter, true);
            IFeatureCursor cursor = features.Update(filter, true);

            IDataStatistics countStatistics = new DataStatisticsClass();
            countStatistics.Field = "OBJECTID";
            countStatistics.Cursor = (ICursor)countCorsor;
            IStatisticsResults result = countStatistics.Statistics;
            int totalCount = result.Count;
            int num = 1;
            IFeature polygon = cursor.NextFeature();
            while (polygon != null)
            {
                UpdateFeatureDetail(String.Format("当前要素处理进度{0}/{1}", num, totalCount));
                num++;
                //得到几何对象
                Polygon geom = (Polygon)polygon.Shape;
                // 多边形顶点要素数   最后一个点与第一个点重复所以不编辑
                int pointCount = geom.PointCount - 1;

                IPoint targetPoint = new ESRI.ArcGIS.Geometry.Point();
                for (int i = 0; i < pointCount; i++)
                {
                    geom.QueryPoint(i, targetPoint);
                    gdbTool.setPointZ(origin, targetPoint, hlevel, buffer);
                    geom.UpdatePoint(i, targetPoint);
                }
                polygon.Shape = (IGeometry)geom;
                cursor.UpdateFeature(polygon);
                polygon = cursor.NextFeature();
            }
        }

        ///
        private void RSFilePath_MouseClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "请选择GDB文件路径.";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                gdbFilePath = folder.SelectedPath;
                RSFilePath.Text = gdbFilePath;
                FillFeaturesToBox(RSFeatures, originFeatureCBX);
            }
        }

        /// <summary>
        /// 接边检查程序  启动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JieBianBtn_Click(object sender, EventArgs e)
        {
            JieBianBtn.Enabled = false;
            Thread process = new Thread(new ThreadStart(this.RoadCheckProcessStart));
            process.Start();
        }

        /// <summary>
        /// 接边进程开始
        /// </summary>
        private void RoadCheckProcessStart()
        {
            startRoadCheckMethod();
        }

        /// <summary>
        /// 代理接边检查方法
        /// </summary>
        private void startRoadCheckMethod()
        {
            if (InvokeRequired)
            {
                Invoke(new StartRoadCheck(startRoadCheckMethod));
            }
            else
            {
                IEnumerator erator = RSFeatures.CheckedItems.GetEnumerator();
                string originFeature = originFeatureCBX.Text; //基础要素
                ArrayList featureArray = Utils.ConvertEnumerlatorToArrayList(erator); //目标要素
                gdbTool.JiebianProcess(originFeature, featureArray);
                JieBianBtn.Enabled = true;
            }

        }
    }
}


