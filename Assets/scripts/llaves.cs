using UnityEngine;
public class Llave : MonoBehaviour
{
    private void OnMouseDown()
    {
      
            // Aumenta el contador de llaves
            Data.llave += 1;
            Debug.Log("Llaves: " + Data.llave);

            // Destruye la llave
            Destroy(gameObject);
        
    }
}

