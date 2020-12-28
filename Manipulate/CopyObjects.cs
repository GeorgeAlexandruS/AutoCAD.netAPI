using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class CopyObjects
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

                    //create new circle
                    using (Circle c1 = new Circle())
                    {
                        c1.Radius = 4.25;
                        c1.Center = new Point3d(2, 3, 0);

                        btr.AppendEntity(c1); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(c1, true);   //add to the transaction

                        //create copy of circle and change radius
                        Circle c2 = c1.Clone() as Circle;
                        c2.Radius = 1;

                        //addthe cloned circle into the block table record
                        btr.AppendEntity(c2); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(c2, true);   //add to the transaction
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
