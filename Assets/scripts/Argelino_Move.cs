using NUnit.Framework;
using UnityEngine;

public class ArgelinoController : MonoBehaviour
{
    public float speed = 3f; // Velocidad del personaje
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 objetivo; // Donde queremos que llegue
    private bool moviendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        objetivo = rb.position; // Inicialmente en su posición
    }

    void Update()
    {
        

        // Detecta clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objetivo = new Vector2(mousePos.x, rb.position.y);
            moviendo = true;
        }
    }

    void FixedUpdate()
    {
        if (moviendo)
        {
            // Calcula la dirección
            Vector2 direccion = (objetivo - rb.position).normalized;

            // Flip del sprite según la dirección X
            if (direccion.x < 0) sr.flipX = true;  // Mirando izquierda
            else if (direccion.x > 0) sr.flipX = false; // Mirando derecha

            // Movimiento usando Rigidbody2D para respetar colisiones
            rb.MovePosition(rb.position + direccion * speed * Time.fixedDeltaTime);

            // Si llegó al objetivo, deja de moverse
            if (Vector2.Distance(rb.position, objetivo) < 0.1f)
            {
                moviendo = false;
            }
        }
    }
}
