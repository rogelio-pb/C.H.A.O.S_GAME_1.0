using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Iframes (opcional)")]
    [SerializeField] private float invulnerableTime = 0f; // e.g. 0.5f
    private float lastDamageTime = -999f;

    [Header("Eventos")]
    public UnityEvent<float, float> OnHealthChanged; // (vidaActual, vidaMax)
    public UnityEvent OnDeath;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealed;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    public bool IsDead => currentHealth <= 0f;

    void Awake()
    {
        currentHealth = Mathf.Clamp(currentHealth <= 0 ? maxHealth : currentHealth, 0, maxHealth);
        Notify();
    }

    public void SetMaxHealth(float value, bool fill = true)
    {
        maxHealth = Mathf.Max(1f, value);
        if (fill) currentHealth = maxHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Notify();
    }

    public void Damage(float amount)
    {
        if (Time.time - lastDamageTime < invulnerableTime || IsDead) return;

        lastDamageTime = Time.time;
        float prev = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth - Mathf.Abs(amount), 0, maxHealth);

        if (currentHealth < prev) OnDamaged?.Invoke();
        Notify();

        if (IsDead) OnDeath?.Invoke();
    }

    public void Heal(float amount)
    {
        if (IsDead) return;
        float prev = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth + Mathf.Abs(amount), 0, maxHealth);

        if (currentHealth > prev) OnHealed?.Invoke();
        Notify();
    }

    public void Kill()
    {
        if (IsDead) return;
        currentHealth = 0f;
        Notify();
        OnDeath?.Invoke();
    }

    void Notify() => OnHealthChanged?.Invoke(currentHealth, maxHealth);

    // --- DEMO: teclas para probar ---
    void Update()
    {
        // Q daña 10, E cura 10 (quítalo en producción)
        if (Input.GetKeyDown(KeyCode.Q)) Damage(10);
        if (Input.GetKeyDown(KeyCode.E)) Heal(10);
    }
}
