using UnityEditor;
using UnityEngine;

public class SceneNametags
{
    private const string MenuPath = "Game Dev Tool Kit/Show Name Tags";

    // Use EditorPrefs so the setting persists when closing Unity or recompiling
    private static bool ShowNameTags
    {
        get => EditorPrefs.GetBool("GameDevToolKit_ShowNameTags", false);
        set => EditorPrefs.SetBool("GameDevToolKit_ShowNameTags", value);
    }

    [MenuItem(MenuPath)]
    public static void ToggleAction()
    {
        // Toggle the internal state
        ShowNameTags = !ShowNameTags;

        // Force Unity's UI checkmark to update right away
        Menu.SetChecked(MenuPath, ShowNameTags);

        // FIX: Force the Scene View to instantly redraw so nametags appear/disappear immediately
        SceneView.RepaintAll();
    }

    // FIX: The validation method must return the actual state so the checkmark stays sync'd on boot
    [MenuItem(MenuPath, true)]
    public static bool ToggleActionValidate()
    {
        Menu.SetChecked(MenuPath, ShowNameTags);
        return true;
    }

    [DrawGizmo(GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
    public static void DrawGlobalNametagWhenClose(Transform transform, GizmoType gizmoType)
    {
        if (!ShowNameTags) return;

        // Filter out prefab children so we don't clutter the scene
        bool isPrefabInstance = PrefabUtility.IsPartOfPrefabInstance(transform.gameObject);
        bool isChildOfPrefab = isPrefabInstance && !PrefabUtility.IsOutermostPrefabInstanceRoot(transform.gameObject);
        if (isChildOfPrefab) return;

        Vector3 labelPosition = GetLabelPosition(transform);

        if (Camera.current != null)
        {
            Vector3 currentCameraPosition = Camera.current.transform.position;
            // Short-range check to prevent absolute visual clutter
            if ((currentCameraPosition - labelPosition).sqrMagnitude < 225.0f) // 15^2 is faster than .magnitude
            {
                Handles.Label(labelPosition, transform.gameObject.name);
            }
        }
    }

    [DrawGizmo(GizmoType.Selected)]
    public static void DrawGlobalNametagWhenSelected(Transform transform, GizmoType gizmoType)
    {
        if (!ShowNameTags) return;

        Vector3 labelPosition = GetLabelPosition(transform);
        Handles.Label(labelPosition, transform.gameObject.name);
    }

    // Helper method to keep your position calculation DRY (Don't Repeat Yourself)
    private static Vector3 GetLabelPosition(Transform transform)
    {
        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer != null)
        {
            return new Vector3(transform.position.x, renderer.bounds.max.y + 0.2f, transform.position.z);
        }
        return transform.position + Vector3.up * 0.5f;
    }
}