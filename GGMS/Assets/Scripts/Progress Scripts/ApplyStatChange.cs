using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStatChange : MonoBehaviour
{
    private void OnEnable()
    {
        LevelUpStats.OnStatPointSpent += HandleAbilityPointsSpent;
    }

    private void OnDisable()
    {
        LevelUpStats.OnStatPointSpent -= HandleAbilityPointsSpent;
    }

    private void HandleAbilityPointsSpent(LevelUpStats levelUp)
    {
        string statName = levelUp.levelSO.statName;

        switch (statName) 
        {
            case "Health":
                StatsMgr.Instance.UpdateMaxHealth();
                break;
            case "Damage":
                StatsMgr.Instance.UpdateDamage();
                break;
            case "Speed":
                StatsMgr.Instance.UpdateSpeed();
                break;
            case "Resilience":
                StatsMgr.Instance.UpdateResilience();
                break;
            case "Magic":
                StatsMgr.Instance.UpdateMagic();
                break;
            default:
                Debug.LogWarning("Unknown stat: " + statName);
                break;
        }

    }
}
