using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalController : MonoBehaviour
{
    // Llama a esta función cuando se presione el botón "Jugar"
    public void IrAlJuego()
    {
        Debug.Log("Cargando escena de juego...");
        SceneManager.LoadScene(2); // Carga la escena con índice 2
    }
    public void IrAlMenu()
    {
        Debug.Log("Cargando escena de juego...");
        SceneManager.LoadScene(1); // Regresa al menu principal
    }
    // Llama a esta función cuando se presione el botón "Créditos"
    public void IrACreditos()
    {
        Debug.Log("Cargando escena de créditos...");
        SceneManager.LoadScene(4); // Carga la escena con índice 5
    }
}