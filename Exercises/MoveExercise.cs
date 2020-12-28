using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class MoveExercise
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

                    //create 3 mtextobjects
                    Point3d insPt = new Point3d(0, 0, 0);

                    MText mt1 = new MText();
                    mt1.Contents = "Move me";
                    mt1.ColorIndex = 3;
                    mt1.Location = insPt;
                    BaseClass.Editor.WriteMessage("\n First Mtext created");

                    MText mt2 = new MText();
                    mt2.Contents = "Don't move me";
                    mt2.ColorIndex = 4;
                    mt2.Location = insPt;
                    BaseClass.Editor.WriteMessage("\n Second Mtext created");

                    MText mt3 = new MText();
                    mt3.Contents = "Don't move me either";
                    mt3.ColorIndex = 1;
                    mt3.Location = insPt;
                    BaseClass.Editor.WriteMessage("\n Third Mtext created");

                    //add the new circles to a collection
                    DBObjectCollection col = new DBObjectCollection
                    {
                        mt1,
                        mt2,
                        mt3
                    };
                    BaseClass.Editor.WriteMessage("\n Objects added to collection");

                    foreach (MText mt in col)
                    {
                        //addthe cloned circle into the block table record
                        btr.AppendEntity(mt); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(mt, true);   //add to the transaction
                        BaseClass.Editor.WriteMessage("\n Objects added to database");

                        if (mt.Text.ToLower() == "Move me") //mt.Content works as well
                        {
                            //create matrix to move circle using a vector from (0,0,0)  to (50,50,0)
                            Point3d startPt = new Point3d(0, 0, 0); //create base point
                            Vector3d destVector = startPt.GetVectorTo(new Point3d(50, 50, 0)); //create vector from basepoint to new location
                            mt.TransformBy(Matrix3d.Displacement(destVector)); //apply vector to object
                            BaseClass.Editor.WriteMessage("\n Mtext with content 'Move me' moved");
                        }
                    }

                    //this command uses an existing AutoCAD command
                    BaseClass.Document.SendStringToExecute("._zoom e ", false, false, false);
                    BaseClass.Editor.WriteMessage("\n Zoom executed");

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
