using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class PromptKeyword
    {

        public static void Create()
        {
            BaseClass.UseTransaction(trans =>
            {
                try
                {
                    // create prompt  keyword options
                    PromptKeywordOptions PKO = new PromptKeywordOptions("");
                    PKO.Message = "\nWhat would you lik to drow?";
                    PKO.Keywords.Add("Line");
                    PKO.Keywords.Add("Circle");
                    PKO.Keywords.Add("MText");
                    PKO.AllowNone = false;

                    PromptResult PR = BaseClass.Editor.GetKeywords(PKO);
                    string answer = PR.StringResult;
                    if (answer != null)
                    {
                        BlockTable bt = trans.GetObject(BaseClass.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                        switch (answer)
                        {
                            case "Line":
                                //Draw Line
                                DrawLines.Create();
                                break;
                            case "Circle":
                                //Draw Circle
                                DrawCircles.Create();
                                break;
                            case "MText":
                                //Draw MText
                                DrawMtext.Create();
                                break;
                            default:
                                BaseClass.Editor.WriteMessage("\nNo option selected");
                                break;

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
