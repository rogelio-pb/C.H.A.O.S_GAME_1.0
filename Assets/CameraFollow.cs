using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El personaje que seguirá la cámara
    public float smoothSpeed = 0.125f; // Qué tan suave se mueve la cámara
    public Vector3 offset; // Distancia de la cámara respecto al personaje

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

