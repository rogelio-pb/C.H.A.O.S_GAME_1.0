using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGoOrMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject pausePanel;   // Panel con botones
    [SerializeField] Button continueBtn;      // Botón "Continuar"
    [SerializeField] Button menuBtn;          // Botón "Menú"

    [Header("Opcional")]
    [SerializeField] bool allowEscKey = true; // Esc/Back para pausar
    [SerializeField] int menuBuildIndex = 0;  // 0 = MainMenu en Build Settings

    bool paused;

    void Awake()
    {
        SetPaused(false);

        if (continueBtn)
        {
            continueBtn.onClick.RemoveAllListeners();
            continueBtn.onClick.AddListener(ContinueGame);
        }
        if (menuBtn)
        {
            menuBtn.onClick.RemoveAllListeners();
            menuBtn.onClick.AddListener(ReturnToMenu);
        }
    }

    void Update()
    {
        if (!allowEscKey) return;
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    // Llama esto desde tu botón "Pausa" del HUD
    public void TogglePause()
    {
        if (paused) ContinueGame();
        else Pause();
    }

    public void Pause() => SetPaused(true);
    public void ContinueGame() => SetPaused(false);

    public void ReturnToMenu()
    {
        // Asegura tiempo normal antes de cambiar de escena
        Time.timeScale = 1f;

#if UNITY_STANDALONE || UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;
#endif

        SceneManager.LoadScene(menuBuildIndex);
    }

    void SetPaused(bool value)
    {
        paused = value;
        if (pausePanel) pausePanel.SetActive(value);
        Time.timeScale = value ? 0f : 1f;

#if UNITY_STANDALONE || UNITY_EDITOR
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible   = value;
#endif
    }
}
