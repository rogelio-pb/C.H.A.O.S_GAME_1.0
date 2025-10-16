using UnityEngine;

public class ArgelinoController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speedNormal = 3f;       // Velocidad normal
    public float speedPerseguido = 5f;   // Velocidad cuando lo persiguen ⚡
    private float velocidadActual;       // Velocidad activa
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 objetivo;
    private bool moviendo = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        objetivo = rb.position;
        velocidadActual = speedNormal;  // 👈 Empieza con velocidad normal
    }

    private void Update()
    {
        // Detecta clic izquierdo o tap
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objetivo = new Vector2(mousePos.x, rb.position.y);
            moviendo = true;
        }
    }

    private void FixedUpdate()
    {
        if (moviendo)
        {
            // Dirección hacia el objetivo
            Vector2 direccion = (objetivo - rb.position).normalized;

            // Flip del sprite
            if (direccion.x < 0) sr.flipX = true;
            else if (direccion.x > 0) sr.flipX = false;

            // Movimiento con la velocidad actual 👇
            rb.MovePosition(rb.position + direccion * velocidadActual * Time.fixedDeltaTime);

            // Si llegó al objetivo, detener
            if (Vector2.Distance(rb.position, objetivo) < 0.1f)
            {
                moviendo = false;
            }
        }
    }

    // 👇 Estas dos funciones permiten que el enemigo cambie su velocidad
    public void ActivarPersecucion(bool perseguido)
    {
        velocidadActual = perseguido ? speedPerseguido : speedNormal;
    }
}
