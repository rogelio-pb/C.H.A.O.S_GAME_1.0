using UnityEngine;

public class InventoryToggleUI : MonoBehaviour
{
    public GameObject inventoryPanel; // Panel raíz del inventario

    private void Start()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false); // Empezar cerrado
    }

    public void ToggleInventory()
    {
        if (inventoryPanel == null) return;

        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);
    }
}
