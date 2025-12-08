using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaTapSimple : MonoBehaviour
{
 

    private void OnMouseDown()
    {
        Debug.Log("Click detectado en la puerta");
        IrAEscena3();
      
    }

    void IrAEscena3()
    {
        Debug.Log("CAMBIANDO A ESCENA 3…");

        // *** Muy importante ***
        PlayerPrefs.SetInt("viene_de_otra_escena", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(4);  //  Tu escena destino
    }
}
