using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaMinijuego : MonoBehaviour
{
    
    private void OnMouseDown()
    {


        SceneManager.LoadScene(4);

        Debug.Log("Cargando escena de juego...");
    }
}
