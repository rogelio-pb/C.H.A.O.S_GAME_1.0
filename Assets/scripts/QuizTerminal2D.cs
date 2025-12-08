using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class QuizTerminal2D : MonoBehaviour
{
    [Header("Referencia al minijuego")]
    public QuizMiniGame quizMiniGame;

    [Header("Detección de Player")]
    public Transform player;          // Arrastra aquí el transform del jugador
    public string playerTag = "Player";
    public float distanciaMaximaUso = 3f;

    [Header("Brillo del objeto (2D)")]
    [Tooltip("Si está vacío, buscará todos los SpriteRenderer en este objeto y sus hijos.")]
    public SpriteRenderer[] targetSprites;

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

        // Si no asignas sprites, se buscan automáticamente
        if (targetSprites == null || targetSprites.Length == 0)
            targetSprites = GetComponentsInChildren<SpriteRenderer>();

        _originalColors = new Color[targetSprites.Length];
        for (int i = 0; i < targetSprites.Length; i++)
        {
            _originalColors[i] = targetSprites[i].color;
        }

        ApagarBrillo();
    }

    // CLICK / TAP SOBRE LA TERMINAL (PC y MÓVIL)
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (quizMiniGame == null)
        {
            Debug.LogWarning("[QuizTerminal2D] Falta asignar QuizMiniGame.");
            return;
        }

        // Si ya está corriendo el quiz, no volver a abrir
        if (quizMiniGame.IsRunning)
            return;

        // Checar distancia al player en 2D
        if (player != null)
        {
            float dist = Vector2.Distance(player.position, transform.position);
            if (dist > distanciaMaximaUso)
            {
                Debug.Log("[QuizTerminal2D] Player muy lejos para usar la terminal. Dist = " + dist);
                return;
            }
        }

        Debug.Log("[QuizTerminal2D] Click/Tap en terminal -> StartQuiz()");
        quizMiniGame.StartQuiz();
    }

    // TRIGGER 2D PARA EL BRILLO
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        _playerEnRango = true;
        EncenderBrillo();
        Debug.Log("[QuizTerminal2D] Player entro en rango.");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        _playerEnRango = false;
        ApagarBrillo();
        Debug.Log("[QuizTerminal2D] Player salio de rango.");
    }

    private void EncenderBrillo()
    {
        if (targetSprites == null) return;

        for (int i = 0; i < targetSprites.Length; i++)
        {
            if (targetSprites[i] == null) continue;

            // Subimos el color para que "brille" un poco
            Color baseColor = _originalColors[i];
            Color finalColor = baseColor * highlightIntensity;
            finalColor.a = baseColor.a; // mantener alpha

            targetSprites[i].color = finalColor;
        }
    }

    private void ApagarBrillo()
    {
        if (targetSprites == null) return;

        for (int i = 0; i < targetSprites.Length; i++)
        {
            if (targetSprites[i] == null) continue;
            targetSprites[i].color = _originalColors[i];
        }
    }
}
