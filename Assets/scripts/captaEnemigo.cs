using UnityEngine;

public class EnemigoPerseguidor : MonoBehaviour
{
    public Transform objetivo;            // El personaje
    public float velocidad = 2f;
    public float rangoDeteccion = 5f;

    private ArgelinoController jugador;   // Referencia al script del jugador
    private bool persiguiendo = false;

    void Start()
    {
        if (objetivo != null)
        {
            jugador = objetivo.GetComponent<ArgelinoController>();

            if (jugador == null)
                Debug.LogError("¡El objetivo no tiene ArgelinoController!");
        }
        else
        {
            Debug.LogError("No asignaste el objetivo del enemigo en el Inspector.");
        }
    }

    void Update()
    {
        if (objetivo == null || jugador == null)
            return;

        float distancia = Vector2.Distance(transform.position, objetivo.position);
        bool dentroDelRango = distancia <= rangoDeteccion;

        // Entra en persecución
        if (dentroDelRango && !persiguiendo)
        {
            persiguiendo = true;
            jugador.ActivarPersecucion(true); // 🔥 velocidad rápida
        }
        // Sale de persecución
        else if (!dentroDelRango && persiguiendo)
        {
            persiguiendo = false;
            jugador.ActivarPersecucion(false); // 😴 velocidad normal
        }

        // Movimiento del enemigo
        if (persiguiendo)
        {
            Vector2 direccion = (objetivo.position - transform.position).normalized;
            transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}
