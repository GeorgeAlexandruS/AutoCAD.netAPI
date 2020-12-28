using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class DrawPLine
    {

        public static void Create()
        {


            BaseClass.UseTransaction(trans =>
            {
                try
                {

                    BlockTable bt;
                    bt = trans.GetObject(BaseClass.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //send message to the user
                    BaseClass.Editor.WriteMessage("\nDrawing a Polyline object: ");

                    Point2d pt0 = new Point2d(50, 100);

                    Polyline pline = new Polyline();
                    pline.AddVertexAt(0, new Point2d(0,0),0,0,0);
                    pline.AddVertexAt(1, new Point2d(10, 10), 0, 0, 0);
                    pline.AddVertexAt(2, new Point2d(10, 15), 0, 0, 0);
                    pline.AddVertexAt(3, new Point2d(30, 42), 0, 0, 0);
                    pline.AddVertexAt(4, new Point2d(55, 25), 0, 0, 0);
                    pline.AddVertexAt(5, pt0, 0, 0, 0);
                   

                    btr.AppendEntity(pline); //add line to the BlockTabeReccord
                    trans.AddNewlyCreatedDBObject(pline, true);   //add to the transaction

                    //set default propreties
                    pline.SetDatabaseDefaults();

                }
                catch (System.Exception ex)
                {
                    BaseClass.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            });

        }


    }
}
