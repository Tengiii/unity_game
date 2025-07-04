using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void ApplyItemEffects(ItemSO itemSO)
    {
        if (itemSO.curHealth > 0)
            StatsMgr.Instance.curHealth += itemSO.curHealth;
        if (itemSO.curMana > 0)
            StatsMgr.Instance.curMana += itemSO.curMana;
        if (itemSO.maxHealth > 0)
            StatsMgr.Instance.maxHealth += itemSO.maxHealth;


        if (itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));
    }

    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (itemSO.maxHealth > 0)
            StatsMgr.Instance.maxHealth -= itemSO.maxHealth;
    }
}
