using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnJugador : MonoBehaviour
{
    private void Start()
    {
        // Solo respawnear si vienes de otra escena
        if (SceneManager.GetActiveScene().buildIndex == 0 && PlayerPrefs.GetInt("viene_de_otra_escena", 0) == 1)
        {
            if (PlayerPrefs.GetInt("checkpoint_activado", 0) == 1)
            {
                float x = PlayerPrefs.GetFloat("respawn_x");
                float y = PlayerPrefs.GetFloat("respawn_y");

                transform.position = new Vector3(x, transform.position.z);

                Debug.Log("RESPAWN EN CHECKPOINT");
            }
            else
            {
                Debug.Log("NO HAY CHECKPOINT — inicia desde el principio");
            }
        }

        // Ya estás en la escena del nivel, reset
        PlayerPrefs.SetInt("viene_de_otra_escena", 0);
        PlayerPrefs.Save();
    }
}
