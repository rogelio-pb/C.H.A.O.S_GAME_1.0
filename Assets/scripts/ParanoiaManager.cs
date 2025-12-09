using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ParanoiaManager : MonoBehaviour
{
    public static ParanoiaManager Instance { get; private set; }

    [Header("Config")]
    [Tooltip("La paranoia máxima. 100 equivale a 100% en la UI.")]
    public float maxParanoiaPercent = 100f;

    [Tooltip("Paranoia inicial al empezar el juego.")]
    public float startParanoiaPercent = 0f;

    [Header("UI")]
    [Tooltip("Imagen de relleno (opcional, tipo Filled).")]
    public Image paranoiaFillImage;

    [Tooltip("Slider que muestra la paranoia (0–1).")]
    public Slider paranoiaSlider;

    [Tooltip("Texto que muestra el porcentaje (0% - 100%).")]
    public TextMeshProUGUI paranoiaText;

    [Header("Game Over")]
    [Tooltip("Se llama cuando la paranoia llega al máximo.")]
    public UnityEvent onParanoiaMax;

    [HideInInspector]
    public float currentParanoiaPercent;   // escala 0–100

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Configuramos el slider 0–1 si existe
        if (paranoiaSlider != null)
        {
            paranoiaSlider.minValue = 0f;
            paranoiaSlider.maxValue = 1f;
            paranoiaSlider.wholeNumbers = false;
        }

        SetParanoia(startParanoiaPercent);
    }

    /// <summary>
    /// Suma (o resta) paranoia en puntos de 0–100.
    /// </summary>
    public void AddParanoiaPercent(float delta)
    {
        Debug.Log($"[ParanoiaManager] AddParanoiaPercent({delta})");
        SetParanoia(currentParanoiaPercent + delta);
    }

    /// <summary>
    /// Fija directamente la paranoia (0–100).
    /// </summary>
    public void SetParanoia(float value)
    {
        float oldValue = currentParanoiaPercent;

        currentParanoiaPercent = Mathf.Clamp(value, 0f, maxParanoiaPercent);

        float normalized01 = (maxParanoiaPercent <= 0f)
            ? 0f
            : currentParanoiaPercent / maxParanoiaPercent;

        Debug.Log($"[ParanoiaManager] SetParanoia -> {currentParanoiaPercent}/{maxParanoiaPercent} (norm={normalized01})");

        UpdateUI(normalized01);

        if (oldValue < maxParanoiaPercent && currentParanoiaPercent >= maxParanoiaPercent)
        {
            HandleParanoiaMax();
        }
    }

    // ──────────────── UI ────────────────

    private void UpdateUI(float normalized01)
    {
        // Imagen de relleno (fillAmount 0–1)
        if (paranoiaFillImage != null)
            paranoiaFillImage.fillAmount = normalized01;

        // Slider (valor 0–1)
        if (paranoiaSlider != null)
        {
            paranoiaSlider.value = normalized01;
            Debug.Log($"[ParanoiaManager] Slider.value = {paranoiaSlider.value}");
        }

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
        Debug.Log("[ParanoiaManager] Paranoia máxima alcanzada.");

        if (onParanoiaMax != null)
            onParanoiaMax.Invoke();
    }
}
