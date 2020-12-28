using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class CopyMultipleObjects
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
                    Circle c1 = new Circle();
                    c1.Radius = 5;
                    c1.Center = new Point3d(0, 0, 0);
                    btr.AppendEntity(c1); //add line to the BlockTabeReccord
                    trans.AddNewlyCreatedDBObject(c1, true);   //add to the transaction

                    //create second new circle
                    Circle c2 = new Circle();
                    c2.Radius = 7;
                    c2.Center = new Point3d(0, 0, 0);
                    btr.AppendEntity(c2); //add line to the BlockTabeReccord
                    trans.AddNewlyCreatedDBObject(c2, true);   //add to the transaction

                    //add the new circles to a collection
                    DBObjectCollection col = new DBObjectCollection();
                    col.Add(c1);
                    col.Add(c2);
                    
                    //copy and move entity in collection
                    foreach (Entity colEnt in col)
                    {
                        Entity ent;
                        ent = colEnt.Clone() as Entity;
                        ent.ColorIndex = 1;

                        //create matrix and move eachcopied entity 20 units to the right
                        ent.TransformBy(Matrix3d.Displacement(new Vector3d(20, 50, 0)));

                        //append new objects
                        btr.AppendEntity(ent); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(ent, true);   //add to the transaction

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
