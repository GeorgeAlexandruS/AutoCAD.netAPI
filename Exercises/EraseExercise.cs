using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class EraseExercise
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

                    using (Line ln = new Line())
                    {
                        ln.StartPoint = new Point3d(0, 0, 0);
                        ln.EndPoint = new Point3d(10, 10, 0);

                        // Add the new object to the BlockTable record
                        btr.AppendEntity(ln);
                        trans.AddNewlyCreatedDBObject(ln, true);

                        // Create a circle 
                        using (Circle c1 = new Circle())
                        {
                            c1.Center = new Point3d(0, 0, 0);
                            c1.Radius = 2;

                            // Add the new object to the BlockTable record
                            btr.AppendEntity(c1);
                            trans.AddNewlyCreatedDBObject(c1, true);

                            // Create a Polyline
                            Polyline pl = new Polyline();
                            pl.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                            pl.AddVertexAt(1, new Point2d(-10, -10), 0, 0, 0);
                            pl.AddVertexAt(2, new Point2d(20, -20), 0, 0, 0);

                            // Add the new object to the BlockTable record
                            btr.AppendEntity(pl);
                            trans.AddNewlyCreatedDBObject(pl, true);

                            // Create the collection
                            DBObjectCollection col = new DBObjectCollection();
                            col.Add(ln);
                            col.Add(c1);
                            col.Add(pl);

                            foreach (Entity ent in col)
                            {
                                //if (ent.GetType() == typeof(Circle))
                                if (ent is Circle)
                                {
                                    ent.Erase(true);
                                }
                                else if (ent is Line)
                                {
                                    ent.ColorIndex = 2;
                                }
                                else
                                {
                                    ent.ColorIndex = 3;
                                }
                            }
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
