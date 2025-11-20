using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParanoiaManager : MonoBehaviour
{
    public static ParanoiaManager Instance;

    [Header("Paranoia")]
    [Tooltip("Valor máximo de paranoia. Normalmente 100 = 100%.")]
    public float maxParanoia = 100f;

    [Tooltip("Paranoia actual en el rango 0–100.")]
    [Range(0f, 100f)]
    public float currentParanoia = 0f;

    [Header("UI")]
    [Tooltip("Slider de la barra de paranoia en el HUD.")]
    public Slider paranoiaSlider;

    [Tooltip("Texto que muestra el porcentaje de paranoia, ej. '35%'.")]
    public TextMeshProUGUI paranoiaText;

    [Header("Umbrales (solo para efectos opcionales)")]
    public float alertThreshold = 50f;   // p.ej. empieza a haber efectos visuales
    public float crisisThreshold = 80f;  // p.ej. paranoia muy alta

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Opcional: DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (paranoiaSlider != null)
        {
            paranoiaSlider.minValue = 0f;
            paranoiaSlider.maxValue = maxParanoia;
            paranoiaSlider.value = currentParanoia;
        }

        UpdateUI();
    }

    // ───────────────── API PRINCIPAL ─────────────────

    /// <summary>
    /// Suma/resta paranoia en valores absolutos (0–100).
    /// Normalmente usarás AddParanoiaPercent en su lugar.
    /// </summary>
    public void AddParanoia(float amount)
    {
        currentParanoia = Mathf.Clamp(currentParanoia + amount, 0f, maxParanoia);
        UpdateUI();
        CheckThresholds();
    }

    /// <summary>
    /// Suma/resta paranoia en PORCENTAJE. Ej: 2 = +2%, -1.5 = -1.5%.
    /// </summary>
    public void AddParanoiaPercent(float percent)
    {
        float amount = maxParanoia * (percent / 100f);
        AddParanoia(amount);
    }

    /// <summary>
    /// Fija la paranoia directamente en un valor concreto (0–100).
    /// </summary>
    public void SetParanoia(float value)
    {
        currentParanoia = Mathf.Clamp(value, 0f, maxParanoia);
        UpdateUI();
        CheckThresholds();
    }

    // ───────────────── LÓGICA INTERNA ─────────────────

    private void UpdateUI()
    {
        if (paranoiaSlider != null)
            paranoiaSlider.value = currentParanoia;

        if (paranoiaText != null)
            paranoiaText.text = $"{currentParanoia:0}%";
    }

    private void CheckThresholds()
    {
        // Aquí puedes disparar efectos especiales según la paranoia.
        // Solo dejo ganchos por si luego quieres usarlos.

        if (currentParanoia >= crisisThreshold)
        {
            // Modo crisis
            // Debug.Log("[PARANOIA] CRISIS");
        }
        else if (currentParanoia >= alertThreshold)
        {
            // Modo alerta
            // Debug.Log("[PARANOIA] ALERTA");
        }
        else
        {
            // Modo normal
            // Debug.Log("[PARANOIA] CALMA");
        }
    }
}
