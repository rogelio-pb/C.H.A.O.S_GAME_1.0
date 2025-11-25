using UnityEngine;

public class ArgelinoController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speedNormal = 3f;
    public float speedPerseguido = 5f;

    [HideInInspector]
    public float speed;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        speed = speedNormal;
    }

    void FixedUpdate()
    {
        // Si hay diálogo abierto, no moverse
        if (DialogueUI.Instance != null && DialogueUI.Instance.IsOpen)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("Moving", false);
            return;
        }

        // Dirección del movimiento desde botones (−1, 0, 1)
        float move = PlayerButtons.moveDirection;

        // ---- ANIMACIÓN ----
        anim.SetBool("Moving", move != 0);

        // ---- Flip del sprite ----
        if (move < 0) sr.flipX = true;
        else if (move > 0) sr.flipX = false;

        // ---- Movimiento ----
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
    }
    public void Atacar()
    {
        anim.SetTrigger("AttackTrigger");  // Activa la animación
        Debug.Log(" Ataque ejecutado");
    }

    // Modo persecución (enemigos)
    public void ActivarPersecucion(bool perseguido = true)
    {
        speed = perseguido ? speedPerseguido : speedNormal;
    }
}
