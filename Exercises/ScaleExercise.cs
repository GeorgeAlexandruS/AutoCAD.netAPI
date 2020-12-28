using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace DrawObjects
{
    public class ScaleExercise
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

                    //create cirles
                    Circle cir1 = new Circle();
                    cir1.Radius = 5;
                    cir1.Center = new Point3d(0, 0, 0);

                    Circle cir2 = new Circle();
                    cir2.Radius = 2.5;
                    cir2.Center = new Point3d(10, 0, 0);
                    cir2.ColorIndex = 1;

                    Circle cir3 = new Circle();
                    cir3.Radius = 5;
                    cir3.Center = new Point3d(20, 0, 0);

                    DBObjectCollection col = new DBObjectCollection
                    {
                        cir1,
                        cir2,
                        cir3
                    };

                    foreach (Circle cir in col)
                    {
                        btr.AppendEntity(cir); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(cir, true); //add to the transaction

                        if (cir.Radius == 2.5 && cir.ColorIndex == 1)
                        {
                            cir.TransformBy(Matrix3d.Scaling(4, cir.Center));
                        }
                    }

                    // zoom extents
                    BaseClass.Document.SendStringToExecute("._zoom e ", false, false, false);
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
