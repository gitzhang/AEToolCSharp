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

namespace GDBProcessWin
{
    public partial class HKDataProcessForm : Form
    {
        /// <summary>
        /// gdb文件路径
        /// </summary>
        private String gdbFilePath;

        /// <summary>
        /// GDB数据库工具
        /// </summary>
        private GDBTools gdbTool;

        public HKDataProcessForm()
        {
            InitializeComponent();
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
                FillFeaturesToBox();
            }
        }

        /// <summary>
        /// 填充要素到CheckedListBox
        /// </summary>
        private void FillFeaturesToBox()
        {
            ArrayList features = gdbTool.GetFeatures(gdbFilePath);
            targerFeatures.Items.Clear();
            targerFeatures.Items.AddRange(features.ToArray());
        }

        /// <summary>
        /// 程序开始运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startBtn_Click(object sender, EventArgs e)
        {
            Thread process = new Thread(new ThreadStart(this.threadProcess));
            process.Start();
        }

        private void threadProcess()
        {
           IEnumerator features = targerFeatures.CheckedItems.GetEnumerator();
           gdbTool.UpdateFeatures(features, originFeature.Text, HLevel.Lines, Double.Parse(buffer.Text));
           processResult.Text = "完成";
        }

        
    }
}
