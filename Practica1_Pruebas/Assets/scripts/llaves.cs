using UnityEngine;
public class Llave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que tocó la llave tiene el script ArgelinoController
        ArgelinoController argelino = other.GetComponent<ArgelinoController>();

        if (argelino != null)
        {
            // Aumenta el contador de llaves
            Data.llave += 1;
            Debug.Log("Llaves: " + Data.llave);

            // Destruye la llave
            Destroy(gameObject);
        }
    }
}

