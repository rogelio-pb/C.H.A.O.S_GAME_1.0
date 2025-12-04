using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteractiveObject2D : MonoBehaviour
{
    [Header("Configuración General")]
    public string playerTag = "Player";
    public Transform player;
    public float distanceMax = 3f; // distancia para permitir interacción

    [Header("Brillo / Highlight")]
    public SpriteRenderer[] sprites;
    public float highlightIntensity = 1.4f;

    [Header("Acción al interactuar (click/touch)")]
    public UnityEvent onInteract;

    private Color[] originalColors;

    private void Awake()
    {
        // El collider debe ser trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        // Buscar sprites si no están asignados
        if (sprites == null || sprites.Length == 0)
            sprites = GetComponentsInChildren<SpriteRenderer>();

        // Guardar colores originales
        originalColors = new Color[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
            originalColors[i] = sprites[i].color;

        ApagarBrillo();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            EncenderBrillo();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            ApagarBrillo();
    }

    private void OnMouseDown()
    {
        if (player != null)
        {
            float dist = Vector2.Distance(player.position, transform.position);
            if (dist > distanceMax)
            {
                Debug.Log("Player está demasiado lejos para interactuar.");
                return;
            }
        }

        onInteract?.Invoke();
    }

    private void EncenderBrillo()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i] == null) continue;

            Color c = originalColors[i] * highlightIntensity;
            c.a = originalColors[i].a;
            sprites[i].color = c;
        }
    }

    private void ApagarBrillo()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i] == null) continue;
            sprites[i].color = originalColors[i];
        }
    }
}
