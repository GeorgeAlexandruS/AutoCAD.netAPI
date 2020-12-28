using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.Linq;

namespace DrawObjects
{
    public static class BaseClass
    {
        public static Document Document => Application.DocumentManager.MdiActiveDocument;
        public static Database Database => Document.Database;
        public static Editor Editor => Document.Editor;

        public static void UseTransaction(Action<Transaction> action)
        {
            using (var transaction = BaseClass.Database.TransactionManager.StartTransaction())
            {
                action(transaction);

                transaction.Commit();
            }
        }

    }
}
