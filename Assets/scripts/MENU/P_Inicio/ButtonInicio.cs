using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonInicio2D : MonoBehaviour
{
    private void OnMouseDown()
    {
        
        SceneManager.LoadScene(1);
    }
}
