using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RevistasUI : MonoBehaviour
{
    [Header("Panel general")]
    public GameObject panelRevista;

    [Header("Texto")]
    public TMP_Text textoRevista;

    private int revistaActual = 0;

    private string[] revistas = new string[]
    {
        "REVISTA 1 — SALUD MODERNA (SEGÚN QUIÉN)\n\n“Nuevo tratamiento promete curar el estrés usando solo aire exhalado por médicos certificados.”\nEn un estudio con 12 participantes, el 83% reportó sentir “algo”.\nNadie sabe qué era.\nEl hospital asegura que los resultados “son prometedores, pero podrían ser efecto del olor a café quemado”.",

        "REVISTA 2 — CIENCIA CLÍNICA HOY… O AYER\n\n“Detectan paciente con pulso que suena a metrónomo.”\nLos especialistas no se ponen de acuerdo: algunos creen que es ansiedad rítmica, otros que el paciente “está marcando el compás para su propia banda sonora personal”.\nSe recomienda no escucharlo de cerca.",

        "REVISTA 3 — TERAPIAS ALTERNATIVAS EXTREMAS\n\n“Nueva terapia de risas involuntarias aprobada para uso experimental.”\nIncluye casco con bocinas, grabaciones de carcajadas y un payaso certificado.\nEfectos secundarios: risa incómoda, ataques de sinceridad y pensamientos color naranja.",

        "REVISTA 4 — NOTICIAS MÉDICAS QUE NO PEDISTE\n\n“Encuentran pasillo del hospital que solo aparece cuando nadie lo está mirando.”\nRecomiendan no buscarlo.",

        "REVISTA 5 — BOLETÍN DE SALUD DEL DÍA\n\n“Sala de espera circular que nunca te deja llegar a la ventanilla.”",

        "REVISTA 6 — EL RINCÓN DEL DOCTOR B.\n\n“La importancia de firmar documentos sin leerlos.”",

        "REVISTA 7 — INVESTIGACIÓN MÉDICA IMPRECISA\n\n“Paciente asegura que una camilla le guiñó el ojo.”",

        "REVISTA 8 — SALUD PARA TODOS\n\n“Pensar demasiado puede provocar ecos en pasillos cerrados.”",
    };

   
    public void AbrirRevista()
    {
        panelRevista.SetActive(true);   // Mostrar panel
        revistaActual = 0;              // Empezar por la primera
        ActualizarRevista();            // Mostrar el texto
    }

    public void CerrarRevista()
    {
        panelRevista.SetActive(false);
    }

    public void SiguienteRevista()
    {
        revistaActual++;
        if (revistaActual >= revistas.Length)
            revistaActual = 0;

        ActualizarRevista();
    }

    public void AnteriorRevista()
    {
        revistaActual--;
        if (revistaActual < 0)
            revistaActual = revistas.Length - 1;

        ActualizarRevista();
    }

    private void ActualizarRevista()
    {
        if (textoRevista != null)
            textoRevista.text = revistas[revistaActual];
        else
            Debug.LogWarning("No hay referencia al Text del panel de revista.");
    }
}
