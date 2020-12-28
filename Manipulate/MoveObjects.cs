using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class MoveObjects
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

                    //create a cirle
                    using (Circle c1 = new Circle())
                    {
                        c1.Radius = 0.5;
                        c1.Center = new Point3d(2, 2, 0);

                        //create matrix to move circle using a vector from (0,0,0)  to (2,0,0)
                        Point3d startPt = new Point3d(0, 0, 0); //create base point
                        Vector3d destVector = startPt.GetVectorTo(new Point3d(2, 0, 0)); //create vector from basepoint to new location
                        c1.TransformBy(Matrix3d.Displacement(destVector)); //apply vector to object

                        //add the new object to the block table record
                        btr.AppendEntity(c1); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(c1, true);   //add to the transaction

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
