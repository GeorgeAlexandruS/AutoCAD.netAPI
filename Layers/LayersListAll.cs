using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class LayersListAll
    {

        public static void Create()
        {


            BaseClass.UseTransaction(trans =>
            {
                try
                {

                    LayerTable LT = trans.GetObject(BaseClass.Database.LayerTableId, OpenMode.ForRead) as LayerTable;

                    foreach (ObjectId LyId in LT)
                    {

                        LayerTableRecord LTR = trans.GetObject(LyId, OpenMode.ForRead) as LayerTableRecord;
                        BaseClass.Editor.WriteMessage("\nLayer name: " + LTR.Name);
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
