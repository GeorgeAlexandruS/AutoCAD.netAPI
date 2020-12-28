using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

namespace DrawObjects
{
    public class DrawLeader
    {

        public static void Create()
        {


            BaseClass.UseTransaction(trans =>
            {
                try
                {
                    BaseClass.Editor.WriteMessage("\nDraw MLeader Exercise");
                    BlockTable bt;
                    bt = trans.GetObject(BaseClass.Database.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //send message to the user
                    BaseClass.Editor.WriteMessage("\nDrawing an Mleader object: ");

                    //specify mtext prameters

                    string txt = "Hello George";
                    Point3d insPt = new Point3d(200, 200, 0);
                    using (MLeader ldr = new MLeader())
                    {

                        ldr.ColorIndex = 1;

                        MText mtext = new MText();
                        using (mtext)
                        {
                            mtext.Contents = txt;
                            ldr.MText = mtext;
                        }

                        //adding leaders
                        int idx = ldr.AddLeaderLine(new Point3d(10, 20, 0));
                        ldr.AddFirstVertex(idx, new Point3d(0, 0, 0));
                        ldr.AddLastVertex(idx, new Point3d(55, 100, 0));
                       
                        btr.AppendEntity(ldr); //add line to the BlockTabeReccord
                        trans.AddNewlyCreatedDBObject(ldr, true);   //add to the transaction

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
