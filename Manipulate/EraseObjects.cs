using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class EraseObjects
    {

        public static void Create()
        {


            BaseClass.UseTransaction(trans =>
            {
                try
                {
                    //Open the BlockTable for Read
                    BlockTable bt;
                    bt = trans.GetObject(BaseClass.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    //Open the BlockTableRecord for Write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //send message to the user
                    BaseClass.Editor.WriteMessage("\nManipulating an object: ");

                    //create new polyline

                    using (Polyline pl = new Polyline())
                    {
                        pl.AddVertexAt(0, new Point2d(2, 4), 0, 0,0);
                        pl.AddVertexAt(1, new Point2d(4, 2), 0, 0, 0);
                        pl.AddVertexAt(2, new Point2d(6, 4), 0, 0, 0);

                        //add the new object to the block table record
                        btr.AppendEntity(pl); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(pl, true);   //add to the transaction

                        //this command uses an existing AutoCAD command
                        BaseClass.Document.SendStringToExecute("._zoom e ", false, false, false);

                        //regen drawing and send an alert message
                        BaseClass.Editor.Regen();
                        Application.ShowAlertDialog("Erase the newly added polyline");

                        //Erase the polyline
                        pl.Erase(true);

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
