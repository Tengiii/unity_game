using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightning : MonoBehaviour
{
    public GameObject lightningPrefab;

    public float castCooldown;
    public float spellCost;

    private float castTimer;

    private void Update()
    {
        castTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && castTimer <= 0 && StatsMgr.Instance.curMana >= spellCost)
        {
            Cast();
        }

    }

    public void Cast()
    {
        //dzia³a dobrze ale animacja gra jakby z srodka wiec pjorun uderza pod kursorem
        Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //a ja chce zeby uderzal tam gdzie jest kursor i dlatego jest to
        spawnPoint += new Vector3(0.0f, 1.1f, 0.0f);

        Lightning lightning = Instantiate(lightningPrefab, (Vector2)spawnPoint, Quaternion.identity).GetComponent<Lightning>();
        castTimer = castCooldown;
        ConsumeMana();
    }

    private void ConsumeMana()
    {
        StatsMgr.Instance.curMana -= spellCost;
    }

}
