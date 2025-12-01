using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target; // El personaje que seguir  la c mara

    private float target_X; // Desplazamiento horizontal entre la c mara y el personaje

    private float posx;

    public float derechaLimite; // L mite derecho del movimiento de la c mara
    public float izquierdaLimite; // L mite izquierdo del movimiento de la c mara

    public float speed; // Velocidad de seguimiento de la c mara
    public bool encendido = true; // Controla si la c mara sigue al personaje

    private void Awake()
    {
        posx = target_X + derechaLimite;
        transform.position = Vector3.Lerp(transform.position, new Vector3(posx, 0, -1), 1);
    }
    void Move_Cam()
    {
        if (encendido)
        {
            if (target)
            {
                target_X = target.transform.position.x;
                if (target_X >= derechaLimite && target_X < izquierdaLimite)
                {
                    posx = target_X;
                }
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(posx, 0, -1), speed * Time.deltaTime);


        }
    }
    void Update()
    {
        Move_Cam();
    }
}