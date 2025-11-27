using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaTapSimple : MonoBehaviour
{
    public int llavesNecesarias = 1;

    private void OnMouseDown()
    {
        Debug.Log("Click detectado en la puerta");

        if (Data.llave >= llavesNecesarias)
        {
            AbrirPuerta();
        }
        else
        {
            Debug.Log("No tienes suficientes llaves");
        }
    }

    void AbrirPuerta()
    {
        Debug.Log("ABRIENDO PUERTA…");
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}
