using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class FlickerLuz_ParanoiaCompat : MonoBehaviour
{
    [Header("Paranoia (compatibilidad con ParanoiaTV2D)")]
    [Tooltip("Umbral en % (usa la misma lógica que ParanoiaTV2D.paranoiaThreshold)")]
    public float paranoiaThreshold = 15f;   // por defecto igual que ParanoiaTV2D

    [Header("Sprite de la luz")]
    public SpriteRenderer luzSprite;        // tu sprite único (si no se asigna se buscará en el GameObject)

    [Header("Parpadeo")]
    public float tiempoMin = 0.05f;
    public float tiempoMax = 0.25f;

    [Header("Alpha")]
    [Range(0f, 1f)] public float alphaEncendida = 1f;
    [Range(0f, 1f)] public float alphaApagada = 0f;

    // si ParanoiaManager no existe, podemos usar esta variable pública para debug
    [Header("Debug / override (opcional)")]
    public bool usarOverrideDeParanoia = false;
    [Range(0f, 100f)] public float overrideParanoiaPercent = 0f;

    bool parpadeando = false;

    void Start()
    {
        if (luzSprite == null)
            luzSprite = GetComponent<SpriteRenderer>();

        // Asegurarnos de que la luz empieza invisible/apagada
        SetAlpha(alphaApagada);
    }

    void Update()
    {
        float paranoia = GetParanoiaPercent();

        if (paranoia >= paranoiaThreshold)
        {
            if (!parpadeando)
            {
                parpadeando = true;
                StartCoroutine(FlickerRoutine());
            }
        }
        else
        {
            if (parpadeando)
            {
                parpadeando = false;
                StopAllCoroutines();
            }
            SetAlpha(alphaApagada);
        }
    }

    IEnumerator FlickerRoutine()
    {
        while (parpadeando)
        {
            // Encendido
            SetAlpha(alphaEncendida);
            yield return new WaitForSeconds(Random.Range(tiempoMin, tiempoMax));

            // Apagado
            SetAlpha(alphaApagada);
            yield return new WaitForSeconds(Random.Range(tiempoMin, tiempoMax));
        }
    }

    void SetAlpha(float a)
    {
        if (luzSprite == null) return;
        Color c = luzSprite.color;
        c.a = a;
        luzSprite.color = c;
    }

    float GetParanoiaPercent()
    {
        // 1) si se forzó el override en inspector (útil para probar)
        if (usarOverrideDeParanoia)
            return overrideParanoiaPercent;

        // 2) intentar leer ParanoiaManager.Instance.currentParanoiaPercent (misma variable que usas en ParanoiaTV2D)
        try
        {
            if (ParanoiaManager.Instance != null)
            {
                // ADVERTENCIA: aquí asumimos que la variable `currentParanoiaPercent` existe
                // Si tu ParanoiaManager usa otro nombre (ej. paranoiaActual), cambialo abajo.
                return ParanoiaManager.Instance.currentParanoiaPercent;
            }
        }
        catch
        {
            // Si la propiedad no existe o lanza excepción, caemos al return 0
        }

        // 3) por defecto 0 si no hay manager
        return 0f;
    }
}
