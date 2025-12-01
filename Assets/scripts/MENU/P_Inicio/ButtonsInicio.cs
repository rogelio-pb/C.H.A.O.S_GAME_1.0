using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [Header("Paneles del Menú")]
    public GameObject panelPrincipal;
    public GameObject panelMenuPrincipal;
    public GameObject panelAjustes;
    public GameObject panelCreditos;
    public GameObject panelArchivo;

    [Header("Pantalla de Carga")]
    public GameObject panelLoading;      //  Panel de carga
    public Image loadingImage;           //  Imagen donde pondrás los frames
    public Sprite[] loadingFrames;       // Tus 9 sprites de animación
    public float frameRate = 0.1f;       // Velocidad de animación

    void Start()
    {
        panelPrincipal.SetActive(true);
        panelMenuPrincipal.SetActive(false);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(false);

        if (panelLoading) panelLoading.SetActive(false);
    }

    // Botón "Iniciar"
    public void AbrirMenu()
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(true);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(false);
    }

    // Botón "Ajustes"
    public void AbrirAjustes()
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(false);
        panelAjustes.SetActive(true);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(false);
    }

    // Botón "Créditos"
    public void AbrirCreditos()
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(false);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(true);
        panelArchivo.SetActive(false);
    }

    // Botón "Archivo"
    public void AbrirArchivo()
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(false);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(true);
    }

    //  INICIAR JUEGO CON PANTALLA DE CARGA
    public void IrAlJuego()
    {
        Debug.Log("Cargando escena de juego...");
        HideAllPanels();
        panelLoading.SetActive(true);

        StartCoroutine(PlayLoadingAnimation());
        StartCoroutine(LoadGameAsync(2)); // Carga la escena con índice 2
    }

    IEnumerator LoadGameAsync(int sceneIndex)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneIndex);
        load.allowSceneActivation = false;

        float timer = 0f;
        float minTime = 4.9f;

        while (!load.isDone)
        {
            timer += Time.deltaTime;

            if (load.progress >= 0.9f && timer >= minTime)
            {
                load.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    // Animación de sprites (GIF)
    IEnumerator PlayLoadingAnimation()
    {
        int frame = 0;

        while (panelLoading.activeSelf)
        {
            loadingImage.sprite = loadingFrames[frame];
            frame = (frame + 1) % loadingFrames.Length;

            yield return new WaitForSeconds(frameRate);
        }
    }

    // Botón "Volver"
    public void VolverAlMenu()
    {
        HideAllPanels();
        panelMenuPrincipal.SetActive(true);
    }
  


    // Ocultar todo
    void HideAllPanels()//Panel que oculta todos los demas
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(false);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(false);
    }
}
