using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;


namespace DrawObjects
{
    public class GetDistanceMethod
    {

        public static void Create()
        {
            BaseClass.UseTransaction(trans =>
            {
                try
                {
                    //prompt user to click the starting point
                    PromptDoubleResult PDR = BaseClass.Editor.GetDistance("\nPick two points to get distance");
                    Application.ShowAlertDialog("\nThe distance between the selected points is " + PDR.Value.ToString());

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
