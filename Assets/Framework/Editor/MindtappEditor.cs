using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using MindTAPP.Unity.Utilities;

namespace MindTAPP.Unity.Framework
{
    public static class MindtappMenuItems
    {
        [MenuItem("GameObject/Framework/Lives", false, 10)]
        public static void CreateLives(MenuCommand menuCommand)
        {
            GameObject go = new GameObject("Lives");
            go.AddComponent<VisualLives>();
            HorizontalLayoutGroup layout = GoUtility.GetSafeComponent<HorizontalLayoutGroup>(go);
            layout.childControlWidth = false;
            layout.childForceExpandWidth = false;
            CreateCustomGameObject(menuCommand, go);
        }
        
        [MenuItem("GameObject/Framework/Score", false, 10)]
        public static void CreateScore(MenuCommand menuCommand)
        {
            GameObject go = new GameObject("Score");
            go.AddComponent<ScoreTracker>();
            CreateCustomGameObject(menuCommand, go);
        }

        [MenuItem("GameObject/Framework/Countdown", false, 10)]
        public static void CreateCountdown(MenuCommand menuCommand)
        {
            GameObject go = new GameObject("Countdown");
            go.AddComponent<Countdown>();
            ContentSizeFitter fitter = go.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            CreateCustomGameObject(menuCommand, go);
        }

        private static void CreateCustomGameObject(MenuCommand menuCommand, GameObject go)
        {
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}