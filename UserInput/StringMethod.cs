using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class StringMethod
    {

        public static void Create()
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
