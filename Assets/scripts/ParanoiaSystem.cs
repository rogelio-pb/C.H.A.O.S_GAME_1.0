using UnityEngine;

public class ParanoiaSystem : MonoBehaviour
{
    [Header("Paranoia (0 - 100)")]
    [Range(0, 100)]
    public float paranoia = 0f; // Valor inicial por defecto = 0

    public delegate void ParanoiaChanged(float newValue);
    public event ParanoiaChanged OnParanoiaChanged;

    private void Start()
    {
        paranoia = 0f;
        OnParanoiaChanged?.Invoke(paranoia);
    }

    public void AddParanoia(float amount)
    {
        SetParanoia(paranoia + amount);
    }

    public void ReduceParanoia(float amount)
    {
        SetParanoia(paranoia - amount);
    }

    public void SetParanoia(float value)
    {
        paranoia = Mathf.Clamp(value, 0f, 100f);
        OnParanoiaChanged?.Invoke(paranoia);
    }

    public float GetParanoiaPercent()
    {
        return paranoia / 100f;
    }
}
