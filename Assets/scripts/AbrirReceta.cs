using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AbrirReceta : MonoBehaviour
{
    [Header("Panel de receta")]
    public RecetaUI RecetaUI;

    [Header("Detección del Player")]
    public string playerTag = "Player";
    public float distanciaMaximaTouch = 3f;
    public Transform player;

    [Header("Brillo del objeto (2D)")]
    public SpriteRenderer[] targetSprites;
    public float highlightIntensity = 1.4f;

    private Color[] originalColors;

    private void Awake()
    {
        // El collider debe ser trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        // Buscar sprites si no están asignados
        if (targetSprites == null || targetSprites.Length == 0)
            targetSprites = GetComponentsInChildren<SpriteRenderer>();

        originalColors = new Color[targetSprites.Length];
        for (int i = 0; i < targetSprites.Length; i++)
            originalColors[i] = targetSprites[i].color;

        ApagarBrillo();
    }

    private void OnMouseDown()
    {
        if (RecetaUI == null)
        {
            Debug.LogError("RecetaUI no está asignado.");
            return;
        }

        if (player != null)
        {
            float distancia = Vector2.Distance(player.position, transform.position);
            if (distancia > distanciaMaximaTouch)
            {
                Debug.Log("Player muy lejos para abrir receta.");
                return;
            }
        }

        RecetaUI.AbrirReceta();
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

    private void EncenderBrillo()
    {
        for (int i = 0; i < targetSprites.Length; i++)
        {
            if (targetSprites[i] == null) continue;

            Color c = originalColors[i] * highlightIntensity;
            c.a = originalColors[i].a;

            targetSprites[i].color = c;
        }
    }

    private void ApagarBrillo()
    {
        for (int i = 0; i < targetSprites.Length; i++)
        {
            if (targetSprites[i] == null) continue;

            targetSprites[i].color = originalColors[i];
        }
    }
}
