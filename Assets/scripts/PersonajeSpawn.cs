using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    public Vector2 posicionInicial;   // posición donde debe aparecer si NO hay checkpoint

    void Start()
    {
        // ¿Hay un checkpoint guardado?
        if (PlayerPrefs.GetInt("tiene_checkpoint", 0) == 1)
        {
            int escenaGuardada = PlayerPrefs.GetInt("respawn_scene");

            // Si estamos en la misma escena donde está el checkpoint
            if (escenaGuardada == SceneManager.GetActiveScene().buildIndex)
            {
                float x = PlayerPrefs.GetFloat("respawn_x");
                float y = PlayerPrefs.GetFloat("respawn_y");

                transform.position = new Vector2(x, y);
                return;
            }
        }

        // Si no hay checkpoint o estamos en otra escena  aparece en el inicio
        transform.position = posicionInicial;
    }

    // Función para reaparecer cuando "muere"
    public void Morir()
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.buildIndex);  // recargar escena
    }
}
