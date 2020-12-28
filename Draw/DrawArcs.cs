using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class DrawArcs
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
                    BaseClass.Editor.WriteMessage("\nDrawing an Arc object: ");

                    Point3d arcCenter = new Point3d(150, 125, 0);
                    double arcRadius = 50;
                    double arcStartAngle = 1;
                    double arcEndAngle = 3;

                    using (Arc arc = new Arc())
                    {
                        arc.Radius = arcRadius;
                        arc.Center = arcCenter;
                        arc.StartAngle = arcStartAngle;
                        arc.EndAngle = arcEndAngle;
                        arc.ColorIndex = 7;

                        arc.SetDatabaseDefaults();

                        btr.AppendEntity(arc); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(arc, true);   //add to the transaction
                    }
                    //set default propreties
                    

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
