using UnityEngine;

public class CerrarPanelUI : MonoBehaviour
{
    [Header("Panel a cerrar")]
    public GameObject panel;

    [Header("Opcional: bloquear al jugador mientras el panel está abierto")]
    public bool reactivarJugador = true;

    public GameObject jugador;

    public void CerrarPanel()
    {
        if (panel != null)
            panel.SetActive(false);

        if (reactivarJugador && jugador != null)
            jugador.SetActive(true);

        Debug.Log("Panel cerrado: " + panel.name);

    }
}
