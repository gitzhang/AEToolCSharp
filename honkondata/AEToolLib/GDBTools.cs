using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace AEToolLib
{
    public class GDBTools
    {
        private IWorkspaceFactory wsf;

        private IWorkspace ws;

        private IFeatureWorkspace fws;

        private String gdbFilePath;

        private int hWnd = 0;

        public const String MULTIPOINT = "Multipoint ZM";
        public const String POINT = "Point ZM";

        /// <summary>
        /// 获取工作空间工厂
        /// </summary>
        private void GetGDBWsf()
        {
            if (wsf == null)
            {
                wsf = new FileGDBWorkspaceFactory();
            }
        }

        /// <summary>
        /// 获取工作空间
        /// </summary>
        /// <param name="gdbFilePath">gdb文件路径</param>
        /// <returns>返回工作空间</returns>
        public IWorkspace GetWorkspace(String gdbFilePath)
        {
            if (ws == null)
            {
                GetGDBWsf();
                this.gdbFilePath = gdbFilePath;
                try
                {
                    ws = wsf.OpenFromFile(gdbFilePath, hWnd);
                    fws = (IFeatureWorkspace)ws;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return ws;
        }

        public void InitWorkspace()
        {
            GetWorkspace(gdbFilePath);
        }

        /// <summary>
        /// 得到gdb中所有的要素
        /// </summary>
        /// <param name="gdbFilePath">gdb文件路径</param>
        public ArrayList GetFeatures(string gdbFilePath)
        {
            ArrayList features = new ArrayList();
            IWorkspace fws = GetWorkspace(gdbFilePath);
            if (fws == null)
                return null;
            IEnumDataset datasets = fws.get_Datasets(esriDatasetType.esriDTFeatureDataset);
            IDataset dataset = datasets.Next();

            while (dataset != null)
            {
                IEnumDataset subDatasets = dataset.Subsets;
                IDataset feature = subDatasets.Next();
                while (feature != null)
                {
                    features.Add(feature.Name);
                    feature = subDatasets.Next();
                }
                dataset = datasets.Next();
            }
            return features;
        }

        /// <summary>
        /// 批量更新要素
        /// </summary>
        /// <param name="targetFeatures"></param>
        /// <param name="originFeature"></param>
        /// <param name="HLevel"></param>
        /// <param name="buffer"></param>
        public void UpdateFeatures(IEnumerator targetFeatures, String originFeature, String PT, String[] HLevel, double buffer)
        {
            while (targetFeatures.MoveNext())
            {
                String featureName = (String)targetFeatures.Current;
                for (int i = 0, length = HLevel.Length; i < length; i++)
                {
                    UpdateFeature(featureName, originFeature, PT, HLevel[i], buffer, null);
                }
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
        public void UpdateFeature(String targetFeature, String originFeature, String PT, String hlevel, double buffer, Delegate delegateMethod)
        {
            InitWorkspace();
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            IFeatureClass features = fws.OpenFeatureClass(targetFeature);
            IFeatureClass origin = fws.OpenFeatureClass(originFeature);
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = "HLevel = " + hlevel;
            IFeatureCursor cursor = features.Update(filter, true);

            IFeature polygon = cursor.NextFeature();
            while (polygon != null)
            {
                //得到几何对象
                Polygon geom = (Polygon)polygon.Shape;
                // 多边形顶点要素数   最后一个点与第一个点重复所以不编辑
                int pointCount = geom.PointCount - 1;

                for (int i = 0; i < pointCount; i++)
                {
                    IPoint targetPoint = null;
                    geom.QueryPoint(i, targetPoint);
                    setPointZ(origin, PT, targetPoint, hlevel, buffer);
                    geom.UpdatePoint(i, targetPoint);
                }
                polygon.Shape = (IGeometry)geom;
                cursor.UpdateFeature(polygon);
                polygon = cursor.NextFeature();
            }
        }



        /// <summary>
        ///  赋值
        /// </summary>
        /// <param name="fws">要素工作空间</param>
        /// <param name="originFeature">数据源要素</param>
        /// <param name="point">目标点</param>
        /// <param name="HLevel">HLevel</param>
        /// <param name="buffer">缓冲范围</param>
        public void setPointZ(IFeatureClass originFeature, String PT, IPoint targetPoint, String HLevel, double buffer)
        {

            //根据点坐标找到指定距离内的三维点云数据
            ISpatialFilter sf = new SpatialFilter();
            ITopologicalOperator topo = (ITopologicalOperator)targetPoint;
            IGeometry bufferPoint = topo.Buffer(buffer);//缓冲距离

            sf.Geometry = bufferPoint;
            sf.GeometryField = "shape";
            sf.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
            sf.WhereClause = "HLevel = " + HLevel;
            IFeatureCursor cursor = originFeature.Search(sf, true);
            IFeature row = cursor.NextFeature();
            if (row != null)
            {
                IPoint point = null;
                if (MULTIPOINT.Equals(PT))
                {
                    IMultipoint closePoint = (IMultipoint)row.Shape;
                    IGeometryCollection gc = (IGeometryCollection)closePoint;
                    point = (IPoint)gc.get_Geometry(0);
                }
                else if (POINT.Equals(PT))
                {
                    point = (IPoint)row.Shape;
                }
                else
                {
                    return;
                }
                double z = Math.Round(point.Z, 2);
                targetPoint.Z = point.Z;
            }

        }

        /// <summary>
        /// 以基准要素为数据源，为目标要素赋值
        /// </summary>
        /// <param name="originFeature">基准要素</param>
        /// <param name="featureArray">目标要素集</param>
        public void JiebianProcess(String originFeature, ArrayList featureArray)
        {
            InitWorkspace();
            if (featureArray == null || featureArray.Count == 0)
            {
                return;
            }
            //接边流程

            //1 取出基准要素
            IFeatureClass featureClass = fws.OpenFeatureClass(originFeature);

            //2 查询出所有的要素(可更新要素)
            IFeatureCursor cursor = featureClass.Search(null, true);

            //3 循环接边
            //3.1 取出一个面
            IFeature updateFeature = cursor.NextFeature();

            while (updateFeature != null)
            {

                //3.2 取出这个面的所有点
                Polygon updatePolygon = (Polygon)updateFeature.Shape;
                int pointCount = updatePolygon.PointCount - 1; //多边形最后一个点与第一个点重复，不编辑
                ESRI.ArcGIS.Geometry.Point point = new ESRI.ArcGIS.Geometry.Point();
                for (int i = 0; i < pointCount; i++)
                {

                    updatePolygon.QueryPoint(i, point);


                    //3.3 判断与点接触的面,条件这个面不是自己
                    ISpatialFilter spatiaFilter = new SpatialFilter();
                    spatiaFilter.GeometryField = "shape";
                    spatiaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelTouches;
                    spatiaFilter.Geometry = point;

                    //3.4 根据条件循环查询
                    foreach (string otherFeatureName in featureArray)
                    {
                        if (originFeature.Equals(otherFeatureName))
                        {
                            spatiaFilter.WhereClause = "OBJECTID <> " + updateFeature.OID;
                        }

                        IFeatureClass otherFeature = fws.OpenFeatureClass(otherFeatureName);
                        IFeatureCursor updateCursor = otherFeature.Update(spatiaFilter, true);
                        IFeature feature = updateCursor.NextFeature();// 查询到的接触面
                        while (feature != null)
                        {
                            Polygon polygon = (Polygon)feature.Shape;
                            //找到同名点 找到立即跳出
                            int PPointCount = polygon.PointCount - 1;
                            IPoint equalsPoint = new Point();
                            for (int j = 0; j < PPointCount; j++)
                            {
                                polygon.QueryPoint(j, equalsPoint);
                                if (IsEqualPoint(point, equalsPoint))
                                {
                                    equalsPoint.Z = point.Z;
                                    polygon.UpdatePoint(j, equalsPoint);
                                    break;
                                }
                            }
                            feature = updateCursor.NextFeature();// 下一个接触面
                        }
                    }
                }
                updateFeature = cursor.NextFeature();
            }
        }

        /// <summary>
        /// 判断是否为同名点
        /// </summary>
        /// <param name="point"></param>
        /// <param name="equalsPoint"></param>
        /// <returns></returns>
        private bool IsEqualPoint(IPoint point, IPoint equalsPoint)
        {
            if (point.Equals(equalsPoint))
            {
                return true;
            }
            else
            {
                return Math.Round(point.X, 3) == Math.Round(equalsPoint.X, 3) && Math.Round(point.Y, 3) == Math.Round(equalsPoint.Y, 3);
            }

        }

        public IWorkspace GetWorkspace()
        {
            InitWorkspace();
            return ws;
        }
    }
}
