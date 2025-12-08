using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnPJuego : MonoBehaviour
{
    private void OnMouseDown()
        {
         
     
        SceneManager.LoadScene(1);

        Debug.Log("Cargando escena de juego...");
    }
}
