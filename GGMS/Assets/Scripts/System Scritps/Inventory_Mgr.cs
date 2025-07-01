using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Mgr : MonoBehaviour
{
    public InventorySlot[] itemSlots;
    public int gold;
    public TMP_Text goldText;

    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUI();
        }
    }

    private void OnEnable()
    {
        Loot.OnItemPicked += AddItem;
    }
    private void OnDisable()
    {
        Loot.OnItemPicked -= AddItem;
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }else
        {
            foreach (var slot in itemSlots)
            {
                if (slot.itemSO == null)
                {
                    slot.itemSO = itemSO;
                    slot.quantity = quantity;
                    slot.UpdateUI();
                    return;
                }
            }
            
        }
    }
}
