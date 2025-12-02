using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class RorschachLevelData
{
    public string levelName = "Nivel";
    public int rows = 3;
    public int cols = 3;

    [Tooltip("Sprites de las piezas en orden correcto (0..n-1). " +
             "Para 3x3 son 9 sprites, para 4x4 son 16, para 5x5 son 25.")]
    public Sprite[] piezas;
}

public class RorschachMiniGame : MonoBehaviour
{
    [Header("UI general")]
    public GameObject panel;                     // panel principal del minijuego
    public TextMeshProUGUI timerText;            // 06:00, 05:59, etc.
    public TextMeshProUGUI levelText;            // "Nivel 1/3"

    [Header("Grid de piezas")]
    public RectTransform gridParent;
    public GridLayoutGroup gridLayout;
    public RorschachPiece piecePrefab;

    [Header("Niveles")]
    public RorschachLevelData[] levels;          // 3 niveles: 3x3, 4x4, 5x5

    [Header("Paranoia")]
    public float paranoiaPorSubmitFallido = 2f;  // +2% por verificación fallida
    public float paranoiaPorDerrotaTiempo = 5f;  // penalización al perder por tiempo

    [Header("Tiempo global")]
    public float tiempoTotal = 360f;             // 6 minutos
    private float tiempoRestante;

    [Header("Feedback")]
    public GameObject greenCheck;                // palomita grande
    public GameObject redX;                      // tache grande

    [Header("Bloquear movimiento")]
    public ArgelinoController player;            // arrastra tu player

    public bool IsRunning { get; private set; } = false;

    // --- estado interno ---
    private int currentLevelIndex = 0;
    private List<RorschachPiece> piezasInstanciadas = new List<RorschachPiece>();
    private RorschachPiece piezaSeleccionada = null;

    void Awake()
    {
        if (panel != null) panel.SetActive(false);
        if (greenCheck != null) greenCheck.SetActive(false);
        if (redX != null) redX.SetActive(false);
    }

    void Update()
    {
        if (!IsRunning)
            return;

        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante < 0f)
            tiempoRestante = 0f;

        ActualizarTextoTimer();

        if (tiempoRestante <= 0f)
        {
            TiempoAgotado();
        }
    }

    // ==== API pública para iniciar/cerrar ====

    public void StartMiniGame()
    {
        if (IsRunning) return;

        if (levels == null || levels.Length == 0)
        {
            Debug.LogWarning("[RorschachMiniGame] No hay niveles configurados.");
            return;
        }

        IsRunning = true;
        tiempoRestante = tiempoTotal;
        currentLevelIndex = 0;

        if (panel != null) panel.SetActive(true);

        if (player != null)
            player.inputBlockedByMiniGame = true;

        ConstruirNivelActual();
    }

    public void CerrarMiniGame()
    {
        IsRunning = false;

        // limpiar piezas
        LimpiarGrid();

        if (panel != null)
            panel.SetActive(false);

        if (player != null)
            player.inputBlockedByMiniGame = false;
    }

    // ==== Construcción y manejo de niveles ====

    private void ConstruirNivelActual()
    {
        LimpiarGrid();

        if (currentLevelIndex < 0 || currentLevelIndex >= levels.Length)
        {
            Debug.LogError("[RorschachMiniGame] Índice de nivel inválido.");
            return;
        }

        RorschachLevelData level = levels[currentLevelIndex];

        // configurar grid
        if (gridLayout != null)
        {
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = level.cols;
        }

        if (levelText != null)
            levelText.text = $"{level.levelName} {currentLevelIndex + 1}/{levels.Length}";

        // instanciar piezas
        piezasInstanciadas.Clear();

        for (int i = 0; i < level.piezas.Length; i++)
        {
            RorschachPiece pieza = Instantiate(piecePrefab, gridParent);
            pieza.Init(this, level.piezas[i], i);
            piezasInstanciadas.Add(pieza);
        }

        // barajar posiciones
        RandomizarOrdenPiezas();

        // rotación aleatoria (0, 90, 180, 270)
        foreach (var p in piezasInstanciadas)
        {
            int steps = Random.Range(0, 4); // 0..3
            for (int s = 0; s < steps; s++)
                p.Rotate90();
        }

        // actualizar índices actuales
        ActualizarIndices();
        DeseleccionarPieza();
    }

    private void LimpiarGrid()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
        piezasInstanciadas.Clear();
        piezaSeleccionada = null;
    }

    private void RandomizarOrdenPiezas()
    {
        // Fisher-Yates sobre los children (siblingIndex)
        int count = gridParent.childCount;
        for (int i = 0; i < count; i++)
        {
            int rnd = Random.Range(i, count);
            Transform a = gridParent.GetChild(i);
            Transform b = gridParent.GetChild(rnd);

            int idxA = a.GetSiblingIndex();
            int idxB = b.GetSiblingIndex();
            a.SetSiblingIndex(idxB);
            b.SetSiblingIndex(idxA);
        }
    }

    private void ActualizarIndices()
    {
        int count = gridParent.childCount;
        for (int i = 0; i < count; i++)
        {
            RorschachPiece p = gridParent.GetChild(i).GetComponent<RorschachPiece>();
            if (p != null)
                p.currentIndex = i;
        }
    }

    // ==== Interacción con piezas ====

    public void OnPieceClicked(RorschachPiece pieza)
    {
        if (!IsRunning) return;

        if (piezaSeleccionada == null)
        {
            // seleccionar primera
            piezaSeleccionada = pieza;
            piezaSeleccionada.SetSelected(true);
        }
        else if (piezaSeleccionada == pieza)
        {
            // deseleccionar si se vuelve a hacer click
            DeseleccionarPieza();
        }
        else
        {
            // intercambiar posiciones entre ambas
            SwapPieces(piezaSeleccionada, pieza);
            DeseleccionarPieza();
            ActualizarIndices();
        }
    }

    private void SwapPieces(RorschachPiece a, RorschachPiece b)
    {
        int indexA = a.transform.GetSiblingIndex();
        int indexB = b.transform.GetSiblingIndex();

        a.transform.SetSiblingIndex(indexB);
        b.transform.SetSiblingIndex(indexA);
    }

    private void DeseleccionarPieza()
    {
        if (piezaSeleccionada != null)
            piezaSeleccionada.SetSelected(false);

        piezaSeleccionada = null;
    }

    // ==== Botón Rotar ====

    public void OnRotateButton()
    {
        if (!IsRunning) return;
        if (piezaSeleccionada == null) return;

        piezaSeleccionada.Rotate90();
    }

    // ==== Botón Submit ====

    public void OnSubmitButton()
    {
        if (!IsRunning) return;

        ActualizarIndices();

        bool todasCorrectas = true;
        foreach (var p in piezasInstanciadas)
        {
            if (!p.IsCorrect())
            {
                todasCorrectas = false;
                break;
            }
        }

        if (todasCorrectas)
        {
            StartCoroutine(FeedbackCorrectoYContinuar());
        }
        else
        {
            StartCoroutine(FeedbackIncorrecto());
            // +2% de paranoia por verificación fallida
            if (ParanoiaManager.Instance != null)
                ParanoiaManager.Instance.AddParanoiaPercent(paranoiaPorSubmitFallido);
        }
    }

    private IEnumerator FeedbackCorrectoYContinuar()
    {
        if (greenCheck != null) greenCheck.SetActive(true);
        if (redX != null) redX.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        if (greenCheck != null) greenCheck.SetActive(false);

        // siguiente nivel o victoria final
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Length)
        {
            // VICTORIA
            Victoria();
        }
        else
        {
            ConstruirNivelActual();
        }
    }

    private IEnumerator FeedbackIncorrecto()
    {
        if (redX != null) redX.SetActive(true);
        if (greenCheck != null) greenCheck.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        if (redX != null) redX.SetActive(false);
    }

    // ==== Timer / victoria / derrota ====

    private void ActualizarTextoTimer()
    {
        int totalSeconds = Mathf.CeilToInt(tiempoRestante);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        if (timerText != null)
            timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void TiempoAgotado()
    {
        if (!IsRunning) return;

        Debug.Log("[RorschachMiniGame] Tiempo agotado, derrota.");

        // penalización paranoia por derrota
        if (ParanoiaManager.Instance != null)
            ParanoiaManager.Instance.AddParanoiaPercent(paranoiaPorDerrotaTiempo);

        CerrarMiniGame();
    }

    private void Victoria()
    {
        Debug.Log("[RorschachMiniGame] ¡Victoria! Se completaron los 3 niveles.");

        // aquí podrías dar llave / premio si quieres

        CerrarMiniGame();
    }
}
