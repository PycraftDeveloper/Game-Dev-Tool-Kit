using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace GameDevToolKit.Editor
{
    public class AutoIncrementVersion : IPostprocessBuildWithReport
    {
        private const string MenuPath = "Game Dev Tool Kit/Options/Easy Version Control";

        // Use EditorPrefs so the setting persists when closing Unity or recompiling
        private static bool EasyVersionControl
        {
            get => EditorPrefs.GetBool("GameDevToolKit_EasyVersionControl", false);
            set => EditorPrefs.SetBool("GameDevToolKit_EasyVersionControl", value);
        }

        public int callbackOrder => 0;

        [MenuItem(MenuPath)]
        public static void ToggleAction()
        {
            // Toggle the internal state
            EasyVersionControl = !EasyVersionControl;

            // Force Unity's UI checkmark to update right away
            Menu.SetChecked(MenuPath, EasyVersionControl);

            // FIX: Force the Scene View to instantly redraw so nametags appear/disappear immediately
            SceneView.RepaintAll();
        }

        // FIX: The validation method must return the actual state so the checkmark stays sync'd on boot
        [MenuItem(MenuPath, true)]
        public static bool ToggleActionValidate()
        {
            Menu.SetChecked(MenuPath, EasyVersionControl);
            return true;
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            if (!EasyVersionControl) return;

            string version = PlayerSettings.bundleVersion;   // e.g. "1.4.2"
            Debug.Log("Current version: " + version);

            string newVersion = IncrementMinor(version);

            PlayerSettings.bundleVersion = newVersion;
            Debug.Log("Updated version: " + newVersion);

            // If you also want to increment Android versionCode or iOS build number:
            PlayerSettings.Android.bundleVersionCode++;
        }

        private string IncrementMinor(string version)
        {
            string[] parts = version.Split('.');

            // Ensure we have x.y.z
            while (parts.Length < 3)
            {
                System.Array.Resize(ref parts, 3);
                parts[parts.Length - 1] = "0";
            }

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            int z = int.Parse(parts[2]);

            y++;      // increment the middle number
            z = 0;    // reset patch version (optional but common)

            return $"{x}.{y}.{z}";
        }
    }
}