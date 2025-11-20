using UnityEngine;

public class ArgelinoController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speedNormal = 3f;       // Velocidad cuando no lo persiguen
    public float speedPerseguido = 5f;   // Velocidad cuando lo persigue un enemigo

    [HideInInspector]
    public float speed;                  // Velocidad actual (se ajusta con ActivarPersecucion)

    [Header("Tap (paso corto)")]
    public float tapStepDistance = 0.4f;   // qué tanto avanza en un toque corto
    public float tapThreshold = 0.15f;     // menos de esto cuenta como "tap" (segundos)

    [Header("Bloqueo por minijuego")]
    public bool inputBlockedByMiniGame = false;
    // Lo pondrá en true/false tu script de minijuego

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // estados de movimiento continuo
    private bool movingLeft = false;
    private bool movingRight = false;

    // para detectar tap vs mantener presionado
    private float pressStartTime = 0f;
    private int lastDir = 0; // -1 izquierda, +1 derecha

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        speed = speedNormal;      // Arranca con velocidad normal
    }

    // ---- helper para saber si la UI debe bloquear el movimiento ----
    private bool IsInputBlocked()
    {
        // diálogo de la enfermera
        if (DialogueUI.Instance != null && DialogueUI.Instance.IsOpen)
            return true;

        // minijuego (lo controlas desde tu script de minijuego)
        if (inputBlockedByMiniGame)
            return true;

        return false;
    }

    void Update()
    {
        if (IsInputBlocked())
        {
            movingLeft = false;
            movingRight = false;
            rb.linearVelocity = Vector2.zero;
            return;
        }
    }

    void FixedUpdate()
    {
        if (IsInputBlocked())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dir = Vector2.zero;

        if (movingLeft)
            dir = Vector2.left;
        else if (movingRight)
            dir = Vector2.right;

        rb.linearVelocity = dir * speed;

        // Flip del sprite
        if (dir.x < 0) sr.flipX = true;
        else if (dir.x > 0) sr.flipX = false;
    }

    // ░░░ MÉTODOS PARA LOS BOTONES ░░░

    public void StartMoveLeft()
    {
        if (IsInputBlocked()) return;

        pressStartTime = Time.time;
        lastDir = -1;

        movingLeft = true;
        movingRight = false;
    }

    public void StartMoveRight()
    {
        if (IsInputBlocked()) return;

        pressStartTime = Time.time;
        lastDir = 1;

        movingRight = true;
        movingLeft = false;
    }

    public void StopMove()
    {
        // si la UI está bloqueando, NO damos ni siquiera el pasito
        if (IsInputBlocked())
        {
            movingLeft = false;
            movingRight = false;
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float heldTime = Time.time - pressStartTime;

        movingLeft = false;
        movingRight = false;
        rb.linearVelocity = Vector2.zero;

        // Tap corto → pasito
        if (heldTime < tapThreshold && lastDir != 0)
        {
            Vector2 stepDir = (lastDir < 0) ? Vector2.left : Vector2.right;
            rb.MovePosition(rb.position + stepDir * tapStepDistance);
        }
    }

    // ░░░ MÉTODOS PARA captaEnemigo ░░░

    public void ActivarPersecucion()
    {
        ActivarPersecucion(true);
    }

    public void ActivarPersecucion(bool perseguido)
    {
        speed = perseguido ? speedPerseguido : speedNormal;
    }
}
