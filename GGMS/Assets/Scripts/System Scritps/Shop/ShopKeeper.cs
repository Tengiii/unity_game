using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopKeeper : MonoBehaviour
{

    public static event Action<ShopMgr, bool> OnShopStateChanged;
    public ShopMgr shopMgr;

    private bool playerInRange;
    private bool isShopOpen;
    public CanvasGroup shopCanvasGroup;

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isShopOpen)
                {
                    OnShopStateChanged?.Invoke(shopMgr, true);
                    isShopOpen = true;
                    Time.timeScale = 0;
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.interactable = true;
                    shopCanvasGroup.blocksRaycasts = true;
                    
                    Debug.Log("ISO " + isShopOpen);
                }else
                {
                    OnShopStateChanged?.Invoke(shopMgr, false);
                    isShopOpen = false;
                    Time.timeScale = 1;
                    shopCanvasGroup.alpha = 0;
                    shopCanvasGroup.interactable = false;
                    shopCanvasGroup.blocksRaycasts = false;
                    Debug.Log("ISO " + isShopOpen);
                }
                
            }
        }else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("PIR " + playerInRange);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("PIR " + playerInRange);
        }
    }
}
