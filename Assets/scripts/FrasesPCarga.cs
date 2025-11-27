using UnityEngine;
using TMPro;
using System.Collections;

public class LoadingMessagesCycle : MonoBehaviour
{
    [Header("Referencia al texto en pantalla")]
    public TextMeshProUGUI textoLoading;

    [Header("Frases de carga")]
    [TextArea(2, 3)]
    public string[] frases;

    [Header("Tiempo entre mensajes")]
    public float tiempoCambio = 1.5f;

    private bool corriendo = true;

    void OnEnable()
    {
        // Cuando el panel se activa, empieza la rotación
        corriendo = true;
        StartCoroutine(CambiarFrases());
    }

    void OnDisable()
    {
        // Si el panel se oculta, detiene la rotación
        corriendo = false;
    }

    IEnumerator CambiarFrases()
    {
        while (corriendo)
        {
            if (frases.Length > 0)
            {
                int index = Random.Range(0, frases.Length);
                textoLoading.text = frases[index];
            }

            yield return new WaitForSeconds(tiempoCambio);
        }
    }
}
