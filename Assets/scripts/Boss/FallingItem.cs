using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public float fallSpeed = 3f;       // Velocidad de caída
    public float paranoiaDamage = 10f; // Cuánta paranoia sube al tocar al jugador

    void Update()
    {
        // Movimiento hacia abajo
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // Se destruye si sale de pantalla
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[FallingItem] Toqué al Player. paranoiaDamage = {paranoiaDamage}", this);

            if (ParanoiaManager.Instance != null)
            {
                ParanoiaManager.Instance.AddParanoiaPercent(paranoiaDamage);
            }
            else
            {
                Debug.LogError("[FallingItem] No hay ParanoiaManager.Instance en la escena.", this);
            }

            Destroy(gameObject);
        }
    }
}
