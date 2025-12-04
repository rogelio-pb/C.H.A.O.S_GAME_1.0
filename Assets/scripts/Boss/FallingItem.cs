using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public float fallSpeed = 3f;      // Velocidad de caída
    public float paranoiaDamage = 2f; // Cuánta paranoia sube al tocar al jugador

    private BossFightManager manager;

    void Start()
    {
        // Busca el Manager dentro de la escena (debe existir uno)
        manager = FindObjectOfType<BossFightManager>();
    }

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
        // Si toca al jugador...
        if (other.CompareTag("Player"))
        {
            manager.AddParanoia(paranoiaDamage); // ← Sube paranoia
            Destroy(gameObject);                // ← Se destruye
        }
    }
}
