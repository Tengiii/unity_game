using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int expReward = 3;

    public delegate void EnemyDefeated(int exp);
    public static event EnemyDefeated OnMonsterDefeated;

    public int curHealth;
    public int maxHealth;

    private void Start()
    {
        curHealth = maxHealth;
    }

    public void changeHealth(int amount)
    {
        curHealth += amount;
        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        else if(curHealth <= 0)
        {
            OnMonsterDefeated?.Invoke(expReward);
            Destroy(gameObject);
        }
    }
}
