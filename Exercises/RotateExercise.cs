using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace DrawObjects
{
    public class RotateExercise
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

                    //create an mtext
                    MText mt1 = new MText();
                    mt1.TextHeight = 3;
                    mt1.Contents = "Rotating MText";
                    mt1.ColorIndex = 1;
                    mt1.Location = new Point3d(10, 10, 0);

                    MText mt2 = mt1.Clone() as MText;
                    mt2.ColorIndex = 1;
                    mt2.Contents = "Rotated MText";

                    //create matrix and move eachcopied entity 20 units to the right

                    //get current coordinate system UCS
                    Matrix3d curUCSMatrix = BaseClass.Document.Editor.CurrentUserCoordinateSystem;
                    CoordinateSystem3d curUCS = curUCSMatrix.CoordinateSystem3d;

                    // rotate the poliline 45deg around the Z-azis of the current UCS
                    // using a basepoint of (4,4.25,0)
                    mt2.TransformBy(Matrix3d.Rotation(0.523599, curUCS.Zaxis, new Point3d(0, 0, 0)));

                    //append new objects
                    btr.AppendEntity(mt2); //add line to the BlockTabeReccord
                    trans.AddNewlyCreatedDBObject(mt2, true);   //add to the transaction
                    BaseClass.Editor.WriteMessage("\n mtext added to db");

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
