using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsMgr : MonoBehaviour
{
    public static StatsMgr Instance;
    private float damageMultiplier = 1.5f;
    private float speedMultiplier = 1.05f;
    private float resilienceMultiplier = 1.1f;
    private float maxHealthMultiplier = 1.5f;
    private float healingMultiplier = 1.2f;
    private float magicMultiplier = 1.2f;
    private float manaMultiplier = 1.3f;
    private float manaRestoreMultiplier = 1.25f;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    public float resilience;
    public float magic;
    public float curMana;
    public float maxMana;
    public float manaRestore;

    [Header("Movement Stats")]
    public float speed;

    [Header("Health Stats")]
    public int maxHealth;
    public int curHealth;
    public float healingAmount = 1.0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateMaxHealth()
    {
        maxHealth += Mathf.RoundToInt(Mathf.Ceil(maxHealthMultiplier));
       
        healingAmount += healingMultiplier;

        maxHealthMultiplier += 0.2f;
        healingMultiplier += 0.035f;
        //Debug.Log(maxHealthMultiplier);
    }

    public void UpdateDamage()
    {
        damage += Mathf.RoundToInt(Mathf.Ceil(damageMultiplier));
        damageMultiplier += 0.2f;
        //Debug.Log(damageMultiplier);
    }
    public void UpdateSpeed()
    {
        speed *= speedMultiplier;
        //Debug.Log(speed);
    }

    public void UpdateResilience()
    {
        resilience *= resilienceMultiplier;
        //Debug.Log(resilience);
    }

    public void UpdateMagic()
    {
        magic *= magicMultiplier;
        maxMana *= manaMultiplier;
        manaRestore *= manaRestoreMultiplier;
        Debug.Log("Magia" + magic);
        Debug.Log("Max Mana" + maxMana);
    }
}
