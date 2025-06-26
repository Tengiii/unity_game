using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Animator outlinesAnim;
    public Slider healthSlider;
    public Slider potionCooldownSlider;

    private float healingCooldown = 15.0f;
    private float healingTimer;

    private void Start()
    {
        UpdateUI();
    }

    public void healThePlayer()
    {
        if (healingTimer <= 0)
        {
            StatsMgr.Instance.curHealth += Mathf.CeilToInt(StatsMgr.Instance.healingAmount);
            if(StatsMgr.Instance.curHealth > StatsMgr.Instance.maxHealth)
            {
                StatsMgr.Instance.curHealth = StatsMgr.Instance.maxHealth;
            }

            healingTimer = healingCooldown;
            UpdateUI();
        }
        else
        {
            return;
        }
       
    }
    private void Update()
    {
        if (healingTimer > 0)
        {
            healingTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Healing"))
        {
            healThePlayer();
        }
        UpdateCooldownsUI();
    }

    public void changeHealth(int amount)
    {
        StatsMgr.Instance.curHealth += amount;
        outlinesAnim.Play("OnDmgOutlines");
        
        if (StatsMgr.Instance.curHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        if (StatsMgr.Instance.curHealth > StatsMgr.Instance.maxHealth)
        {
            StatsMgr.Instance.curHealth = StatsMgr.Instance.maxHealth;
        }

        UpdateUI();
    }
    public void UpdateUI()
    {
        healthSlider.maxValue = StatsMgr.Instance.maxHealth;
        healthSlider.value = StatsMgr.Instance.curHealth;
        
    }
    public void UpdateCooldownsUI()
    {
        potionCooldownSlider.maxValue = healingCooldown;
        potionCooldownSlider.value = healingTimer;
    }
  
}
