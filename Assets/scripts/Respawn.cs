using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public string playerTag = "Player";
    public Transform player;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag(playerTag);
            if (p != null) player = p.transform;
        }

        ApplyRespawn();
    }

    void ApplyRespawn()
    {
        // No hay checkpoint guardado  no respawn
        if (!PlayerPrefs.HasKey("checkpoint_scene")) return;

        int checkpointScene = PlayerPrefs.GetInt("checkpoint_scene");
        int lastScene = PlayerPrefs.GetInt("last_scene", -1);
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        // Primera vez en el juego  NO respawn
        if (lastScene == -1) return;

        // Si recarga la misma escena  NO respawn
        if (lastScene == currentScene) return;

        // Solo respawn si el checkpoint pertenece a esta escena
        if (currentScene != checkpointScene) return;

        // Aplicar respawn
        float x = PlayerPrefs.GetFloat("checkpoint_x");
        float y = PlayerPrefs.GetFloat("checkpoint_y");

        player.position = new Vector3(x, y, player.position.z);

        Debug.Log("RESPAWN aplicado: " + player.position);
    }
}
