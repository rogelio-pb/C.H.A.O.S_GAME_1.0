using UnityEngine;
using UnityEngine.UI;
using TMPro;   // 👈 importante

public class InventorySlotUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text amountText;   // 👈 antes era Text

    [HideInInspector] public int slotIndex;
    private Inventory inventory;

    public void Setup(Inventory inv, int index)
    {
        inventory = inv;
        slotIndex = index;
        Refresh();
    }

    public void Refresh()
    {
        if (inventory == null) return;

        InventorySlot slot = inventory.GetSlot(slotIndex);
        if (slot == null || slot.IsEmpty)
        {
            iconImage.enabled = false;
            amountText.text = "";
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = slot.item.icon;

            amountText.text = slot.item.stackable && slot.amount > 1
                ? slot.amount.ToString()
                : "";
        }
    }

    public void OnClickSlot()
    {
        InventorySlot slot = inventory.GetSlot(slotIndex);
        if (slot == null || slot.IsEmpty) return;

        Debug.Log("Click en slot: " + slotIndex + " - " + slot.item.itemName);
        // inventory.RemoveAt(slotIndex, 1);
    }
}
