using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;

    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    private RectTransform infoPanelRect;


    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSO itemSO)
    {
        infoPanel.alpha = 1;
        itemNameText.text = itemSO.itemName;
        itemDescriptionText.text = itemSO.itemDescription;
    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    public void FollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 offset = new Vector3(10, 10, 0);
        infoPanelRect.position = mousePos + offset;
    }

}
