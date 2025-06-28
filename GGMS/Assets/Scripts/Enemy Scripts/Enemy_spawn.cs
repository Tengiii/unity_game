using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Enemy_Health;

public class Enemy_Spawn : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public Transform player;
    
    public float spawnRadius;
    public float time = 10f;
    public bool isLimited;
    public int maxE = 10;
    
    private int limitCounter;
    private bool limitReached;
    private float activationDistance = 20f;
    private int count = 1;
    private float curDistance;
    private bool spawnStarted = false;
    private float inactiveTimer = 0f;
    private float maxInactiveTime = 20f;

    private List<GameObject> spawnedEnemies = new List<GameObject>();


    private void Update()
    {
        curDistance = Vector2.Distance(transform.position, player.transform.position);

        if (curDistance > activationDistance)
        {
            inactiveTimer += Time.deltaTime;

            if (inactiveTimer >= maxInactiveTime)
            {
                RemoveAllEnemies();
                inactiveTimer = 0f; // Reset po czyszczeniu
            }
        }
        else
        {
            inactiveTimer = 0f;
        }
    }
    void Start()
    {
        //curDistance = 25f;
        StartCoroutine(ManageSpawningByDistance());
    }
    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += kill;
    }
    void SpawnEnemy()
    {
        if (count >= maxE || limitReached) return;

        if (isLimited)
        {
            if (limitCounter >= maxE)
            {
                limitReached = true;
                Debug.Log("Limit reached");
                return;
            }
            limitCounter++;
            
        }
        Vector2 sp = transform.position + UnityEngine.Random.insideUnitSphere * spawnRadius;
        GameObject enemy = Instantiate(enemyPrefabs, sp, Quaternion.identity);
        spawnedEnemies.Add(enemy);
        count++;

    }
    private void kill(int exp)
    {
        count--;
        count = Mathf.Max(0, count);
    }
    private void OnDrawGizmosSelected()
    {
        float alpha = 0.2f;
        Gizmos.color = new Color(1f, 0f, 1f, alpha);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }

    private void RemoveAllEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear();
        count = 0;
        limitCounter = 0;
        limitReached = false;
        Debug.Log("Enemies were removed since they were inactive for too long");
    }

    /*private IEnumerator ManageSpawningByDistance()
    {
        while (true)
        {
            curDistance = Vector2.Distance(transform.position, player.transform.position);

            if (curDistance <= activationDistance)
            {
                inactiveTimer = 0f;
                if (!spawnStarted)
                {
                    InvokeRepeating("SpawnEnemy", 2f, time);
                    spawnStarted = true;
                }
            }
            else 
            {
                inactiveTimer += 1f;
                if(spawnStarted)
                {
                    CancelInvoke("SpawnEnemy");
                    spawnStarted = false;
                }
                if (inactiveTimer >= maxInactiveTime)
                {
                    RemoveAllEnemies();
                }
            }

            yield return new WaitForSeconds(1f); // co sekundê sprawdzamy
        }
    }*/
    private IEnumerator ManageSpawningByDistance()
    {
        while (true)
        {
            if (curDistance <= activationDistance)
            {
                if (!spawnStarted)
                {
                    InvokeRepeating("SpawnEnemy", 2f, time);
                    spawnStarted = true;
                }
            }
            else
            {
                if (spawnStarted)
                {
                    CancelInvoke("SpawnEnemy");
                    spawnStarted = false;
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

}