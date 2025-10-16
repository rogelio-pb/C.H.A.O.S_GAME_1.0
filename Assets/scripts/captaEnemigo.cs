using UnityEngine;

public class EnemigoPerseguidor : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 2f;
    public float rangoDeteccion = 5f;

    private ArgelinoController jugador;
    private bool persiguiendo = false;

    void Start()
    {
        if (objetivo != null)
            jugador = objetivo.GetComponent<ArgelinoController>();
    }

    void Update()
    {
        if (objetivo == null) return;

        float distancia = Vector2.Distance(transform.position, objetivo.position);
        bool dentroDelRango = distancia <= rangoDeteccion;

        if (dentroDelRango && !persiguiendo)
        {
            persiguiendo = true;
            jugador.ActivarPersecucion(true); // ⚡ activa velocidad rápida
        }
        else if (!dentroDelRango && persiguiendo)
        {
            persiguiendo = false;
            jugador.ActivarPersecucion(false); // 💤 vuelve a velocidad normal
        }

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
