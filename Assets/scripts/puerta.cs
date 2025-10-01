using UnityEngine;

public class PuertaBloqueadora : MonoBehaviour
{
    [Header("Llaves necesarias para abrir")]
    public int llavesNecesarias = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica que sea el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Data.llave >= llavesNecesarias)
            {
                Debug.Log("¡Puerta abierta!");
                Destroy(gameObject); // Destruye la puerta y puede pasar
            }
            else
            {
                Debug.Log("Necesitas " + llavesNecesarias + " llave(s) para abrir esta puerta.");
                // Opcional: puedes reproducir un sonido de "cerrado"
            }
        }
    }
}
