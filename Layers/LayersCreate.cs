using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Colors;


namespace DrawObjects
{
    public class LayersCreate
    {

        public static void Create()
        {


            BaseClass.UseTransaction(trans =>
            {
                try
                {
                    LayerTable LT = trans.GetObject(BaseClass.Database.LayerTableId, OpenMode.ForRead) as LayerTable;

                    string layerName = "GASLayer";
                    if (LT.Has(layerName) == true)
                    {
                        BaseClass.Editor.WriteMessage("\nlayer elready exists");
                        trans.Abort();
                    }
                    else
                    {
                        LT.UpgradeOpen();
                        using (LayerTableRecord LTR = new LayerTableRecord())
                        {
                            LTR.Name = layerName;
                            LTR.Color = Color.FromColorIndex(ColorMethod.ByLayer, 1);
                            LT.Add(LTR);
                                                        
                            trans.AddNewlyCreatedDBObject(LTR, true);   //add to the transaction
                            BaseClass.Database.Clayer = LT[layerName]; //make new layer the currrent layer

                            BaseClass.Editor.WriteMessage("\nThe newly created layer [" + LTR.Name + "] is now the current layer");

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
