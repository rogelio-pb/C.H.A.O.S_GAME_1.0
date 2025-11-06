using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El personaje que seguir� la c�mara
    public float smoothSpeed = 0.125f; // Qu� tan suave se mueve la c�mara
    public Vector3 offset; // Distancia de la c�mara respecto al personaje

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