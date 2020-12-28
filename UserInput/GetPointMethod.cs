using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class GetPointMethod
    {

        public static void Create()
        {
            BaseClass.UseTransaction(trans =>
            {
                try
                {
                    //prompt user to click the starting point
                    PromptPointOptions PPO = new PromptPointOptions("Pick starting point..");
                    PromptPointResult PPR = BaseClass.Editor.GetPoint(PPO);
                    Point3d startPoint = PPR.Value;

                    //prompt user to click the end point
                    PPO = new PromptPointOptions("Pick end point");
                    PPO.UseBasePoint = true;
                    PPO.BasePoint = startPoint;
                    PPR = BaseClass.Editor.GetPoint(PPO);
                    Point3d endPoint = PPR.Value;

                    if (startPoint == null || endPoint == null)
                    {
                        BaseClass.Editor.WriteMessage("\nInvalid point(s)");
                        return;
                    }

                    //create line


                    BlockTable bt = trans.GetObject(BaseClass.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    Line ln = new Line(startPoint, endPoint);

                    btr.AppendEntity(ln); //add line to the BlockTabeReccord
                    trans.AddNewlyCreatedDBObject(ln, true);   //add to the transaction

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
