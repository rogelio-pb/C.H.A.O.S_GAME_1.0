using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizMiniGame : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;                 // Panel del minijuego
    public TextMeshProUGUI questionText;     // Texto de la pregunta
    public TextMeshProUGUI timerText;        // Cronómetro
    public TextMeshProUGUI progressText;     // "Pregunta 3/20"
    public TextMeshProUGUI statusText;       // Mensaje final
    public TextMeshProUGUI rewardText;       // Mensaje de la llave / nombre

    [Header("Botones de respuesta")]
    public Button[] answerButtons;           // 3 o 4 botones

    [Header("Preguntas")]
    public List<QuestionData> questions = new List<QuestionData>(); // 20 preguntas
    public float tiempoPorPregunta = 6f;

    [Header("Paranoia (en %) por pregunta")]
    public float paranoiaMala = 2f;      // si contesta mal
    public float paranoiaTiempo = 0.5f;  // si no contesta dentro de los 6 s
    public float paranoiaBuena = -1.5f;  // si contesta bien

    [Header("Bloquear movimiento del player")]
    public ArgelinoController player;    // arrastra aquí tu ArgelinoController

    [Header("Recompensa llave")]
    public bool darLlaveSiCompleta = true;
    public ItemData llaveItem;           // tu item "llave"
    public string nombreJugador = "ARGELINO";

    private int currentQuestionIndex = 0;
    private float tiempoRestante = 0f;
    private bool esperandoRespuesta = false;
    private int respuestasCorrectasEnIntento = 0;

    void Awake()
    {
        if (panel != null) panel.SetActive(false);
        if (statusText != null) statusText.text = "";
        if (rewardText != null) rewardText.text = "";
    }

    void Update()
    {
        if (!panel.activeSelf || !esperandoRespuesta) return;

        tiempoRestante -= Time.deltaTime;
        if (timerText != null)
            timerText.text = tiempoRestante.ToString("0");

        if (tiempoRestante <= 0f)
        {
            TiempoAgotado();
        }
    }

    // Llamar para iniciar el minijuego
    public void StartQuiz()
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogWarning("[QuizMiniGame] No hay preguntas configuradas.");
            return;
        }

        currentQuestionIndex = 0;
        respuestasCorrectasEnIntento = 0;

        if (statusText != null) statusText.text = "";
        if (rewardText != null) rewardText.text = "";

        if (panel != null) panel.SetActive(true);

        // Bloquear movimiento del player + marcar bloqueo de input
        if (player != null)
        {
            player.inputBlockedByMiniGame = true;
        }

        CargarPreguntaActual();
    }

    private void CargarPreguntaActual()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            TerminarQuiz();
            return;
        }

        QuestionData q = questions[currentQuestionIndex];

        if (questionText != null)
            questionText.text = q.questionText;

        if (progressText != null)
            progressText.text = $"Pregunta {currentQuestionIndex + 1}/{questions.Count}";

        // Mezclar indices de opciones
        List<int> indices = new List<int>();
        for (int i = 0; i < q.options.Length; i++)
            indices.Add(i);

        for (int i = 0; i < indices.Count; i++)
        {
            int rnd = Random.Range(i, indices.Count);
            (indices[i], indices[rnd]) = (indices[rnd], indices[i]);
        }

        // Configurar botones
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < answerButtons.Length && i < q.options.Length; i++)
        {
            int optionIndexReal = indices[i];
            string textoOpcion = q.options[optionIndexReal];
            bool esCorrecta = (optionIndexReal == q.correctIndex);

            answerButtons[i].gameObject.SetActive(true);

            TextMeshProUGUI btnText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null) btnText.text = textoOpcion;

            answerButtons[i].onClick.AddListener(() =>
            {
                OnAnswerSelected(esCorrecta);
            });
        }

        tiempoRestante = tiempoPorPregunta;
        esperandoRespuesta = true;
    }

    private void OnAnswerSelected(bool esCorrecta)
    {
        if (!esperandoRespuesta) return;
        esperandoRespuesta = false;

        if (esCorrecta)
        {
            respuestasCorrectasEnIntento++;

            if (ParanoiaManager.Instance != null)
                ParanoiaManager.Instance.AddParanoiaPercent(paranoiaBuena); // -1.5%
        }
        else
        {
            if (ParanoiaManager.Instance != null)
                ParanoiaManager.Instance.AddParanoiaPercent(paranoiaMala);  // +2%
        }

        currentQuestionIndex++;
        CargarPreguntaActual();
    }

    private void TiempoAgotado()
    {
        if (!esperandoRespuesta) return;
        esperandoRespuesta = false;

        if (ParanoiaManager.Instance != null)
            ParanoiaManager.Instance.AddParanoiaPercent(paranoiaTiempo);   // +0.5%

        currentQuestionIndex++;
        CargarPreguntaActual();
    }

    private void TerminarQuiz()
    {
        esperandoRespuesta = false;

        if (statusText != null)
        {
            if (respuestasCorrectasEnIntento >= questions.Count)
                statusText.text = "<color=#00ff88>¡Has completado el cuestionario!</color>";
            else
                statusText.text = $"Has contestado bien {respuestasCorrectasEnIntento}/{questions.Count}.";
        }

        // Si contestó todas bien → dar llave
        if (darLlaveSiCompleta && respuestasCorrectasEnIntento >= questions.Count)
        {
            if (Inventory.Instance != null && llaveItem != null)
            {
                Inventory.Instance.AddItem(llaveItem, 1);
            }

            if (rewardText != null)
            {
                rewardText.text = $"Has obtenido la llave, <b>{nombreJugador}</b>.";
            }
        }

        Invoke(nameof(CerrarQuiz), 1.5f);
    }

    public void CerrarQuiz()
    {
        if (panel != null) panel.SetActive(false);

        if (player != null)
        {
            player.inputBlockedByMiniGame = false;
        }
    }
}
