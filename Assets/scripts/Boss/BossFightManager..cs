using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro; // 👈 IMPORTANTE

public class BossFightManager : MonoBehaviour
{
    [Header("Player")]
    public ArgelinoController player;

    [Header("Objetos que caen")]
    public GameObject[] objetos;
    public float spawnInterval = 1.2f;
    public Transform spawnAreaLeft;
    public Transform spawnAreaRight;

    [Header("Tiempo de supervivencia")]
    public float fightDuration = 60f;
    private float timer;
    private bool isFighting = true;

    [Header("UI")]
    [Tooltip("Texto que muestra el tiempo restante")]
    public TextMeshProUGUI timerText;   // 👈 NUEVO

    void Start()
    {
        timer = fightDuration;
        UpdateTimerUI(); // 👈 Muestra el tiempo inicial
        StartCoroutine(SpawnRutina());
    }

    void Update()
    {
        if (!isFighting) return;

        timer -= Time.deltaTime;

        UpdateTimerUI(); // 👈 Se actualiza cada frame

        if (timer <= 0f)
        {
            WinFight();
        }
    }

    IEnumerator SpawnRutina()
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

    // ⬇️ Función nueva que actualiza el tiempo visualmente
    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Formato de tiempo: MINUTO:SEGUNDO
            int minutos = Mathf.FloorToInt(timer / 60f);
            int segundos = Mathf.FloorToInt(timer % 60f);

            timerText.text = $"{minutos:00}:{segundos:00}";
        }
    }

    public void AddParanoia(float amount)
    {
        if (ParanoiaManager.Instance != null)
        {
            ParanoiaManager.Instance.AddParanoiaPercent(amount);
        }
    }

    public void WinFight()
    {
        isFighting = false;
        if (player != null)
            player.inputBlockedByMiniGame = true;

        SceneManager.LoadScene("FinalDemo");
    }

    public void LoseFight()
    {
        isFighting = false;
        if (player != null)
            player.inputBlockedByMiniGame = true;

        SceneManager.LoadScene("BossLose");
    }
}
