using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameDevToolKit.Editor
{
    /// <summary>
    /// Adds a "Create Variant for Renderer" button to Material inspectors.
    ///
    /// The button is available for any saved Material asset.
    /// If the currently selected GameObject has a Renderer using the Material,
    /// the newly-created variant is automatically assigned to that Renderer.
    ///
    /// If there is no suitable Renderer, the variant is still created but is
    /// not automatically assigned.
    /// </summary>
    [InitializeOnLoad]
    public static class MaterialVariantForRenderer
    {
        private const string ButtonLabel = "Create Variant for Renderer";

        static MaterialVariantForRenderer()
        {
            UnityEditor.Editor.finishedDefaultHeaderGUI += OnMaterialInspectorHeader;
        }

        private static void OnMaterialInspectorHeader(UnityEditor.Editor editor)
        {
            if (editor == null || editor.target == null)
                return;

            // Only add this to Material inspectors.
            if (!(editor.target is Material material))
                return;

            // Don't try to handle multi-selection.
            if (editor.targets.Length != 1)
            {
                EditorGUILayout.HelpBox(
                    "Create Variant requires a single Material.",
                    MessageType.Info);

                return;
            }

            Renderer renderer = FindSelectedRenderer();

            bool rendererUsesMaterial =
                renderer != null &&
                UsesMaterial(renderer, material);

            EditorGUILayout.Space(3);

            // The button is ALWAYS enabled for a valid Material.
            if (GUILayout.Button(
                    ButtonLabel,
                    GUILayout.Height(EditorGUIUtility.singleLineHeight + 6)))
            {
                CreateVariant(material, renderer, rendererUsesMaterial);
            }

            // Give the user useful information about what will happen.
            if (rendererUsesMaterial)
            {
                EditorGUILayout.LabelField(
                    $"Will assign to: {renderer.name}",
                    EditorStyles.miniLabel);
            }
            else if (renderer != null)
            {
                EditorGUILayout.LabelField(
                    $"Selected Renderer '{renderer.name}' does not use this Material.",
                    EditorStyles.miniLabel);
            }
            else
            {
                EditorGUILayout.LabelField(
                    "No Renderer selected — variant will be created without assignment.",
                    EditorStyles.miniLabel);
            }

            EditorGUILayout.Space(3);
        }

        /// <summary>
        /// Finds a Renderer from the current selection.
        ///
        /// Checks:
        /// 1. The selected GameObject.
        /// 2. Children of the selected GameObject.
        /// </summary>
        private static Renderer FindSelectedRenderer()
        {
            GameObject selected = Selection.activeGameObject;

            if (selected == null)
                return null;

            // Renderer directly on selected object.
            Renderer renderer = selected.GetComponent<Renderer>();

            if (renderer != null)
                return renderer;

            // Renderer somewhere underneath it.
            return selected.GetComponentInChildren<Renderer>();
        }

        /// <summary>
        /// Determines whether the Renderer uses this exact Material.
        /// </summary>
        private static bool UsesMaterial(
            Renderer renderer,
            Material material)
        {
            if (renderer == null || material == null)
                return false;

            Material[] materials = renderer.sharedMaterials;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] == material)
                    return true;
            }

            return false;
        }

        private static void CreateVariant(
            Material sourceMaterial,
            Renderer renderer,
            bool rendererUsesMaterial)
        {
            if (sourceMaterial == null)
                return;

            // ------------------------------------------------------------
            // Validate that the Material is actually an asset.
            // ------------------------------------------------------------

            string sourcePath =
                AssetDatabase.GetAssetPath(sourceMaterial);

            if (string.IsNullOrEmpty(sourcePath))
            {
                EditorUtility.DisplayDialog(
                    ButtonLabel,
                    "The source Material must be a saved asset before a variant can be created.",
                    "OK");

                return;
            }

            // ------------------------------------------------------------
            // Determine where the variant should be saved.
            // ------------------------------------------------------------

            string sourceDirectory =
                Path.GetDirectoryName(sourcePath);

            if (string.IsNullOrEmpty(sourceDirectory))
                sourceDirectory = "Assets";

            // ------------------------------------------------------------
            // Generate a useful name.
            // ------------------------------------------------------------

            string variantName;

            if (rendererUsesMaterial && renderer != null)
            {
                variantName =
                    $"{sourceMaterial.name} - {renderer.name} Variant";
            }
            else
            {
                variantName =
                    $"{sourceMaterial.name} Variant";
            }

            string variantPath =
                Path.Combine(
                    sourceDirectory,
                    variantName + ".mat");

            // Make sure we never overwrite an existing asset.
            variantPath =
                AssetDatabase.GenerateUniqueAssetPath(variantPath);

            // ------------------------------------------------------------
            // Create the Material Variant.
            // ------------------------------------------------------------

            Material variant =
                new Material(sourceMaterial)
                {
                    name = Path.GetFileNameWithoutExtension(variantPath),
                    parent = sourceMaterial
                };

            // ------------------------------------------------------------
            // Save the Material Variant.
            // ------------------------------------------------------------

            AssetDatabase.CreateAsset(
                variant,
                variantPath);

            AssetDatabase.SaveAssets();

            // ------------------------------------------------------------
            // Assign the variant to the Renderer if appropriate.
            // ------------------------------------------------------------

            bool assignedToRenderer = false;

            if (rendererUsesMaterial && renderer != null)
            {
                Material[] materials =
                    renderer.sharedMaterials;

                int materialSlot = -1;

                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i] == sourceMaterial)
                    {
                        materialSlot = i;
                        break;
                    }
                }

                if (materialSlot >= 0)
                {
                    Undo.RecordObject(
                        renderer,
                        "Create Material Variant for Renderer");

                    materials[materialSlot] = variant;

                    renderer.sharedMaterials = materials;

                    PrefabUtility
                        .RecordPrefabInstancePropertyModifications(renderer);

                    EditorUtility.SetDirty(renderer);

                    assignedToRenderer = true;
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // ------------------------------------------------------------
            // Select the newly-created variant.
            // ------------------------------------------------------------

            Selection.activeObject = variant;

            EditorGUIUtility.PingObject(variant);

            // ------------------------------------------------------------
            // Report what happened.
            // ------------------------------------------------------------

            if (assignedToRenderer)
            {
                Debug.Log(
                    $"Created Material Variant '{variant.name}' " +
                    $"from '{sourceMaterial.name}' and assigned it to " +
                    $"Renderer '{renderer.name}'.");
            }
            else
            {
                Debug.Log(
                    $"Created Material Variant '{variant.name}' " +
                    $"from '{sourceMaterial.name}'. " +
                    $"It was not assigned to a Renderer.");
            }
        }
    }
}