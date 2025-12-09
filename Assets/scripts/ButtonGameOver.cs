using UnityEngine;

public class ButtonGameOver : MonoBehaviour
{
public void RestartLevel()
    {
        // Reload the current active scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

    }

    public void QuitGame()
    {
        // Quit the application
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
