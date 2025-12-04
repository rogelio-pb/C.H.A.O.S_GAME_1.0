using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecetaUI : MonoBehaviour
{
    [Header("Panel general")]
    public GameObject panelReceta;

    [Header("Texto")]
    public TMP_Text textoReceta;

    [Header("Scroll")]
    public ScrollRect scrollRect;

    private int recetaActual = 0;

    private string[] recetas = new string[]
    {
        "RECETA\n" +
        "Paciente: —\n" +
        "Fecha: ilegible\n" +
        "Médico tratante: firma incompleta\n\n" +
        "Medicamento recetado:\n" +
        "Clarividol®\n" +
        "(Regulador transitorio de percepción clínica)\n\n" +
        "Dosis:\n" +
        "Según percepción del síntoma.\n" +
        "Si no percibe síntomas, iniciar dosis preventiva igualmente.\n\n" +
        "Vía de administración:\n" +
        "Oral o mental, indistinto.\n\n" +
        "Frecuencia:\n" +
        "Cada vez que dude.\n\n" +
        "Recomendaciones:\n" +
        "– No leer el diagnóstico en voz alta.\n" +
        "– Evitar espejos durante el tratamiento.\n" +
        "– Si el entorno cambia, anotar el cambio, no corregirlo.\n" +
        "– Continuar el tratamiento incluso si comienza a sentirse “bien”.\n\n" +
        "Efectos secundarios comunes:\n" +
        "– Incomodidad leve\n" +
        "– Sensación de estar siendo observado\n" +
        "– Aumento moderado de la autoconciencia\n\n" +
        "Efectos secundarios poco comunes:\n" +
        "– Recuerdos que no pertenecen al paciente\n" +
        "– Dificultad para distinguir instrucciones de pensamientos\n" +
        "– Familiaridad con lugares visitados por primera vez\n\n" +
        "Suspender el tratamiento si:\n" +
        "el paciente empieza a preguntarse quién redactó esta receta.\n\n" +
        "Nota manuscrita al margen:\n" +
        "“No importa si no está enfermo. La receta funciona igual.”"
    };

    // ABRIR PANEL
    public void AbrirReceta()
    {
        panelReceta.SetActive(true);
        recetaActual = 0;

        // Asigna texto completo
        textoReceta.text = recetas[recetaActual];

        // Asegurar que el scroll empiece ARRIBA
        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    // CERRAR PANEL
    public void CerrarReceta()
    {
        panelReceta.SetActive(false);
    }
}
