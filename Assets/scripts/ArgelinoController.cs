using UnityEngine;

public class ArgelinoController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speedNormal = 3f;       // Velocidad cuando no lo persiguen
    public float speedPerseguido = 5f;   // Velocidad cuando lo persigue un enemigo

    [HideInInspector]
    public float speed;                  // Velocidad actual (se ajusta con ActivarPersecucion)

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 objetivo;  // Punto al que se moverá
    private bool moviendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        objetivo = rb.position;   // Inicia en su posición actual
        speed = speedNormal;      // Arranca con velocidad normal
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
            objetivo = new Vector2(mousePos.x, mousePos.y); // Se mueve a X e Y
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
            // Calcular dirección hacia el objetivo
            Vector2 direccion = (objetivo - rb.position);

            // Si ya prácticamente llegamos, nos detenemos
            if (direccion.magnitude < 0.05f)
            {
                moviendo = false;
                return;
            }

            direccion.Normalize();

            // Flip del sprite según la dirección X
            if (direccion.x < 0) sr.flipX = true;        // Mirando izquierda
            else if (direccion.x > 0) sr.flipX = false; // Mirando derecha

            // Movimiento usando Rigidbody2D para respetar colisiones
            rb.MovePosition(rb.position + direccion * speed * Time.fixedDeltaTime);
        }
    }

    // ░░░ MÉTODOS PARA captaEnemigo ░░░

    // Versión sin parámetros, por si la llaman así:
    // argelino.ActivarPersecucion();
    public void ActivarPersecucion()
    {
        ActivarPersecucion(true);
    }

    // Versión con bool, por si la llaman así:
    // argelino.ActivarPersecucion(true);  // perseguido
    // argelino.ActivarPersecucion(false); // vuelve a normal
    public void ActivarPersecucion(bool perseguido)
    {
        speed = perseguido ? speedPerseguido : speedNormal;
        // Aquí podrías también cambiar animaciones, colores, etc.
        // Debug.Log("[Argelino] Persecución: " + perseguido + "  speed = " + speed);
    }
}
