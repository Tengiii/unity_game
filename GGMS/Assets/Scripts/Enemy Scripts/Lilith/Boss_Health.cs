using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss_Health : MonoBehaviour
{
    [HideInInspector]public int curHealth;
    [HideInInspector]public int maxHealth;

    private float timer;
    private float healCooldown;

    public Slider slider;
    public CanvasGroup canvasGroup;
    
   

    private void Start()
    {
        maxHealth = 300;
        curHealth = maxHealth;
        healCooldown = 15.0f;

        slider.maxValue = maxHealth;
        slider.value = curHealth;
        slider.minValue = 0;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        Heal();
    }

    public void DamageTheBoss(int amount)
    {
        curHealth -= amount;
        UpdateUI();
    }

    private void Heal()
    {
        if (timer <= 0)
        {
            curHealth += 30;
            timer = healCooldown;
            UpdateUI();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvasGroup.alpha = 1;
            UpdateUI();
        }

    }

    private void UpdateUI()
    {
        slider.value = curHealth;
    }
}
