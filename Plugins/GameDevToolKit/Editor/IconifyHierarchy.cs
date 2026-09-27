using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

[InitializeOnLoad]
public class IconifyHierarchy
{
    static IconifyHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.EntityIdToObject(instanceID) as GameObject;
        if (obj == null) return;

        GUIContent nativeContent = EditorGUIUtility.ObjectContent(obj, typeof(GameObject));
        if (nativeContent == null || nativeContent.image == null) return;

        string textureName = nativeContent.image.name;

        bool isLargeTag = textureName.Contains("sv_label");
        bool isSmallIcon = textureName.Contains("sv_icon_dot");

        if (isLargeTag || isSmallIcon)
        {
            int targetTextureIndex = 0;

            if (isLargeTag)
            {
                targetTextureIndex = ExtractNumberFromString(textureName, 0);
            }
            else if (isSmallIcon)
            {
                targetTextureIndex = ExtractNumberFromString(textureName, 0);
            }

            Color backgroundColor = EditorGUIUtility.isProSkin ? new Color(0.22f, 0.22f, 0.22f, 1f) : new Color(0.76f, 0.76f, 0.76f, 1f);
            if (Selection.activeGameObject == obj)
            {
                backgroundColor = EditorGUIUtility.isProSkin ? new Color(0.17f, 0.36f, 0.53f, 1f) : new Color(0.22f, 0.44f, 0.67f, 1f);
            }
            EditorGUI.DrawRect(selectionRect, backgroundColor);

            Texture2D finalShapeIcon = EditorGUIUtility.FindTexture($"sv_icon_dot{targetTextureIndex}_pix16_gizmo");

            if (finalShapeIcon == null)
            {
                finalShapeIcon = EditorGUIUtility.FindTexture($"sv_icon_dot{targetTextureIndex}_sml");
            }

            Rect paddedRect = new Rect(selectionRect.x + 2, selectionRect.y + 3, 10, 10);
            if (finalShapeIcon != null)
            {
                GUI.DrawTexture(paddedRect, finalShapeIcon, ScaleMode.ScaleToFit);
            }

            Rect textRect = new Rect(selectionRect.x + 16, selectionRect.y, selectionRect.width - 16, selectionRect.height);

            GUIStyle customStyle = new GUIStyle(EditorStyles.label);
            customStyle.fontStyle = FontStyle.Normal;
            customStyle.alignment = TextAnchor.MiddleLeft;

            if (Selection.activeGameObject == obj)
            {
                customStyle.normal.textColor = Color.white;
            }

            string cleanName = obj.name.StartsWith("!") ? obj.name.Substring(1) : obj.name;
            EditorGUI.LabelField(textRect, cleanName, customStyle);
        }
    }

    private static int ExtractNumberFromString(string text, int fallback)
    {
        Match match = Regex.Match(text, @"\d+");
        if (match.Success && int.TryParse(match.Value, out int result))
        {
            return result;
        }
        return fallback;
    }
}