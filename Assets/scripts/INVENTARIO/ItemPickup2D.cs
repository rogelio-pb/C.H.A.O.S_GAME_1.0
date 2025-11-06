using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup2D : MonoBehaviour
{
    public ItemData item;
    public int amount = 1;

    private void Reset()
    {
        // Asegura que el collider sea trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Asumimos que el Player tiene la etiqueta "Player"
        if (!collision.CompareTag("Player")) return;

        if (Inventory.Instance != null && item != null)
        {
            bool added = Inventory.Instance.AddItem(item, amount);
            if (added)
            {
                // Se añadió correctamente: desaparecer del mundo
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventario lleno, no se pudo recoger " + item.itemName);
            }
        }
    }
}
