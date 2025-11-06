using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Singleton para acceder con Inventory.Instance
    public static Inventory Instance;

    [Header("Configuración")]
    [Tooltip("Cantidad de casillas del inventario")]
    public int inventorySize = 20;

    [Header("Slots (no modificar en Play)")]
    public List<InventorySlot> slots = new List<InventorySlot>();

    // Se dispara cada vez que cambia algo en el inventario
    public event Action OnInventoryChanged;

    private void Awake()
    {
        // Singleton básico
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Inicializar lista de slots
        if (slots == null || slots.Count != inventorySize)
        {
            slots = new List<InventorySlot>(inventorySize);
            for (int i = 0; i < inventorySize; i++)
            {
                slots.Add(new InventorySlot());
            }
        }
    }

    /// <summary>
    /// Agrega un ítem al inventario. Devuelve true si entró todo, false si no hubo espacio suficiente.
    /// </summary>
    public bool AddItem(ItemData item, int amount = 1)
    {
        if (item == null || amount <= 0)
            return false;

        int originalAmount = amount;

        Debug.Log($"[INVENTORY] Intentando agregar {amount}x {item.itemName}");

        // 1) Intentar stackear en slots existentes
        if (item.stackable)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                InventorySlot slot = slots[i];

                if (slot.item == item && slot.amount < item.maxStack)
                {
                    int space = item.maxStack - slot.amount;
                    int toAdd = Mathf.Min(space, amount);

                    slot.amount += toAdd;
                    amount -= toAdd;

                    Debug.Log($"[INVENTORY] Stackeado {toAdd} en slot {i}. Total en slot: {slot.amount}");

                    if (amount <= 0)
                    {
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }
        }

        // 2) Buscar slots vacíos
        for (int i = 0; i < slots.Count && amount > 0; i++)
        {
            InventorySlot slot = slots[i];

            if (slot.IsEmpty)
            {
                int toAdd = item.stackable ? Mathf.Min(item.maxStack, amount) : 1;

                slot.item = item;
                slot.amount = toAdd;
                amount -= toAdd;

                Debug.Log($"[INVENTORY] Nuevo item {item.itemName} en slot {i}. Cantidad: {slot.amount}");
            }
        }

        bool addedSomething = originalAmount != amount;

        if (addedSomething)
        {
            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.Log("[INVENTORY] No hubo espacio para agregar el item.");
        }

        // True solo si se agregó TODO
        return addedSomething && amount == 0;
    }

    /// <summary>
    /// Elimina una cantidad de un slot (si se llega a 0, limpia el slot).
    /// </summary>
    public void RemoveAt(int index, int amount = 1)
    {
        if (index < 0 || index >= slots.Count) return;
        if (amount <= 0) return;

        InventorySlot slot = slots[index];
        if (slot.IsEmpty) return;

        slot.amount -= amount;
        Debug.Log($"[INVENTORY] Removidos {amount} del slot {index}. Nuevo valor: {slot.amount}");

        if (slot.amount <= 0)
        {
            slot.Clear();
            Debug.Log($"[INVENTORY] Slot {index} quedó vacío.");
        }

        OnInventoryChanged?.Invoke();
    }

    /// <summary>
    /// Devuelve el slot indicado (o null si el índice es inválido).
    /// </summary>
    public InventorySlot GetSlot(int index)
    {
        if (index < 0 || index >= slots.Count)
            return null;

        return slots[index];
    }

    /// <summary>
    /// Limpia todo el inventario.
    /// </summary>
    public void ClearInventory()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Clear();
        }

        Debug.Log("[INVENTORY] Inventario limpiado.");
        OnInventoryChanged?.Invoke();
    }
}
