using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida del jugador")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Barra de vida (UI)")]
    public Slider healthSlider; // Opcional, si tienes una barra en canvas

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        Debug.Log($"[PLAYER] Daño recibido: {damage}. Vida actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("[PLAYER] El jugador ha muerto.");
        // Aquí pones lo que quieras: desactivar al player, cambiar de escena, etc.
    }
}
