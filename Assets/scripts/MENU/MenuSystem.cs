using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuSystemByName : MonoBehaviour
{
    [Header("Escenas")]
    [SerializeField] int gameSceneIndex = 1;   // Escena del juego (índice 1)

    [Header("Paneles del Menú")]
    [SerializeField] GameObject panelPrincipal;
    [SerializeField] GameObject panelOpciones;
    [SerializeField] GameObject panelCreditos;
    [SerializeField] GameObject panelArchivo;
    [SerializeField] GameObject panelLoading;

    [Header("Primera Selección (Gamepad/Teclado)")]
    [SerializeField] Selectable firstSelectedMain;
    [SerializeField] Selectable firstSelectedOptions;
    [SerializeField] Selectable firstSelectedCreditos;
    [SerializeField] Selectable firstSelectedArchivo;

    void Start()
    {
        if (panelLoading) panelLoading.SetActive(false);
        ShowMain();
    }

    // ==========================================================
    //  Iniciar juego con pantalla de carga
    // ==========================================================
    public void OnComenzar()
    {
        Time.timeScale = 3f;

        panelPrincipal.SetActive(false);
        panelOpciones.SetActive(false);
        if (panelCreditos) panelCreditos.SetActive(false);
        if (panelArchivo) panelArchivo.SetActive(false);
        panelLoading.SetActive(true);

        StartCoroutine(LoadGameAsync());
    }

    IEnumerator LoadGameAsync()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(gameSceneIndex);
        load.allowSceneActivation = false;

        float minLoadTime = 1f;
        float timer = 0f;

        while (!load.isDone)
        {
            timer += Time.deltaTime;

            if (load.progress >= 0.9f && timer >= minLoadTime)
            {
                load.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    // ==========================================================
    //  MOSTRAR MENÚS
    // ==========================================================
    public void ShowOptions()
    {
        HideAllPanels();
        panelOpciones.SetActive(true);
        Select(firstSelectedOptions);
    }

    public void ShowCreditos()
    {
        HideAllPanels();
        panelCreditos.SetActive(true);
        Select(firstSelectedCreditos);
    }

    public void ShowArchivo()
    {
        HideAllPanels();
        panelArchivo.SetActive(true);
        Select(firstSelectedArchivo);
    }

    public void ShowMain()
    {
        HideAllPanels();
        panelPrincipal.SetActive(true);
        Select(firstSelectedMain);
    }

    // ==========================================================
    //  UTILIDAD: Oculta todos los paneles excepto loading
    // ==========================================================
    void HideAllPanels()
    {
        if (panelPrincipal) panelPrincipal.SetActive(false);
        if (panelOpciones) panelOpciones.SetActive(false);
        if (panelCreditos) panelCreditos.SetActive(false);
        if (panelArchivo) panelArchivo.SetActive(false);
    }

    // ==========================================================
    //  SALIR DEL JUEGO
    // ==========================================================
    public void OnSalir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ==========================================================
    //  SELECCIÓN AUTOMÁTICA (Control/Teclado)
    // ==========================================================
    void Select(Selectable s)
    {
        if (!s) return;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(s.gameObject);
    }
}
