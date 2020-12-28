using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace DrawObjects
{
    public class ScaleObjects
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
                    using (Polyline pline = new Polyline())
                    {
                        pline.AddVertexAt(0, new Point2d(1, 2), 0, 0, 0);
                        pline.AddVertexAt(1, new Point2d(1, 3), 0, 0, 0);
                        pline.AddVertexAt(2, new Point2d(2, 3), 0, 0, 0);
                        pline.AddVertexAt(3, new Point2d(3, 3), 0, 0, 0);
                        pline.AddVertexAt(4, new Point2d(4, 4), 0, 0, 0);
                        pline.AddVertexAt(5, new Point2d(4, 2), 0, 0, 0);
                        //pline.SetBulgeAt(1, -2);//Create bulge of -2 at vertex 1
                        pline.Closed = true; //Close polyline

                        // Scale the object by 0.5
                        // using a basepoint of (4,4.25,0)
                        pline.TransformBy(Matrix3d.Scaling(0.5, new Point3d(4, 4.25,0))); 

                        //add the new object to the block table record
                        btr.AppendEntity(pline); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(pline, true);   //add to the transaction

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
