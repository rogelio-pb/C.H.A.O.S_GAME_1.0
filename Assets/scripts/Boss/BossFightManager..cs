using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;     // NECESARIO PARA IEnumerator

public class BossFightManager : MonoBehaviour
{
    [Header("Player")]
    public ArgelinoController player;    // ← TU CONTROLADOR REAL
    public float paranoia = 0f;
    public Image paranoiaBar;

    [Header("Objetos que caen")]
    public GameObject[] objetos; // 3 prefabs
    public float spawnInterval = 1.2f;
    public Transform spawnAreaLeft;
    public Transform spawnAreaRight;

    [Header("Tiempo de supervivencia")]
    public float fightDuration = 60f;
    private float timer;
    private bool isFighting = true;

    void Start()
    {
        timer = fightDuration;
        StartCoroutine(SpawnRutina());
    }

    void Update()
    {
        if (!isFighting) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            WinFight();
        }
    }

    IEnumerator SpawnRutina()   // ← YA FUNCIONA
    {
        while (isFighting)
        {
            SpawnObjeto();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObjeto()
    {
        float x = Random.Range(spawnAreaLeft.position.x, spawnAreaRight.position.x);
        int index = Random.Range(0, objetos.Length);

        Vector3 pos = new Vector3(x, spawnAreaLeft.position.y, 0f);
        Instantiate(objetos[index], pos, Quaternion.identity);
    }

    public void AddParanoia(float amount)
    {
        paranoia += amount;
        paranoiaBar.fillAmount = paranoia / 100f;

        if (paranoia >= 100f)
        {
            LoseFight();
        }
    }

    void WinFight()
    {
        isFighting = false;
        player.inputBlockedByMiniGame = true;
        SceneManager.LoadScene("BossWin");
    }

    void LoseFight()
    {
        isFighting = false;
        player.inputBlockedByMiniGame = true;
        SceneManager.LoadScene("BossLose");
    }
}
