using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LevelUpStats : MonoBehaviour
{
    public LevelSO levelSO;

    public TMP_Text levelText;
    public Button button;

    public int curLevel = 1;
    public static event Action<LevelUpStats> OnStatPointSpent;

    private void OnValidate()
    {
        if (levelSO != null && levelText != null)
        {
            UpdateUI();
            //Debug.Log("Zwalidowane");
        }
    }
    public void UpgradeStat()
    {
        //Debug.Log("ULEPSZ");
        curLevel++;
        OnStatPointSpent?.Invoke(this);
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        //Debug.Log("ZAKTUALIZUJ UI");
        button.interactable = true;
        levelText.text = levelSO.statName + ": " + curLevel.ToString();  
    }

   

}
