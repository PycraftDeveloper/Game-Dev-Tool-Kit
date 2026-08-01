using UnityEngine;

public class FingerIK : MonoBehaviour
{
    [Header("Joint Hierarchy (Root -> Tip)")]
    [Tooltip("Requires exactly 3 joints: Proximal, Intermediate, Distal.")]
    public Transform[] joints;

    [Header("Targets")]
    public Transform tipTarget;

    [Header("Local Rotational Axes")]
    [Tooltip("The axis the finger curls around (usually Right or Left).")]
    public Vector3 bendAxis = Vector3.right;

    [Tooltip("The axis the knuckle splay moves around (usually Up or Down).")]
    public Vector3 splayAxis = Vector3.up;

    [Header("Joint Limits (Degrees)")]
    public float proximalCurlMax = 85f;

    public float intermediateCurlMax = 100f;
    public float distalCurlMax = 75f;

    public float proximalSplayMax = 15f;

    [Header("Smoothing")]
    [Tooltip("Higher values snap faster. Lower values eliminate flickers and add lag.")]
    public float smoothSpeed = 15f;

    private Quaternion[] bindRotations;
    private Quaternion[] currentTargetRotations;

    private void Start()
    {
        if (joints == null || joints.Length < 3)
        {
            Debug.LogError("FingerIK requires exactly 3 joints assigned in the array.");
            enabled = false;
            return;
        }

        bindRotations = new Quaternion[joints.Length];
        currentTargetRotations = new Quaternion[joints.Length];

        for (int i = 0; i < joints.Length; i++)
        {
            bindRotations[i] = joints[i].localRotation;
            currentTargetRotations[i] = joints[i].localRotation;
        }
    }

    private void LateUpdate()
    {
        if (tipTarget == null) return;

        SolveFinger();
        ApplySmoothedRotations();
    }

    private void SolveFinger()
    {
        Vector3 targetWorldPos = tipTarget.position;
        Vector3 localTargetPos;

        if (joints[0].parent != null)
        {
            Vector3 parentLocalPos = joints[0].parent.InverseTransformPoint(targetWorldPos);
            localTargetPos = parentLocalPos - joints[0].localPosition;
        }
        else
        {
            localTargetPos = targetWorldPos - joints[0].position;
        }

        Vector3 splayProjected = Vector3.ProjectOnPlane(localTargetPos, bendAxis);

        if (splayProjected.sqrMagnitude < 0.001f) splayProjected = Vector3.forward;

        float splayAngle = Vector3.SignedAngle(Vector3.forward, splayProjected.normalized, splayAxis);
        splayAngle = Mathf.Clamp(splayAngle, -proximalSplayMax, proximalSplayMax);
        Quaternion splayRot = Quaternion.AngleAxis(splayAngle, splayAxis);

        Vector3 splayedTargetPos = Quaternion.Inverse(splayRot) * localTargetPos;
        Vector3 curlProjected = Vector3.ProjectOnPlane(splayedTargetPos, splayAxis);

        if (curlProjected.sqrMagnitude < 0.001f) curlProjected = Vector3.forward;

        float targetCurlAngle = Vector3.SignedAngle(Vector3.forward, curlProjected.normalized, bendAxis);

        float curlFactor = Mathf.Clamp01(targetCurlAngle / proximalCurlMax);

        float proximalCurl = curlFactor * proximalCurlMax;
        float intermediateCurl = curlFactor * intermediateCurlMax;
        float distalCurl = curlFactor * distalCurlMax;

        currentTargetRotations[0] = bindRotations[0] * splayRot * Quaternion.AngleAxis(proximalCurl, bendAxis);
        currentTargetRotations[1] = bindRotations[1] * Quaternion.AngleAxis(intermediateCurl, bendAxis);
        currentTargetRotations[2] = bindRotations[2] * Quaternion.AngleAxis(distalCurl, bendAxis);
    }

    private void ApplySmoothedRotations()
    {
        float t = Time.deltaTime * smoothSpeed;

        for (int i = 0; i < joints.Length; i++)
        {
            joints[i].localRotation = Quaternion.Slerp(joints[i].localRotation, currentTargetRotations[i], t);
        }
    }
}