using UnityEngine;
using UnityEditor;

namespace GameDevToolKit.Editor
{
    public class QuickLinks
    {
        [MenuItem("Game Dev Tool Kit/Folders/Open Assets Folder")]
        public static void OpenAssetsFolder()
        {
            // Define your absolute path
            string path = Application.dataPath;

            // This opens the directory natively on both Windows and macOS
            EditorUtility.RevealInFinder(path);
        }

        [MenuItem("Game Dev Tool Kit/Folders/Open Application Persistent Data Folder")]
        public static void OpenApplicationPersistantDataFolder()
        {
            // Define your absolute path
            string path = Application.persistentDataPath;

            // This opens the directory natively on both Windows and macOS
            EditorUtility.RevealInFinder(path);
        }
    }
}