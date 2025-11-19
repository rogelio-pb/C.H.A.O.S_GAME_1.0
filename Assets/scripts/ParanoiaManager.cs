using UnityEngine;
using UnityEngine.UI;

public class ParanoiaManager : MonoBehaviour
{
    public static ParanoiaManager Instance;

    [Header("Paranoia")]
    public float maxParanoia = 100f;
    public float currentParanoia = 0f;

    [Header("UI - Barra de paranoia")]
    public Slider paranoiaSlider;   // Slider en el Canvas

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (paranoiaSlider != null)
        {
            paranoiaSlider.minValue = 0f;
            paranoiaSlider.maxValue = maxParanoia;
            paranoiaSlider.value = currentParanoia;
        }
    }

    public void AddParanoia(float amount)
    {
        currentParanoia += amount;
        currentParanoia = Mathf.Clamp(currentParanoia, 0f, maxParanoia);

        if (paranoiaSlider != null)
            paranoiaSlider.value = currentParanoia;

        Debug.Log($"[PARANOIA] Cambio: {amount}, Actual: {currentParanoia}");
    }

    public void SetParanoia(float value)
    {
        currentParanoia = Mathf.Clamp(value, 0f, maxParanoia);

        if (paranoiaSlider != null)
            paranoiaSlider.value = currentParanoia;
    }
}
