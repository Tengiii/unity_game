using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ShopMgr : MonoBehaviour
{
    public static event Action<ShopMgr, bool> OnShopStateChanged;

    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private ShopSlot[] shopSlots;
    [SerializeField] private Inventory_Mgr inventoryMgr;
    

    private void Start()
    {
        PopulateShopItems();
        OnShopStateChanged?.Invoke(this, true);
    }

    public void PopulateShopItems()
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++) 
        {
            ShopItems shopItem = shopItems[i];          //shopItemS <-- ze lista ta od gory a nie ten shopItem co jest w tej lini
            shopSlots[i].Initialize(shopItem.itemSO, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }

        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(ItemSO itemSO, int price)
    {
        if(itemSO != null && inventoryMgr.gold >= price)
        {
            if (HasSpaceForItem(itemSO))
            {
                inventoryMgr.gold -= price;
                inventoryMgr.goldText.text = inventoryMgr.gold.ToString();
                inventoryMgr.AddItem(itemSO, 1);
            }
        }
    }

    private bool HasSpaceForItem(ItemSO itemSO)
    {
        foreach (var slot in inventoryMgr.itemSlots)
        {
            if (slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
                return true;
            else if (slot.itemSO == null)
                return true;
        }
        return false;
    }

    public void SellItem(ItemSO itemSO)
    {
        if (itemSO == null)
            return;

        foreach (var slot in shopSlots)
        {
            if(slot.itemSO == itemSO)
            {
                inventoryMgr.gold += slot.price / 2;
                inventoryMgr.goldText.text = inventoryMgr.gold.ToString();
                return;
            }
        }
    }


}

[System.Serializable]
public class ShopItems 
{
    public ItemSO itemSO;
    public int price;

}
