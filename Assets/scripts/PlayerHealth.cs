using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;   // 👈 IMPORTANTE

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de vida")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI - Barra de vida")]
    public Slider healthSlider;

    [Header("Muerte")]
    public string deathSceneName = "GameOverScene";  // 👈 nombre EXACTO de tu escena

    private bool isDead = false;

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
        if (isDead) return; // si ya está muerto, ignora más daño

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
        if (isDead) return;
        isDead = true;

        Debug.Log("[PLAYER] El jugador ha muerto.");

        // OPCIONAL: aquí puedes desactivar el movimiento del player, animación, etc.
        // GetComponent<PlayerMovement>().enabled = false;

        // Cargar la escena de muerte
        if (!string.IsNullOrEmpty(deathSceneName))
        {
            SceneManager.LoadScene(deathSceneName);
        }
        else
        {
            // Si no pusiste nombre, al menos desactivamos al player
            gameObject.SetActive(false);
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }
}
