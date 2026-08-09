using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    [Tooltip("The target aspect ratio to use. The default is 16:9.")]
    [SerializeField] private Vector2 TargetAspectRatio = new Vector2(16f, 9f);

    [Tooltip("The camera to fix the aspect ratio for. Can be left empty to use the default active camera.")]
    [SerializeField] private Camera TargetCamera;

    [Tooltip("Should the aspect ratio be enforced if the window changes size later?")]
    [SerializeField] private bool ContinuousEnforcement = true;

    private Vector2 lastScreenSize;

    private void Start()
    {
        Adjust();

        lastScreenSize = new Vector2(Screen.width, Screen.height);
    }

    private void Update()
    {
        if (ContinuousEnforcement)
        {
            if (Screen.width != lastScreenSize.x || Screen.height != lastScreenSize.y)
            {
                lastScreenSize = new Vector2(Screen.width, Screen.height);
                Adjust();
            }
        }
    }

    public void Adjust()
    {
        float targetAspect = TargetAspectRatio.x / TargetAspectRatio.y;

        Rect safe = Screen.safeArea;

        float windowAspect = safe.width / safe.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera cam;
        if (TargetCamera == null)
        {
            cam = Camera.main;
        }
        else
        {
            cam = TargetCamera;
        }

        if (scaleHeight < 1.0f)
        {
            float height = scaleHeight;
            float y = (1.0f - height) / 2.0f;

            cam.rect = new Rect(0, y, 1, height);
        }
        else
        {
            float width = 1.0f / scaleHeight;
            float x = (1.0f - width) / 2.0f;

            cam.rect = new Rect(x, 0, width, 1);
        }
    }
}