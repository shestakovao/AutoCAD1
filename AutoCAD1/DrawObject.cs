using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;



namespace AutoCAD1
{
    public class DrawObject
    {

        [CommandMethod("CreateLayer")]
        public void CreateLayer()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
                try
                {
                    LayerTable lt = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                    string layerName = "Координатная сетка";
                    if (lt.Has(layerName))//поиск есть ли слой с таким именем
                    {
                        trans.Abort();
                    }
                    else
                    {
                        lt.UpgradeOpen();
                        LayerTableRecord ltr = new LayerTableRecord();
                        ltr.Name = layerName;
                        ltr.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByLayer, 1);
                        lt.Add(ltr);
                        trans.AddNewlyCreatedDBObject(ltr, true);
                        db.Clayer = lt[layerName];//выбор созданого слоя текущим
                        trans.Commit();
                    }


                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error: " + ex.Message);
                    trans.Abort();
                }
        }

        [CommandMethod("DrawPLine")]
        public void DrawPLine()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
                try
                {
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    Polyline pLine = new Polyline();

                    for (int i = 0; i < 10; i++)
                    {
                        pLine.AddVertexAt(i, new Point2d(i * 10, i * 10), 0, 0, 0);
                    }

                    pLine.AddVertexAt(10, new Point2d(100, 0), 0, 0, 0);

                    pLine.ColorIndex = 30;
                    pLine.LineWeight = LineWeight.LineWeight140;
                    pLine.Closed = true;//полилиния замкнуто
                    btr.AppendEntity(pLine);
                    trans.AddNewlyCreatedDBObject(pLine, true);
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error: " + ex.Message);
                    trans.Abort();
                }
        }

        [CommandMethod("DrawArc")]
        public void DrawArc()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
                try
                {
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using (Arc arc = new Arc())
                    {
                        arc.Center = new Point3d(250, 250, 0);
                        arc.Radius = 20;
                        arc.StartAngle = (double)45/180*Math.PI;//начальный угол в радианах
                        arc.EndAngle = (double)90/180 * Math.PI;//конечный угол в радианах
                        arc.LineWeight = LineWeight.LineWeight060;
                        arc.ColorIndex = 171;
                        btr.AppendEntity(arc);
                        trans.AddNewlyCreatedDBObject(arc, true);

                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error: " + ex.Message);
                    trans.Abort();
                }
        }

        [CommandMethod("DrawCircle")]
        public void DrawCircle()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
                try
                {
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using (Circle circle = new Circle())
                    {
                        circle.Center = new Point3d(150, 150, 0);
                        circle.Radius = 7.5;
                        circle.LineWeight = LineWeight.LineWeight053;
                        circle.ColorIndex = 91;
                        btr.AppendEntity(circle);
                        trans.AddNewlyCreatedDBObject(circle, true);

                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error: " + ex.Message);
                    trans.Abort();
                }
        }

        [CommandMethod("DrawMText")]
        public void DrawMText()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
                try
                {
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    string txt = "AutoCAD111";
                    Point3d insertPoint = new Point3d(200, 200, 0);

                    using (MText mText = new MText())
                    {
                        mText.Contents = txt; //текст
                        mText.Location = insertPoint; //точка вставки
                        mText.Height = 23; //пользовательская высота
                        mText.TextHeight = 30; //Высота текста
                        btr.AppendEntity(mText);
                        trans.AddNewlyCreatedDBObject(mText, true);
                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error: " + ex.Message);
                    trans.Abort();
                }
        }

        [CommandMethod("DrawLine")]
        public void DrawLine()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
                try
                {
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    Point3d pt1 = new Point3d(0, 0, 0);
                    Point3d pt2 = new Point3d(100, 100, 0);
                    Line ln = new Line(pt1, pt2);
                    ln.ColorIndex = 1;
                    ln.LineWeight = LineWeight.LineWeight211;
                    btr.AppendEntity(ln);
                    trans.AddNewlyCreatedDBObject(ln, true);
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error: "+ex.Message);
                    trans.Abort();
                }
        }

    }
}
