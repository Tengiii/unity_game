using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO itemSO;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;

    private Inventory_Mgr inventoryMgr;
    private static ShopMgr activeShop;

    private void Start()
    {
        inventoryMgr = GetComponentInParent<Inventory_Mgr>();
    }

    private void OnEnable()
    {
        ShopKeeper.OnShopStateChanged += HandleShopStateChanged;
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopStateChanged -= HandleShopStateChanged;
    }


    private void HandleShopStateChanged(ShopMgr shopMgr, bool isOpen)
    {
        //activeShop = isOpen ? shopMgr : null;
       
        if (isOpen)
            activeShop = shopMgr;
        else
            activeShop = null;
        Debug.Log("Shop state changed: " + isOpen);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if(activeShop != null)
                {
                    activeShop.SellItem(itemSO);
                    quantity--;
                    UpdateUI();
                }else
                {
                    inventoryMgr.UseItem(this);
                }
               
            }
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryMgr.DropItem(this);
            }
        }
    }

    public void UpdateUI()
    {
        if(quantity <=0)
            itemSO = null;
        

        if (itemSO != null)
        {
            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
        

    }
}
