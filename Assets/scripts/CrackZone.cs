using UnityEngine;

public class CrackZone : MonoBehaviour
{
    [Tooltip("Paranoia por grietas")]
    public float paranoiaDamage = 1.5f;

    public float requiredTime = 3f;
    private float timer = 0f;

    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            timer = 0f; // reiniciar
        }
    }

    private void Update()
    {
        if (playerInside)
        {
            timer += Time.deltaTime;

            if (timer >= requiredTime)
            {
                // Aumentar paranoia
                ParanoiaManager.Instance.AddParanoiaPercent(paranoiaDamage);
                Debug.Log("Paranoia +5 (grieta)");

                timer = 0f; // evitar que se repita sin salir
            }
        }
    }
}
