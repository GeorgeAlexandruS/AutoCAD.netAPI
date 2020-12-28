using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Colors;


namespace DrawObjects
{
    public class StringInputMethod
    {


        public static void CurrentLayer()
        {
            BaseClass.UseTransaction(trans =>
            {
                try
                {
                    LayerTable LT = trans.GetObject(BaseClass.Database.LayerTableId, OpenMode.ForRead) as LayerTable;
                    PromptStringOptions prompt = new PromptStringOptions("\n>>Enter layer name: ");
                    prompt.AllowSpaces = false;
                    //Get the result of the user input
                    PromptResult result = BaseClass.Editor.GetString(prompt);
                    if (result.Status == PromptStatus.OK)
                    {
                        string layerName = result.StringResult;
                                                
                        if (LT.Has(layerName) == true)
                        {
                            BaseClass.Editor.WriteMessage("\nLayer " + layerName + " exists and is now set to current");
                            BaseClass.Database.Clayer = LT[layerName]; //make new layer the currrent layer
                        }
                        else
                        {
                            Application.ShowAlertDialog("\nThe Layer " + layerName + " you entered does not exist");
                        }
                    }
                    else
                    {
                        Application.ShowAlertDialog("\nNo layer entered");
                    }
                }

                catch (System.Exception ex)
                {
                    BaseClass.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            });
        }



        public static void GetUserString()
        {


            BaseClass.UseTransaction(trans =>
            {
                try
                {
                    //prompt user using PromptStringOptions
                    PromptStringOptions prompt = new PromptStringOptions("\n>>Enter your name: \n>>");
                    prompt.AllowSpaces = true;

                    //Get the result of the user input
                    PromptResult result = BaseClass.Editor.GetString(prompt);
                    if (result.Status == PromptStatus.OK)
                    {
                        string name = result.StringResult;
                        BaseClass.Editor.WriteMessage("\nHello " + name);
                        Application.ShowAlertDialog("Your name is :" + name);
                    }
                    else
                    {
                        BaseClass.Editor.WriteMessage("\nNo name entered");
                        Application.ShowAlertDialog("\nNo name entered");
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
