using UnityEngine;
// Si NO tienes diálogos aún, puedes borrar cualquier línea con "DialogueUI"

public class ArgelinoController : MonoBehaviour
{
    public float speed = 3f; // Velocidad del personaje

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 objetivo; // Punto al que queremos llegar
    private bool moviendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        objetivo = rb.position; // Inicialmente en su posición actual
    }

    void Update()
    {
        // Si tienes sistema de diálogos y el panel está abierto, no te mueves
        if (DialogueUI.Instance != null && DialogueUI.Instance.IsOpen)
        {
            moviendo = false;
            objetivo = rb.position;
            return;
        }

        // Detecta clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main == null) return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Ahora nos movemos hacia X e Y (no solo X)
            objetivo = new Vector2(mousePos.x, mousePos.y);
            moviendo = true;
        }
    }

    void FixedUpdate()
    {
        // Si hay diálogo abierto, no mover
        if (DialogueUI.Instance != null && DialogueUI.Instance.IsOpen)
            return;

        if (moviendo)
        {
            // Calcula la dirección hacia el objetivo
            Vector2 direccion = (objetivo - rb.position);

            // Si ya prácticamente llegamos, nos detenemos
            if (direccion.magnitude < 0.05f)
            {
                moviendo = false;
                return;
            }

            direccion.Normalize();

            // Flip del sprite según la dirección X
            if (direccion.x < 0) sr.flipX = true;   // Mirando izquierda
            else if (direccion.x > 0) sr.flipX = false; // Mirando derecha

            // Movimiento usando Rigidbody2D para respetar colisiones
            rb.MovePosition(rb.position + direccion * speed * Time.fixedDeltaTime);
        }
    }
}
