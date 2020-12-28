using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class CopyExercise
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
                    c1.Radius = 1;
                    c1.Center = new Point3d(0, 0, 0);

                    //create second new circle
                    Circle c2 = new Circle();
                    c2.Radius = 2;
                    c2.Center = new Point3d(10, 10, 0);

                    //create second new circle
                    Circle c3 = new Circle();
                    c3.Radius = 5;
                    c3.Center = new Point3d(30, 30, 0);
                    c3.ColorIndex = 5;

                    //add the new circles to a collection
                    DBObjectCollection col = new DBObjectCollection
                    {
                        c1,
                        c2,
                        c3
                    };

                    //copy and move entity in collection
                    foreach (Circle colEnt in col)
                    {
                        btr.AppendEntity(colEnt); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(colEnt, true);   //add to the transaction

                        if (colEnt.Radius == 2)
                        {
                            Circle c4 = colEnt.Clone() as Circle;
                            c4.ColorIndex = 3;
                            c4.Radius = 10;

                            //create matrix and move eachcopied entity 20 units to the right
                            c4.TransformBy(Matrix3d.Displacement(new Vector3d(50, 0, 0)));

                            //append new objects
                            btr.AppendEntity(c4); //add line to the BlockTabeReccord
                            trans.AddNewlyCreatedDBObject(c4, true);   //add to the transaction

                        }


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
