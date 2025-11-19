using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI2D : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;          // Asignar el Player
    private Rigidbody2D rb;

    [Header("Patrulla")]
    public Transform puntoA;          // Punto de inicio de patrulla
    public Transform puntoB;          // Punto final de patrulla
    private Transform destinoActual;

    [Header("Movimiento")]
    public float velocidadPatrulla = 2f;
    public float velocidadPersecucion = 3f;

    [Header("Detecci�n / Ataque")]
    public float radioDeteccion = 5f;
    public float radioAtaque = 1.2f;
    public float tiempoEntreAtaques = 1f;
    public int danioAlPlayer = 10;

    private float ultimoTiempoAtaque;

    private enum EstadoEnemy { Patrullando, Persiguiendo, Atacando }
    private EstadoEnemy estadoActual = EstadoEnemy.Patrullando;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Empezar patrullando hacia puntoA
        destinoActual = puntoA;
    }

    private void Update()
    {
        if (player == null) return;

        float distanciaAlPlayer = Vector2.Distance(transform.position, player.position);

        // Cambiar estado seg�n distancia
        if (distanciaAlPlayer <= radioAtaque)
        {
            estadoActual = EstadoEnemy.Atacando;
        }
        else if (distanciaAlPlayer <= radioDeteccion)
        {
            estadoActual = EstadoEnemy.Persiguiendo;
        }
        else
        {
            estadoActual = EstadoEnemy.Patrullando;
        }
    }

    private void FixedUpdate()
    {
        switch (estadoActual)
        {
            case EstadoEnemy.Patrullando:
                Patrullar();
                break;
            case EstadoEnemy.Persiguiendo:
                Perseguir();
                break;
            case EstadoEnemy.Atacando:
                Atacar();
                break;
        }
    }

    private void Patrullar()
    {
        if (puntoA == null || puntoB == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direccion = (destinoActual.position - transform.position).normalized;
        rb.linearVelocity = direccion * velocidadPatrulla;

        // Si ya lleg� cerca del punto de destino, cambiarlo
        if (Vector2.Distance(transform.position, destinoActual.position) < 0.1f)
        {
            destinoActual = (destinoActual == puntoA) ? puntoB : puntoA;
        }

        // Opcional: voltear sprite seg�n direcci�n
        if (rb.linearVelocity.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = rb.linearVelocity.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void Perseguir()
    {
        Vector2 direccion = (player.position - transform.position).normalized;
        rb.linearVelocity = direccion * velocidadPersecucion;

        // Voltear sprite hacia el jugador
        if (rb.linearVelocity.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = rb.linearVelocity.x > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void Atacar()
    {
        // Al atacar, puedes decidir que se quede quieto o siga pegado al player
        rb.linearVelocity = Vector2.zero;

        if (Time.time - ultimoTiempoAtaque >= tiempoEntreAtaques)
        {
            // Comprobar que el player sigue cerca
            float distancia = Vector2.Distance(transform.position, player.position);
            if (distancia <= radioAtaque + 0.2f)
            {
                // Hacer da�o al player
                PlayerHealth ph = player.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(danioAlPlayer);
                    Debug.Log("[ENEMY] Atacando al player, da�o: " + danioAlPlayer);
                }

                ultimoTiempoAtaque = Time.time;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Solo para ver en la escena los radios de detecci�n/ataque
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioAtaque);
    }
}
