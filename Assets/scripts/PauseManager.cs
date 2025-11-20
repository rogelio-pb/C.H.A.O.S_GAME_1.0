using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [Header("Referencias UI")]
    public GameObject pausePanel;

    private bool isPaused = false;

    public bool IsPaused => isPaused;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;           // congela todo
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;           // reanuda el juego
        pausePanel.SetActive(false);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;           // por si acaso
        SceneManager.LoadScene("MainMenu");  // cambia la escena
    }
}
