using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ParanoiaManager : MonoBehaviour
{
    public static ParanoiaManager Instance { get; private set; }

    [Header("Config")]
    [Tooltip("La paranoia máxima. 25 equivale a 100% en la UI.")]
    public float maxParanoiaPercent = 25f;

    [Tooltip("Paranoia inicial al empezar el juego.")]
    public float startParanoiaPercent = 0f;

    [Header("UI")]
    [Tooltip("Imagen de la barra / jeringa (tipo Filled).")]
    public Image paranoiaFillImage;

    [Tooltip("Texto que muestra el porcentaje (0% - 100%).")]
    public TextMeshProUGUI paranoiaText;

    [Header("Game Over")]
    [Tooltip("Se llama cuando la paranoia llega al máximo (25).")]
    public UnityEvent onParanoiaMax;

    [HideInInspector]
    public float currentParanoiaPercent;   // escala 0–25

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Si quieres que sobreviva entre escenas:
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetParanoia(startParanoiaPercent);
    }

    // ──────────────── API PÚBLICA ────────────────

    /// <summary>
    /// Suma (o resta) paranoia en "puntos".
    /// Tus otros scripts llaman a esto: AddParanoiaPercent(+/-x).
    /// </summary>
    public void AddParanoiaPercent(float delta)
    {
        SetParanoia(currentParanoiaPercent + delta);
    }

    /// <summary>
    /// Fija directamente la paranoia.
    /// </summary>
    public void SetParanoia(float value)
    {
        float oldValue = currentParanoiaPercent;

        currentParanoiaPercent = Mathf.Clamp(value, 0f, maxParanoiaPercent);

        float normalized01 = (maxParanoiaPercent <= 0f)
            ? 0f
            : currentParanoiaPercent / maxParanoiaPercent;

        UpdateUI(normalized01);

        // Si acabamos de llegar al máximo → perder
        if (oldValue < maxParanoiaPercent && currentParanoiaPercent >= maxParanoiaPercent)
        {
            HandleParanoiaMax();
        }
    }

    // ──────────────── UI ────────────────

    private void UpdateUI(float normalized01)
    {
        // Barra (fillAmount 0–1)
        if (paranoiaFillImage != null)
            paranoiaFillImage.fillAmount = normalized01;

        // Texto (0–100%)
        if (paranoiaText != null)
        {
            int displayPercent = Mathf.RoundToInt(normalized01 * 100f);
            paranoiaText.text = displayPercent.ToString() + "%";
        }
    }

    // ──────────────── GAME OVER ────────────────

    private void HandleParanoiaMax()
    {
        Debug.Log("[ParanoiaManager] Paranoia máxima alcanzada. Jugador pierde.");

        if (onParanoiaMax != null)
            onParanoiaMax.Invoke();
        // Aquí puedes conectar en el Inspector:
        // - GameManager.GameOver()
        // - Cambiar de escena, etc.
    }
}
