using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelUpMgr : MonoBehaviour
{
    public LevelUpStats[] levelUps;
    public TMP_Text pointText;
    public int availablePoints;

    private void OnEnable()
    {
        LevelUpStats.OnStatPointSpent += HandleAbilityPointsSpent;
        //Debug.Log("Enabled");
        ExpMgr.OnLevelUp += UpdatePoints;
      
    }

    private void OnDisable()
    {
        LevelUpStats.OnStatPointSpent -= HandleAbilityPointsSpent;
        // Debug.Log("Disabled");
        ExpMgr.OnLevelUp -= UpdatePoints;
    }

    private void Start()
    {
        foreach (LevelUpStats levelUp in levelUps)
        {
            levelUp.button.onClick.AddListener(() => CheckAvailablePoints(levelUp));
            //Debug.Log("S£UCHAM");
        }
        UpdatePoints(0);
    }

    private void CheckAvailablePoints(LevelUpStats levelUp)
    {
        if (availablePoints > 0)
        {
            levelUp.UpgradeStat();
            //Debug.Log("SPRAWDZAM DOSTEPNE PUNKTY");
        }
    }

    private void HandleAbilityPointsSpent(LevelUpStats levelUp)
    {
        if (availablePoints > 0)
        {
            UpdatePoints(-1);
            //Debug.Log("Zajmuje siê wydanymi punkatmi");
        }
    }

    public void UpdatePoints(int amount)
    {
        availablePoints += amount;
        pointText.text = "Points to spend: " + availablePoints;
        //Debug.Log("ZAKTUALIZUJ PUNKTY");
    }
}
