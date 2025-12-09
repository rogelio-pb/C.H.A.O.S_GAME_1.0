using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private GameObject panel;   // Canvas/Panel: MenuPause
    [SerializeField] private Button btnContinuar; // hijo: CONTINUAR
    [SerializeField] private Button btnSalir;     // hijo: SALIR

    [Header("Configuración")]
    [SerializeField] private string mainMenuScene = "MainMenu";
    [SerializeField] private bool allowEscKey = true;

    private bool paused;

    void Awake()
    {
        // Estado inicial
        if (panel) panel.SetActive(false);

        // Enlaza botones
        if (btnContinuar)
        {
            btnContinuar.onClick.RemoveAllListeners();
            btnContinuar.onClick.AddListener(Continuar);
        }
        if (btnSalir)
        {
            btnSalir.onClick.RemoveAllListeners();
            btnSalir.onClick.AddListener(SalirAlMenu);
        }
    }

    void Update()
    {
        if (!allowEscKey) return;
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    // Llama esto desde tu botón "⏸" del HUD
    public void Toggle()
    {
        if (paused) Continuar();
        else Pausar();
    }

    public void Pausar()
    {
        paused = true;
        if (panel) panel.SetActive(true);
        Time.timeScale = 0f;

#if UNITY_STANDALONE || UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;
#endif
    }

    public void Continuar()
    {
        paused = false;
        if (panel) panel.SetActive(false);
        Time.timeScale = 1f;

#if UNITY_STANDALONE || UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
#endif
    }

    public void SalirAlMenu()
    {
        // Asegura el tiempo normal antes de cambiar de escena
        Time.timeScale = 1f;

#if UNITY_STANDALONE || UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;
#endif

        SceneManager.LoadScene(0);
    }
}
