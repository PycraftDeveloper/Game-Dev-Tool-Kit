using UnityEngine;
using UnityEditor;
using System;

namespace GameDevToolKit.Editor
{
    public class SaveScreenshotToFile
    {
        static private Camera SelectedCamera = null;

        [MenuItem("Game Dev Tool Kit/Render Camera to file")]
        public static void RenderCameraToFile()
        {
            RenderTexture rt = new RenderTexture(SelectedCamera.pixelWidth, SelectedCamera.pixelHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
            RenderTexture oldRT = SelectedCamera.targetTexture;
            SelectedCamera.targetTexture = rt;
            SelectedCamera.Render();
            SelectedCamera.targetTexture = oldRT;

            RenderTexture.active = rt;
            Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            RenderTexture.active = null;

            byte[] bytes = tex.EncodeToPNG();
            DateTime dt = DateTime.Now;
            string name = "Screenshot " + dt.ToString("yyyy-MM-dd HHmmss") + ".png";
            string path = EditorUtility.SaveFilePanelInProject("Save Screenshot", name, "png", "Please enter a file name to save the screenshot to");
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("Save cancelled");
                return;
            }
            System.IO.File.WriteAllBytes(path, bytes);
            AssetDatabase.ImportAsset(path);
            Debug.Log("Saved to " + path);
        }

        [MenuItem("Game Dev Tool Kit/Render Camera to file", true)]
        public static bool RenderCameraToFileValidation()
        {
            if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<Camera>() == null)
            {
                SelectedCamera = Camera.main;
            }
            else
            {
                SelectedCamera = Selection.activeGameObject.GetComponent<Camera>();
            }
            return SelectedCamera != null;
        }
    }
}