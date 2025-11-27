using TMPro;
using UnityEngine;

public class DiarioSecretoUI : MonoBehaviour
{
    [Header("Panel general")]
    public GameObject panelDiario;   // Panel del diario

    [Header("Texto")]
    public TMP_Text textoDiario;     // Campo de texto

    [Header("Indice")]
    public TMP_Text textoIndice;


    private int paginaActual = 0;

    // --- TODAS LAS PÁGINAS DEL DIARIO ---
    private string[] paginas = new string[]
    {
        // Día 1
        "“CUADERNO DEL INTERNO N° 17”\nDía 1\n\n" +
        "Hoy inicié mi rotación en el ala de observación clínica. Me asignaron revisar a los pacientes con síntomas variables. " +
        "Todo parece rutinario: controles de signos, temperatura, estado de conciencia.\n\n" +
        "Sin embargo, hubo un caso que no encaja. Un individuo identificado solo como “A.” Llegó sin historial médico. " +
        "Su estatura registrada en admisión fue de 1.74 m, pero en mi medición aparece como 3 cm. " +
        "Creí que era un error en el sistema, pero la cinta de medición también lo indica.\n\n" +
        "Repetí el proceso: sigue midiendo 3 cm, pero ocupa la camilla normalmente. No entiendo.",

        // Día 2
        "Día 2\n\n" +
        "Intenté continuar con la evaluación del paciente “A.”, pero hoy presenta síntomas completamente distintos.\n\n" +
        "Ayer tenía temperatura normal; hoy, según el termómetro, está “ardiendo en vacío”. No es fiebre. " +
        "Es algo como… ausencia de calor.\n\n" +
        "Al observarlo directamente, siento que su silueta se distorsiona, como si no estuviera totalmente aquí.",

        // Día 3
        "Día 3\n\n" +
        "Los demás internos no parecen notar nada extraño. O eso dicen. Algunos evitan mirar a “A.” directamente.\n\n" +
        "Hoy escribí en la hoja clínica:\nDiagnóstico preliminar: Trastorno de eco persistente.\n" +
        "Es lo más cercano a lo que veo: cuando habla, sus palabras se repiten unos segundos después… " +
        "incluso cuando no lo escucho hablar.",

        // Día 4
        "Día 4\n\n" +
        "“A.” me preguntó hoy si yo “recordaba mi turno anterior”. No sé cómo responder a eso.\n\n" +
        "No sé por qué lo escribió en mi propio cuaderno. No sé cuándo lo escribió.\n\n" +
        "Revisé las cámaras del pasillo: “A.” no salió de su sala.\n" +
        "Pero mi letra no es así.",

        // Día 5
        "Día 5\n\n" +
        "Mientras actualizaba las notas, sentí que alguien estaba leyendo por encima de mi hombro.\n\n" +
        "Era “A.”, pero seguía acostado en la camilla. Lo vi dos veces al mismo tiempo, uno acostado y uno de pie. " +
        "Cuando parpadeé, desapareció el que me observaba.\n\n" +
        "Creo que está consciente de que este hospital no funciona bajo reglas normales.\n" +
        "Creo que sabe algo más.",

        // Día 6
        "Día 6\n\n" +
        "Hoy, durante la revisión, “A.” se incorporó sin moverse. Su cuerpo quedó quieto, pero su cabeza me siguió mientras caminaba.\n\n" +
        "Me preguntó:\n“¿Tú eliges escribir, o alguien te está escribiendo?”\n\n" +
        "No supe qué decir.\nCerraré este cuaderno por hoy.",

        // Última página rota
        "Día ???\n\n" +
        "“A. me miró como si yo no existiera.\n" +
        "Creo que él sabe que alguien más lo está viendo desde afuera...”"
    };


    // --- Actualizar índice ---
    private void ActualizarIndice()
    {
        if (textoIndice != null)
            textoIndice.text =  (paginaActual + 1) + " de " + paginas.Length;
    }

    // --- Abrir el diario ---
    public void AbrirDiario()
    {
        panelDiario.SetActive(true);
        paginaActual = 0;
        ActualizarPagina();
        ActualizarIndice();   
    }

    // --- Cerrar ---
    public void CerrarDiario()
    {
        panelDiario.SetActive(false);
    }

    // --- Siguiente ---
    public void SiguientePagina()
    {
        paginaActual++;
        if (paginaActual >= paginas.Length)
            paginaActual = paginas.Length - 1;

        ActualizarPagina();
        ActualizarIndice();   
    }

    // --- Anterior ---
    public void AnteriorPagina()
    {
        paginaActual--;
        if (paginaActual < 0)
            paginaActual = 0;

        ActualizarPagina();
        ActualizarIndice();   
    }

    // --- Actualizar texto ---
    private void ActualizarPagina()
    {
        if (textoDiario != null)
            textoDiario.text = paginas[paginaActual];

        ActualizarIndice();  
    }
}
