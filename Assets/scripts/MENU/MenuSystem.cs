using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystemByName : MonoBehaviour
{
    [Header("Escenas por nombre")]
    [SerializeField] string firstLevelName = "Juego";
    [SerializeField] string menuName = "MainMenu";

    [Header("Paneles")]
    [SerializeField] GameObject panelPrincipal;
    [SerializeField] GameObject panelOpciones;
    [SerializeField] Selectable firstSelectedMain;
    [SerializeField] Selectable firstSelectedOptions;

    void Start()
    {
        ShowMain();
        Select(firstSelectedMain);
        // Log útil: confirma que ambas escenas están en Build Settings
        Debug.Log($"Menu espera: '{menuName}' y '{firstLevelName}' en Build Settings.");
    }

    public void OnComenzar()
    {
        Time.timeScale = 1f; // por si venías de un juego pausado
        SceneManager.LoadScene(firstLevelName);
    }

    public void ShowOptions()
    {
        panelPrincipal.SetActive(false);
        panelOpciones.SetActive(true);
        Select(firstSelectedOptions);
    }

    public void ShowMain()
    {
        if (panelOpciones) panelOpciones.SetActive(false);
        if (panelPrincipal) panelPrincipal.SetActive(true);
        Select(firstSelectedMain);
    }

    public void OnSalir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void Select(Selectable s)
    {
        if (!s) return;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(s.gameObject);
    }
}
