using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class DrawCircles
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
                    BaseClass.Editor.WriteMessage("\nDrawing a Circle object: ");

                    Point3d circleCenter = new Point3d(2, 3, 0);
                    double circleRadius = 4.25;
                    using (Circle circle = new Circle())
                    {
                        circle.Radius = circleRadius;
                        circle.Center = circleCenter;
                        circle.ColorIndex = 4;

                        btr.AppendEntity(circle); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(circle, true);   //add to the transaction
                    }


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
