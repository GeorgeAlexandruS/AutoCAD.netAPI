using Autodesk.AutoCAD.Runtime;
using System;
using System.IO;

namespace DrawObjects
{
    public class CommandsExercises
    {

        [CommandMethod("ExCopy")]
        public void CopyEx()
        {
            CopyExercise.Create();
        }

        [CommandMethod("ExErase")]
        public void EraseEx()
        {
            EraseExercise.Create();
        }

        [CommandMethod("ExMove")]
        public void MoveEx()
        {
            MoveExercise.Create();
        }

        [CommandMethod("ExMirror")]
        public void MirrorEx()
        {
            MirrorExercise.Create();
        }

        [CommandMethod("ExRotate")]
        public void RotateEx()
        {
            RotateExercise.Create();
        }

        [CommandMethod("ExScale")]
        public void ScaleEx()
        {
            ScaleExercise.Create();
        }
    }
}
