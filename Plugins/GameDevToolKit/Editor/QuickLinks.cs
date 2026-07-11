using UnityEngine;
using UnityEditor;
using System;

namespace GameDevToolKit.Editor
{
    public class QuickLinks
    {
        [MenuItem("Game Dev Tool Kit/Open Assets Folder")]
        public static void OpenAssetsFolder()
        {
            // Define your absolute path
            string path = Application.dataPath;

            // This opens the directory natively on both Windows and macOS
            EditorUtility.RevealInFinder(path);
        }
    }
}