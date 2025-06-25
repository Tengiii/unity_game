using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        UpdateUI();
    }
    private void Update()
    {
        RegMana();    
    }

    public void changeMana(float amount)
    {
        StatsMgr.Instance.curMana += amount;
        UpdateUI();
    }
    public void UpdateUI()
    {
        slider.maxValue = StatsMgr.Instance.maxMana;
        slider.value = StatsMgr.Instance.curMana;
    }

    private void RegMana()
    {
        if (StatsMgr.Instance.curMana < StatsMgr.Instance.maxMana)
        {
            StatsMgr.Instance.curMana += 0.3f * Time.deltaTime;
            Debug.Log(StatsMgr.Instance.curMana);
        }
        else
        {
            return;
        }

    }
}
