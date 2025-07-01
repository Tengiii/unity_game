using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Loot : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer sr;
    public Animator anim;

    public static event Action<ItemSO, int> OnItemPicked;
    public int quantity;

    private void OnValidate()
    {
        if(itemSO == null)
        {
            return;
        }
        sr.sprite = itemSO.icon;
        this.name = itemSO.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("LootPickUp");
            OnItemPicked?.Invoke(itemSO,quantity);
            Destroy(gameObject, 0.5f);
        }
    }
}
