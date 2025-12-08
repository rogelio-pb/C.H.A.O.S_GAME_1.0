using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointNotification : MonoBehaviour
{
    public GameObject panelNotificacion;
    public float duracion = 3f;

    private void Start()
    {
        if (panelNotificacion != null)
            panelNotificacion.SetActive(false);
    }

    public void MostrarNotificacion()
    {
        StartCoroutine(NotificacionCoroutine());
    }

    private IEnumerator NotificacionCoroutine()
    {
        panelNotificacion.SetActive(true);

        yield return new WaitForSeconds(duracion);

        panelNotificacion.SetActive(false);
    }
}
