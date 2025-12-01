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
            IrAEscena3();
        }
        else
        {
            Debug.Log("No tienes suficientes llaves");
        }
    }

    void IrAEscena3()
    {
        Debug.Log("CAMBIANDO A ESCENA 3…");

        // *** Muy importante ***
        PlayerPrefs.SetInt("viene_de_otra_escena", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(3);  //  Tu escena destino
    }
}
