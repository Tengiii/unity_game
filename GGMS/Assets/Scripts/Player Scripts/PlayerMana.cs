using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    public Slider slider;
    public float manaRestoreCooldown;
    
    private float manaRestoreTimer;
    

    private void Start()
    {
        UpdateUI();
        
    }
    private void Update()
    {
        manaRestoreTimer -= Time.deltaTime;
        RegMana();    
        StartCoroutine(refreshManaReg());
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
            if (Input.GetButtonDown("manaRestore") && manaRestoreTimer <= 0)
            {
                StatsMgr.Instance.curMana += StatsMgr.Instance.manaRestore;
                manaRestoreTimer = manaRestoreCooldown;
            }
        }
        else
        {
            return;
        }

    }
    private IEnumerator refreshManaReg()
    {
        UpdateUI();
        yield return new WaitForSeconds(1);
    }
}
