using UnityEngine;
using UnityEngine.UI;

public class CreditsAutoScroll : MonoBehaviour
{
    [Header("Scroll")]
    public ScrollRect scrollRect;
    public float scrollSpeed = 20f;

    [Header("Paneles")]
    public GameObject panelCreditos;       // Este panel donde están los créditos
    public GameObject panelMenuPrincipal;  // Tu panel al que debe regresar

    private bool scrolling = false;   // Ya no empieza automáticamente

    void OnEnable()
    {
        // Cada vez que se ACTIVE el panel de créditos, reinicia
        ReiniciarCreditos();
    }

    void Update()
    {
        if (!scrolling) return;

        // Baja el contenido hacia ABAJO
        scrollRect.verticalNormalizedPosition -= (scrollSpeed * Time.deltaTime) / 100f;

        // Cuando llega al fondo:
        if (scrollRect.verticalNormalizedPosition <= 0f)
        {
            scrollRect.verticalNormalizedPosition = 0f;
            scrolling = false;

            // Volver al menú automáticamente
            VolverAlMenu();
        }
    }

    public void ReiniciarCreditos()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();

        // Comenzar desde ARRIBA
        scrollRect.verticalNormalizedPosition = 1f;

        // Activar el scroll
        scrolling = true;
    }

    private void VolverAlMenu()
    {
        panelCreditos.SetActive(false);
        panelMenuPrincipal.SetActive(true);
    }
}
