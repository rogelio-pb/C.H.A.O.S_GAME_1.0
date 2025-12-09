using UnityEngine;

public class FinalDemoController : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelFinalDemo;   // Imagen final que aparece primero
    public GameObject panelCreditos;    // Panel ScrollView de créditos

    [Header("Tiempo antes de mostrar créditos")]
    public float tiempoEspera = 3f;

    [Header("Control del Scroll de Créditos")]
    public CreditsAutoScroll creditsScroll; // tu script actual

    private void Start()
    {
        // Mostrar solo el panel final
        panelFinalDemo.SetActive(true);
        panelCreditos.SetActive(false);

        StartCoroutine(FlujoFinal());
    }

    private System.Collections.IEnumerator FlujoFinal()
    {
        // Espera unos segundos con la imagen final activa
        yield return new WaitForSeconds(tiempoEspera);

        // Mostrar créditos y ocultar pantalla final
        panelFinalDemo.SetActive(false);
        panelCreditos.SetActive(true);

        // Reiniciar el scroll para que inicie desde arriba
        if (creditsScroll != null)
            creditsScroll.ReiniciarCreditos();
    }
}
