using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    void OnEnable()
    {
        PlayerPrefs.SetInt("last_scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }
}
