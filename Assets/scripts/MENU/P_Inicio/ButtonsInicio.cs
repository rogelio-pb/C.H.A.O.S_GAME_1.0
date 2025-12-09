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
        /*Al iniciar el juego, reiniciar el checkpoint
       * PlayerPrefs.DeleteKey("tiene_checkpoint");
        *PlayerPrefs.DeleteKey("respawn_x");
        PlayerPrefs.DeleteKey("respawn_y");
        PlayerPrefs.DeleteKey("respawn_scene");
        PlayerPrefs.Save();
        Debug.Log("Checkpoint reiniciado al iniciar el juego.");
        * Al iniciar, mostrar solo el panel principal
        */
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
    public void NuevaPartida()
    {
        SaveSystem.ClearData();  // Borra checkpoint y progreso

        PlayerPrefs.SetInt("tiene_checkpoint", 0);
        PlayerPrefs.Save();

        // Cargar escena de inicio (0 o la que uses)
        IrAlJuego();
    }

    public void ContinuarPartida()
    {
        SaveSystem.LoadGame();

        if (PlayerPrefs.GetInt("tiene_checkpoint", 0) == 1)
        {
            int scene = PlayerPrefs.GetInt("respawn_scene");
            SceneManager.LoadScene(scene);
            Debug.Log("Cargando escena de juego...");
            HideAllPanels();
            panelLoading.SetActive(true);

            StartCoroutine(PlayLoadingAnimation());
        }
        else
        {
            Debug.Log("No hay checkpoint guardado. Iniciando nuevo.");
            NuevaPartida();
        }
    }


    //  INICIAR JUEGO CON PANTALLA DE CARGA
    public void IrAlJuego()
    {
        Debug.Log("Cargando escena de juego...");
        HideAllPanels();
        panelLoading.SetActive(true);

        StartCoroutine(PlayLoadingAnimation());
        StartCoroutine(LoadGameAsync(1)); // Carga la escena con índice 2
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

    // Botón "Salir"
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
