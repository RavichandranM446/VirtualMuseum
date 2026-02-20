using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;               // The Player
    public Vector3 offset = new Vector3(0, 2, -4);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Move and rotate camera
        transform.position = smoothedPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
