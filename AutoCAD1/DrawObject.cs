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
