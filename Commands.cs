﻿using Autodesk.AutoCAD.Runtime;
using System;
using System.IO;

namespace DrawObjects
{
    public class Commands
    {

        [CommandMethod("GASDrawLine")]
        public void CreateLine()
        {
            DrawLines.Create();
        }

        [CommandMethod("GASDrawMText")]
        public void CreateMText()
        {
            DrawMtext.Create();
        }

        [CommandMethod("GASDrawMLeader")]
        public void CreateMLeader()
        {
            DrawLeader.Create();
        }

        [CommandMethod("GASDrawCircle")]
        public void CreateCircle()
        {
            DrawCircles.Create();
        }

        [CommandMethod("GASDrawArc")]
        public void CreateArc()
        {
            DrawArcs.Create();
        }

        [CommandMethod("GASDrawPolyLine")]
        public void CreatePLine()
        {
            DrawPLine.Create();
        }

        [CommandMethod("GASCloneObject")]
        public void CloneObject()
        {
            CopyObjects.Create();
        }

        [CommandMethod("GASCloneMultipleObject")]
        public void CloneMultipleObject()
        {
            CopyMultipleObjects.Create();
        }
        
        [CommandMethod("GASEraseObject")]
        public void EraseObject()
        {
            EraseObjects.Create();
        }

        [CommandMethod("GASmoveObject")]
        public void MoveObject()
        {
            MoveObjects.Create();
        }

        [CommandMethod("GASMirrorObject")]
        public void MirrorObject()
        {
            MirrorObjects.Create();
        }

        [CommandMethod("GASRotateObject")]
        public void RotateObject()
        {
            RotateObjects.Create();
        }

        [CommandMethod("GASScaleObject")]
        public void ScaleObject()
        {
            ScaleObjects.Create();
        }


        [CommandMethod("GASListAllLayers")]
        public void LayersList()
        {
            LayersListAll.Create();
        }


        [CommandMethod("GASCreateLayer")]
        public void LayerCreate()
        {
            LayersCreate.Create();
        }

        [CommandMethod("GASSetLayerToObject")]
        public void LayerSet()
        {
            LayersSetToObject.Create();
        }
    }
}