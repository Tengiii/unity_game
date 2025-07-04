using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{

    public string itemName;
    public string itemDescription;
    public Sprite icon;
    public bool isGold;
    public int stackSize = 3;

    [Header("Stats")]
    public int curHealth;
    public float curMana;
    public int maxHealth;
    public float speed;
    public int damage;

    [Header("Temporary items")]
    public float duration;

}
