using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EscritorioRorschach2D : MonoBehaviour
{
    [Header("Minijuego 2 (Rorschach)")]
    public RorschachMiniGame miniGame;    // arrastra aquí tu RorschachMiniGameController

    [Header("Detección de Player")]
    public Transform player;              // arrastra el transform de Argelino
    public string playerTag = "Player";
    public float distanciaMaximaUso = 3f;

    [Header("Brillo del escritorio")]
    [Tooltip("Si lo dejas vacío, buscará todos los SpriteRenderer en este objeto y sus hijos.")]
    public SpriteRenderer[] highlightSprites;

    public Color highlightColor = Color.cyan;
    [Range(0.5f, 3f)]
    public float highlightIntensity = 1.4f;

    private bool _playerEnRango = false;
    private Color[] _originalColors;

    private void Awake()
    {
        // El collider 2D debe ser trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        // Detectar sprites automáticamente si no se asignan
        if (highlightSprites == null || highlightSprites.Length == 0)
            highlightSprites = GetComponentsInChildren<SpriteRenderer>();

        _originalColors = new Color[highlightSprites.Length];
        for (int i = 0; i < highlightSprites.Length; i++)
        {
            _originalColors[i] = highlightSprites[i].color;
        }

        ApagarBrillo();
    }

    // CLICK / TAP SOBRE EL ESCRITORIO
    private void OnMouseDown()
    {
        if (TapBlocker.IsTouchOverUI())
            return;
        if (miniGame == null)
        {
            Debug.LogWarning("[EscritorioRorschach2D] Falta asignar miniGame.");
            return;
        }

        // Si ya está corriendo el minijuego, no volver a abrir
        if (miniGame.IsRunning)
            return;

        // Checar distancia al player
        if (player != null)
        {
            float dist = Vector2.Distance(player.position, transform.position);
            if (dist > distanciaMaximaUso)
            {
                Debug.Log("[EscritorioRorschach2D] Player muy lejos para usar el escritorio. Dist = " + dist);
                return;
            }
        }

        Debug.Log("[EscritorioRorschach2D] Click/Tap en escritorio -> StartMiniGame()");
        miniGame.StartMiniGame();
    }

    // TRIGGER 2D PARA BRILLO

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        _playerEnRango = true;
        EncenderBrillo();
        Debug.Log("[EscritorioRorschach2D] Player entro en rango.");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        _playerEnRango = false;
        ApagarBrillo();
        Debug.Log("[EscritorioRorschach2D] Player salio de rango.");
    }

    private void EncenderBrillo()
    {
        if (highlightSprites == null) return;

        for (int i = 0; i < highlightSprites.Length; i++)
        {
            if (highlightSprites[i] == null) continue;

            Color baseColor = _originalColors[i];
            Color finalColor = baseColor * highlightIntensity;
            finalColor.a = baseColor.a; // conservar transparencia

            highlightSprites[i].color = finalColor;
        }
    }

    private void ApagarBrillo()
    {
        if (highlightSprites == null) return;

        for (int i = 0; i < highlightSprites.Length; i++)
        {
            if (highlightSprites[i] == null) continue;
            highlightSprites[i].color = _originalColors[i];
        }
    }
}
