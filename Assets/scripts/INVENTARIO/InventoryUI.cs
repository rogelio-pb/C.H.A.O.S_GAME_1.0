using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Referencias")]
    public Inventory inventory;
    public Transform slotsParent;           // Contenedor (Grid Layout Group)
    public InventorySlotUI slotPrefab;      // Prefab del slot UI

    private List<InventorySlotUI> slotInstances = new List<InventorySlotUI>();

    private void Start()
    {
        if (inventory == null)
            inventory = Inventory.Instance;

        GenerateSlots();

        inventory.OnInventoryChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= UpdateUI;
    }

    private void GenerateSlots()
    {
        // Limpiar anteriores
        foreach (Transform child in slotsParent)
        {
            Destroy(child.gameObject);
        }
        slotInstances.Clear();

        // Crear nuevos según el tamaño del inventario
        for (int i = 0; i < inventory.inventorySize; i++)
        {
            InventorySlotUI slotUI = Instantiate(slotPrefab, slotsParent);
            slotUI.Setup(inventory, i);
            slotInstances.Add(slotUI);
        }
    }

    private void UpdateUI()
    {
        foreach (InventorySlotUI slotUI in slotInstances)
        {
            slotUI.Refresh();
        }
    }
}
