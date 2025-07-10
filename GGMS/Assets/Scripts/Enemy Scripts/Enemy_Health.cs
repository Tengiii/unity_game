using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour
{
    public delegate void EnemyDefeated(int exp);
    public static event EnemyDefeated OnMonsterDefeated;
    
    public int expReward = 3;
    public int curHealth;
    public int maxHealth;
    [SerializeField] enemy_healthbar healthBar;

    private DropLoot dropLoot;
    private void Awake()
    {
        healthBar = GetComponentInChildren<enemy_healthbar>();
        dropLoot = GetComponent<DropLoot>();
    }
    private void Start()
    {
        curHealth = maxHealth;
        healthBar.UpdateHealthBar(curHealth, maxHealth);
    }

    public void changeHealth(int amount)
    {
        curHealth += amount;
        healthBar.UpdateHealthBar(curHealth, maxHealth);
        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        else if(curHealth <= 0)
        {
            OnMonsterDefeated?.Invoke(expReward);
            dropLoot?.DropLootAtDeath();
            Destroy(gameObject);
        }
    }
}