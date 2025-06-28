using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Enemy_Health;

public class Enemy_Spawn : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public float spawnRadius = 20f;
    public float time = 10f;

    public int maxE = 10;
    private int count = 1;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, time);
    }
    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += kill;
    }
    void SpawnEnemy()
    {
        if (count >= maxE) return;

        Vector2 sp = transform.position + UnityEngine.Random.insideUnitSphere * spawnRadius;
        Instantiate(enemyPrefabs, sp, Quaternion.identity);
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
}