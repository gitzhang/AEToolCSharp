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
              } catch(Exception)
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
        public void UpdateFeatures(IEnumerator targetFeatures,String originFeature,String[] HLevel,double buffer)
        {
            while (targetFeatures.MoveNext())
            {
                String featureName = (String)targetFeatures.Current;
                for (int i = 0, length = HLevel.Length;i < length ; i++)
                {
                    UpdateFeature(featureName, originFeature,HLevel[i],buffer);
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
        public void UpdateFeature(String targetFeature,String originFeature, String hlevel,double buffer)
        {
            InitWorkspace();
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            IFeatureClass features = fws.OpenFeatureClass(targetFeature);
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
                    IPoint targetPoint = geom.get_Point(i);
                    setPointZ(fws, originFeature, targetPoint, hlevel, buffer);
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
        public void setPointZ(IFeatureWorkspace fws, String originFeature, IPoint targetPoint, String HLevel,double buffer)
        {

            //根据点坐标找到指定距离内的三维点云数据
            IFeatureClass points = fws.OpenFeatureClass(originFeature);

            ISpatialFilter sf = new SpatialFilter();
            ITopologicalOperator topo = (ITopologicalOperator)targetPoint;
            IGeometry bufferPoint = topo.Buffer(buffer);//缓冲1米的距离

            sf.Geometry = bufferPoint;
            sf.GeometryField = "shape";
            sf.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
            sf.WhereClause = "HLevel = " + HLevel;
            IFeatureCursor cursor = points.Search(sf, true);
            IFeature row = cursor.NextFeature();
            if (row != null)
            {
                IMultipoint closePoint = (IMultipoint)row.Shape;
                IGeometryCollection gc = (IGeometryCollection)closePoint;
                IPoint point = (IPoint)gc.get_Geometry(0);
                targetPoint.Z = point.Z;
            }

        }

        public void JiebianProcess(ArrayList featureArray)
        {
            InitWorkspace();
            if(featureArray == null || featureArray.Count == 0)
            {
                return ;
            }
            foreach (String featureName in featureArray) //循环接边
            {
                //接边流程

                //1 取出一个面
                IFeatureClass featureClass = fws.OpenFeatureClass(featureName);

                //2 查询出所有的要素(可更新要素)
                IFeatureCursor updateCursor = featureClass.Update(null, true);

                //3 循环接边
                //3.1 取出一个面
                IFeature updateFeature = updateCursor.NextFeature();

                //3.2 取出这个面的所有点
                Polygon updatePolygon = (Polygon)updateFeature.Shape;
                int pointCount = updatePolygon.PointCount - 1; //多边形最后一个点与第一个点重复，不编辑

                for (int i = 0; i < pointCount; i++)
                {
                    ESRI.ArcGIS.Geometry.Point point = (Point)updatePolygon.get_Point(i);

                    //3.3 判断与点接触的面,条件这个面不是自己
                    ISpatialFilter spatiaFilter = new SpatialFilter();
                    spatiaFilter.GeometryField = "shape";
                    spatiaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelTouches;
                    spatiaFilter.Geometry = updatePolygon.get_Point(i);
                    
                    //3.4 根据条件循环查询
                    foreach (string otherFeatureName in featureArray)
                    {
                        if (featureName.Equals(otherFeatureName))
                        {
                            spatiaFilter.WhereClause = "OBJECTID <> " + updateFeature.OID;
                        }

                        IFeatureClass otherFeature = fws.OpenFeatureClass(otherFeatureName);
                        IFeatureCursor cursor = otherFeature.Update(spatiaFilter,true);
                        IFeature feature = cursor.NextFeature();// 查询到的接触点
                        int id = feature.OID;
                    }
                }

            }
        }

        public IWorkspace GetWorkspace()
        {
            InitWorkspace();
            return ws;
        }
    }
}
