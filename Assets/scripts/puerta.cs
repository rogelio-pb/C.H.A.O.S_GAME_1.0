using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaBloqueadora : MonoBehaviour
{
    [Header("Llaves necesarias para abrir")]
    public int llavesNecesarias = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica que sea el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Data.llave >= llavesNecesarias && collision.gameObject.CompareTag("Player"))
            {
            Debug.Log("¡Puerta abierta!");
                    Destroy(gameObject); // Destruye la puerta(por si ocupo para otra parte del codigo :D )
             Debug.Log("¡Has salido del nivel!");
                
             int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex + 1); // Asegúrate de tener el nivel "NextLevel" en tus escenas

                Data.llave -= llavesNecesarias; // Resta las llaves usadas
            }
            else
            {
                Debug.Log("Necesitas " + llavesNecesarias + " llave(s) para abrir esta puerta.");
                // Opcional: puedes reproducir un sonido de "cerrado"
            }
        }
        }
    }

 