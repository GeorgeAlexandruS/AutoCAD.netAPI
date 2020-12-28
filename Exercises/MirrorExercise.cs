using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class MirrorExercise
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
                    Point3d insPt1 = new Point3d(0, 0, 0);
                    Point3d insPt2 = new Point3d(10, 0, 0);
                    Point3d insPt3 = new Point3d(-10, 0, 0);
                    string text = "Mirrored";

                    MText mt1 = new MText();
                    mt1.TextHeight = 3;
                    mt1.Contents = text;
                    mt1.ColorIndex = 1;
                    mt1.Location = insPt1;

                    BaseClass.Editor.WriteMessage("\n First Mtext created");

                    MText mt2 = new MText();
                    mt2.TextHeight = 2;
                    mt2.Contents = text;
                    mt2.ColorIndex = 2;
                    mt2.Location = insPt2;

                    BaseClass.Editor.WriteMessage("\n Second Mtext created");

                    MText mt3 = new MText();
                    mt3.TextHeight = 2;
                    mt3.Contents = text;
                    mt3.ColorIndex = 2;
                    mt3.Location = insPt3;
                    BaseClass.Editor.WriteMessage("\n Second Mtext created");

                    DBObjectCollection col = new DBObjectCollection
                    {
                        mt1,
                        mt2,
                        mt3
                    };

                    foreach (MText mtext in col)
                    {
                        //add the new object to the block table record
                        btr.AppendEntity(mtext); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(mtext, true);   //add to the transaction
                        if (mtext.TextHeight == 3 && mtext.ColorIndex == 1)
                        {
                            //clone newly created polyline
                            MText cloneMtext = mtext.Clone() as MText;

                            //define the emirror center line
                            Point3d ptFrom = new Point3d(0, 15, 0);
                            Point3d ptTo = new Point3d(20, 15, 0);
                            Line3d ln = new Line3d(ptFrom, ptTo);

                            // Mirror the object accross the mirror line
                            cloneMtext.TransformBy(Matrix3d.Mirroring(ln));

                            //add the mirrored object to the block table record
                            btr.AppendEntity(cloneMtext); //add line to the BlockTabeReccord
                            trans.AddNewlyCreatedDBObject(cloneMtext, true);   //add to the transaction
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
