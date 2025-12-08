using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizMiniGame : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;                 // Panel del minijuego
    public TextMeshProUGUI questionText;     // Texto de la pregunta
    public TextMeshProUGUI timerText;        // Cronómetro
    public TextMeshProUGUI progressText;     // "Pregunta 3/20"
    public TextMeshProUGUI statusText;       // Mensaje final pequeño
    public TextMeshProUGUI rewardText;       // Mensaje pequeño de recompensa

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

    [Header("Feedback visual")]
    [Tooltip("Palomita verde que aparece 1 segundo cuando respondes bien")]
    public GameObject correctMark;       // palomita verde (Image u otro)
    public float tiempoFeedback = 1f;    // tiempo que se muestra la palomita

    [Header("Pantalla grande de llave")]
    [Tooltip("Panel que muestra la llave en grande (debe estar desactivado por defecto)")]
    public GameObject keyPanel;          // panel grande con imagen de llave
    public TextMeshProUGUI keyPanelText; // texto grande: ¡HAS OBTENIDO LLAVE!
    public string textoHasObtenidoLlave = "¡HAS OBTENIDO LLAVE!";

    [Header("Salida al terminar")]
    [Tooltip("Si se activa, al terminar el quiz se carga esta escena")]
    public bool cargarEscenaAlTerminar = false;
    public string nombreEscenaNivel;     // nombre de la escena (en Build Settings)

    // Estado interno
    public bool IsRunning { get; private set; } = false;

    private int currentQuestionIndex = 0;
    private float tiempoRestante = 0f;
    private bool esperandoRespuesta = false;
    private int respuestasCorrectasEnIntento = 0;

    void Awake()
    {
        if (panel != null) panel.SetActive(false);
        if (statusText != null) statusText.text = "";
        if (rewardText != null) rewardText.text = "";

        if (correctMark != null) correctMark.SetActive(false);
        if (keyPanel != null) keyPanel.SetActive(false);
    }

    void Update()
    {
        if (!IsRunning || panel == null || !panel.activeSelf || !esperandoRespuesta)
            return;

        tiempoRestante -= Time.deltaTime;

        if (timerText != null)
            timerText.text = Mathf.Ceil(tiempoRestante).ToString("0");

        if (tiempoRestante <= 0f)
        {
            TiempoAgotado();
        }
    }

    // Llamar para iniciar el minijuego
    public void StartQuiz()
    {
        if (IsRunning)
            return; // ya está abierto

        if (questions == null || questions.Count == 0)
        {
            Debug.LogWarning("[QuizMiniGame] No hay preguntas configuradas.");
            return;
        }

        IsRunning = true;
        currentQuestionIndex = 0;
        respuestasCorrectasEnIntento = 0;

        if (statusText != null) statusText.text = "";
        if (rewardText != null) rewardText.text = "";

        if (correctMark != null) correctMark.SetActive(false);
        if (keyPanel != null) keyPanel.SetActive(false);

        if (panel != null) panel.SetActive(true);

        // Bloquear movimiento del player
        if (player != null)
        {
            player.inputBlockedByMiniGame = true;
        }

        CargarPreguntaActual();
    }

    private void CargarPreguntaActual()
    {
        // Si ya pasamos todas las preguntas → terminar quiz
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

            // Capturamos el valor de esCorrecta en la lambda
            bool respuestaCorrecta = esCorrecta;

            answerButtons[i].onClick.AddListener(() =>
            {
                OnAnswerSelected(respuestaCorrecta);
            });
        }

        tiempoRestante = tiempoPorPregunta;
        esperandoRespuesta = true;
    }
    //==== boton cerrar quiz manual ====
    public void BotonCerrarQuiz()
    {
        CerrarQuizManual();
    }


    private void OnAnswerSelected(bool esCorrecta)
    {
        if (!esperandoRespuesta) return;
        esperandoRespuesta = false;

        // Paranoia + contador de correctas
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

        // Feedback de palomita y luego siguiente pregunta
        StartCoroutine(FeedbackAndNextQuestion(esCorrecta));
    }

    private void TiempoAgotado()
    {
        if (!esperandoRespuesta) return;
        esperandoRespuesta = false;

        if (ParanoiaManager.Instance != null)
            ParanoiaManager.Instance.AddParanoiaPercent(paranoiaTiempo);   // +0.5%

        // Aquí no hay palomita, solo pasar a la siguiente después de un pequeño delay
        StartCoroutine(FeedbackAndNextQuestion(false));
    }

    private IEnumerator FeedbackAndNextQuestion(bool esCorrecta)
    {
        // Desactivar interacción para que no se puedan spamear clicks
        SetButtonsInteractable(false);

        if (esCorrecta && correctMark != null)
            correctMark.SetActive(true);

        yield return new WaitForSeconds(tiempoFeedback);

        if (correctMark != null)
            correctMark.SetActive(false);

        // Reactivamos (la siguiente pregunta los volverá a configurar)
        SetButtonsInteractable(true);

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

        bool completoPerfecto = (respuestasCorrectasEnIntento >= questions.Count);

        // Si contestó todas bien → dar llave + mostrar pantalla grande
        if (darLlaveSiCompleta && completoPerfecto)
        {
            if (Inventory.Instance != null && llaveItem != null)
            {
                Inventory.Instance.AddItem(llaveItem, 1);
            }

            if (rewardText != null)
            {
                rewardText.text = $"Has obtenido la llave, <b>{nombreJugador}</b>.";
            }

            if (keyPanel != null)
            {
                keyPanel.SetActive(true);

                if (keyPanelText != null)
                    keyPanelText.text = textoHasObtenidoLlave.ToUpper();
            }
        }

        // Cerrar quiz y desbloquear player (y opcionalmente cambiar de escena)
        StartCoroutine(CerrarLuegoDeDelay());
    }

    private IEnumerator CerrarLuegoDeDelay()
    {
        yield return new WaitForSeconds(1.5f);

        if (panel != null)
            panel.SetActive(false);

        if (player != null)
            player.inputBlockedByMiniGame = false;

        IsRunning = false;

        // Opcional: regresar al nivel cargando la escena
        if (cargarEscenaAlTerminar && !string.IsNullOrEmpty(nombreEscenaNivel))
        {
            SceneManager.LoadScene(nombreEscenaNivel);
        }
    }

    public void CerrarQuizManual()
    {
        // Por si quieres cerrar desde un botón "Salir"
        StopAllCoroutines();

        if (panel != null)
            panel.SetActive(false);

        if (player != null)
            player.inputBlockedByMiniGame = false;

        IsRunning = false;
    }

    private void SetButtonsInteractable(bool value)
    {
        if (answerButtons == null) return;
        foreach (var btn in answerButtons)
        {
            if (btn != null)
                btn.interactable = value;
        }
    }
}
