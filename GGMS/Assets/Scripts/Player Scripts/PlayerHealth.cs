using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Animator outlinesAnim;
    public Slider healthSlider;
    public Slider potionCooldownSlider;

    private float healingCooldown = 15.0f;
    private float healingTimer;
    private int tempHp;
    //smirec
    public GameObject deathScreen;
    private bool zmar;

    private void Start()
    {
        UpdateUI();
    }

    public void healThePlayer()
    {
        if (zmar)
        {
            return;
        }
        if (healingTimer <= 0)
        {
            StatsMgr.Instance.curHealth += Mathf.CeilToInt(StatsMgr.Instance.healingAmount);
            if (StatsMgr.Instance.curHealth > StatsMgr.Instance.maxHealth)
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
        UpdateUI();
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
    //umieranie
    public void Die()
    {
        zmar = true;
        deathScreen.SetActive(true);
    }
    //respawn
    public void Respawn()
    {
        StatsMgr.Instance.curHealth = StatsMgr.Instance.maxHealth;
        UpdateUI();
        zmar = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        deathScreen.SetActive(false);
    }
    public void changeHealth(int amount)
    {
        StatsMgr.Instance.curHealth += amount;
        Sound_Mgr.PlaySound(SoundType.HURT, 0.1f);
        outlinesAnim.Play("OnDmgOutlines");

        if (StatsMgr.Instance.curHealth <= 0 && !zmar)
        {
            gameObject.SetActive(false);
            Die();
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