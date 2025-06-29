using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsUI : MonoBehaviour
{
    public GameObject[] StatSlot;
    public CanvasGroup statsCanvas;

    public bool statsOpened = false;

    private void Start()
    {
        UpdateAllStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (statsOpened)
            {
                Time.timeScale = 1; //resumes the time | the game is not stopped
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsOpened = false;
            }
            else
            {
                Time.timeScale = 0; //stops the time | the game is stopped
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsOpened = true;
            }   
        }
    }

    public void UpdateDamage()
    {
        StatSlot[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsMgr.Instance.damage;
    }
    public void UpdateHealth()
    {
        StatSlot[1].GetComponentInChildren<TMP_Text>().text = "Health: " + StatsMgr.Instance.maxHealth;
    }
    public void UpdateSpeed()
    {
        decimal decimalSpeed = Math.Round((decimal)StatsMgr.Instance.speed, 2);
        StatSlot[2].GetComponentInChildren<TMP_Text>().text = "Speed: " + decimalSpeed;
    }
    public void UpdateMagic()
    {
        decimal decimalMagic = Math.Round((decimal)StatsMgr.Instance.magic, 2);
        StatSlot[3].GetComponentInChildren<TMP_Text>().text = "Magic dmg: " + decimalMagic;
    }
    public void UpdateMana()
    {
        decimal decimalMana = Math.Round((decimal)StatsMgr.Instance.maxMana, 2);
        StatSlot[4].GetComponentInChildren<TMP_Text>().text = "Max mana: " + decimalMana;
    }
    public void UpdateResilience()
    {
        decimal decimalResilience = Math.Round((decimal)StatsMgr.Instance.resilience, 2);
        StatSlot[5].GetComponentInChildren<TMP_Text>().text = "Resilience: " + decimalResilience;
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateHealth();
        UpdateSpeed();
        UpdateMagic();
        UpdateMana();
        UpdateResilience();
    }
}
