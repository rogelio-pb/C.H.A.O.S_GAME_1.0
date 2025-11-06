using UnityEngine;

public class ParanoiaTrigger : MonoBehaviour
{
    public float amount = 10f;
    private ParanoiaSystem paranoiaSystem;

    private void Start()
    {
        paranoiaSystem = FindObjectOfType<ParanoiaSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (paranoiaSystem != null)
            {
                paranoiaSystem.AddParanoia(amount);
            }
        }
    }
}

