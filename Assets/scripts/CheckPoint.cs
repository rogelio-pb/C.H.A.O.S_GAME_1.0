using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [Header("Detección por toque/click")]
    public float distanciaMaximaTouch = 2.5f;   // distancia máxima permitida
    public Transform player;                    // arrastra aquí al player
    public CheckpointNotification notificador;  // arrastra aquí el notificador de checkpoint
    private void OnMouseDown()
    {
        if (player == null)
        {
            Debug.LogError("No se asignó el player al checkpoint");
            return;
        }

        // Revisar distancia para evitar activación desde lejos
        float distancia = Vector2.Distance(player.position, transform.position);
        if (distancia > distanciaMaximaTouch)
        {
            Debug.Log("Estás muy lejos para activar el checkpoint.");
            return;
        }

        ActivarCheckpoint();
    }

    void ActivarCheckpoint()
    {
        // Guarda la posición del jugador
        PlayerPrefs.SetFloat("respawn_x", player.position.x);
        PlayerPrefs.SetFloat("respawn_y", player.position.y);

        // Guarda la escena donde se activó
        PlayerPrefs.SetInt("respawn_scene", SceneManager.GetActiveScene().buildIndex);

        // Marca que SÍ hay un checkpoint
        PlayerPrefs.SetInt("tiene_checkpoint", 1);

        PlayerPrefs.Save();

        Debug.Log("Checkpoint ACTIVADO manualmente por toque.");

        if (notificador != null)
            notificador.MostrarNotificacion();
        
    }
}
