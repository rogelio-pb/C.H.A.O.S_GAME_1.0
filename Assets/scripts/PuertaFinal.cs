using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaFinal : MonoBehaviour
{
    [Header("Llaves necesarias")]
    public int llavesNecesarias = 2;

    [Header("Detección del Player")]
    public string playerTag = "Player";
    public float distanciaMaximaTouch = 3f;
    public Transform player;

    [Header("Mensaje al jugador")]
    public GameObject mensajeObj;
    public TMPro.TMP_Text mensajeTexto;
    public float tiempoMensaje = 2f;

    private bool puedeInteractuar = false;

    void Start()
    {
        if (mensajeObj != null)
            mensajeObj.SetActive(false);
    }

    // ----------------------
    // INTERACCIÓN CON CLICK
    // ----------------------
    private void OnMouseDown()
    {
        if (!puedeInteractuar) return;

        float distancia = Vector2.Distance(player.position, transform.position);
        if (distancia > distanciaMaximaTouch) return;

        RevisarLlaves();
    }

    // -----------------------------
    // VERIFICAR SI TIENE LAS LLAVES
    // -----------------------------
    void RevisarLlaves()
    {
        int llavesActuales = Data.llave;  // tu variable global

        if (llavesActuales >= llavesNecesarias)
        {
            AbrirPuerta();
        }
        else
        {
            MostrarMensaje("Se necesitan las llaves (" +
                llavesActuales + "/" + llavesNecesarias + ")");
        }
    }

    // -------------------------
    // SI YA TIENE LAS 2 LLAVES
    // -------------------------
    void AbrirPuerta()
    {
        Debug.Log("Abrir puerta FINAL – tiene todas las llaves.");

        // Guardar que vienes de otra escena
        PlayerPrefs.SetInt("viene_de_otra_escena", 1);
        PlayerPrefs.Save();

        // Aquí pones tu escena de carga
        SceneManager.LoadScene(5);
    }

    // -------------
    // MENSAJES UI
    // -------------
    void MostrarMensaje(string texto)
    {
        if (mensajeObj == null || mensajeTexto == null) return;

        mensajeObj.SetActive(true);
        mensajeTexto.text = texto;

        CancelInvoke(nameof(EsconderMensaje));
        Invoke(nameof(EsconderMensaje), tiempoMensaje);
    }

    void EsconderMensaje()
    {
        if (mensajeObj != null)
            mensajeObj.SetActive(false);
    }

    // -----------------------------------
    // ACTIVAR BRILLO / INTERACCIÓN
    // -----------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            puedeInteractuar = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            puedeInteractuar = false;
    }
}
