
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS;
using ESRI.ArcGIS.EngineCore;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;

namespace honkondata
{
    class AETool
    {
        private static IWorkspaceFactory wfs;
        private static IWorkspace ws;

        public String fileName = "D:/map/RoadPolygon_final_20110901.gdb";

        public AETool()
        {
            // 注册产品
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine);

            // 初始化授权
            AoInitialize ao = new AoInitialize();
            ao.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);

            // 获取工作空间工厂
            wfs = new FileGDBWorkspaceFactory();
           
        }


        static void Main(string[] args)
        {
            AETool ae = new AETool();
            ae.UpdateFeatures("JunctionPolygonZ",0);
            ae.UpdateFeatures("JunctionPolygonZ",1);
            ae.UpdateFeatures("RoadAssetPolygonZ",0);
            ae.UpdateFeatures("RoadAssetPolygonZ",1);
            ae.UpdateFeatures("RoadPolygonZ",0);
            ae.UpdateFeatures("RoadPolygonZ",1);
        }

        /**
         * 获取当前工作空间
         */
        private IWorkspace GetWorkspace()
        {
            if (ws == null)
            {
                ws = wfs.OpenFromFile(fileName, 0);
            }
            return ws;
        }

        /**
         * 更改工作空间并返回
         */
        private IWorkspace ChangeWorkspace(String fileName)
        {
            ws = wfs.OpenFromFile(fileName, 0);
            return ws;
        }
        
        /*
         * 更新要素类
         */
        private void UpdateFeatures(String featureClass, int hlevel)
        {
            IFeatureWorkspace fws = (IFeatureWorkspace)GetWorkspace();
            IFeatureClass features = fws.OpenFeatureClass(featureClass);
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
                    IPoint point = geom.get_Point(i);
                    setPointZ(point,polygon,hlevel);
                    geom.UpdatePoint(i, point);
                }
                polygon.Shape = (IGeometry)geom;
                cursor.UpdateFeature(polygon);
                polygon = cursor.NextFeature();
            }
            Console.WriteLine("运行结束");
        }

        /*
         * point 要赋值的顶点要素
         * geom 面范围
         */
        private void setPointZ(IPoint point, IFeature geom, int HLevel)
        { 

            //根据点坐标找到指定距离内的三维点云数据
            IFeatureWorkspace fws = (IFeatureWorkspace)GetWorkspace();
            IFeatureClass points = fws.OpenFeatureClass("point");


            ISpatialFilter sf = new SpatialFilter();
            //sf.WhereClause = "HLevel=1";
            ITopologicalOperator topo = (ITopologicalOperator)point;
            IGeometry bufferPoint = topo.Buffer(1);//缓冲1米的距离

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
                IPoint p = (IPoint)gc.get_Geometry(0);
                Console.WriteLine(p.Z);
                point.Z = p.Z;
            }
            
        }



    }
}
