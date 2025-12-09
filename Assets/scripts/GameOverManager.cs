using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLoader : MonoBehaviour
{
    public string sceneName = "GameOverScene"; // así se llamará tu escena

    public void LoadGameOver()
    {
        SceneManager.LoadScene(sceneName);
    }
}
