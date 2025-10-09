using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Health targetHealth; // arrastra el Health objetivo
    [SerializeField] private Slider slider;       // si usas Slider
    [SerializeField] private Image fillImage;     // si usas Image con Fill

    [Header("Opcional")]
    [SerializeField] private bool hideWhenFull = false;

    void OnEnable()
    {
        if (targetHealth != null)
            targetHealth.OnHealthChanged.AddListener(UpdateBar);
    }

    void OnDisable()
    {
        if (targetHealth != null)
            targetHealth.OnHealthChanged.RemoveListener(UpdateBar);
    }

    void Start()
    {
        if (targetHealth != null)
            UpdateBar(targetHealth.CurrentHealth, targetHealth.MaxHealth);
    }

    void UpdateBar(float current, float max)
    {
        float t = (max <= 0f) ? 0f : current / max;

        if (slider) slider.value = t;
        if (fillImage) fillImage.fillAmount = t;

        if (hideWhenFull)
        {
            bool show = t < 1f;
            if (slider) slider.gameObject.SetActive(show);
            if (fillImage) fillImage.gameObject.SetActive(show);
        }
    }
}
