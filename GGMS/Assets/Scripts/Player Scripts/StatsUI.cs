using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public void UpdateSpeed()
    {
        StatSlot[2].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsMgr.Instance.speed;
    }
    public void UpdateHealth()
    {
        StatSlot[1].GetComponentInChildren<TMP_Text>().text = "Health: " + StatsMgr.Instance.maxHealth;
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateHealth();
        UpdateSpeed();
    }
}
