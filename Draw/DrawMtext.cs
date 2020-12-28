using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace DrawObjects
{
    public class DrawMtext
    {

        public static void Create()
        {


            BaseClass.UseTransaction(trans =>
            {
                try
                {
                    BaseClass.Editor.WriteMessage("\nDraw MText Exercise");
                    BlockTable bt;
                    bt = trans.GetObject(BaseClass.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //send message to the user
                    BaseClass.Editor.WriteMessage("\nDrawing an Mtext object: ");

                    //specify mtext prameters

                    string txt = "Hello George";
                    Point3d insPt = new Point3d(200, 200, 0);
                    using (MText mtx = new MText())
                    {
                        mtx.Contents = txt;
                        mtx.Location = insPt;
                        mtx.ColorIndex = 2;

                        btr.AppendEntity(mtx); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(mtx, true);   //add to the transaction
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
