using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    public bool showLogs;

    [Header("Possible drops")]
    public List<LootTableEntry> lootTable;

    public Transform dropPosition;

    [Header("10 -> 10%, 100->100% etc...")]
    public int chanceForDrop;

    public void DropLootAtDeath()
    {
        foreach (var entry in lootTable)
        {
            int chance = Random.Range(0, 100);
            Log($"Change: {chance} vs Drop Chance: {entry.dropChance}");

            if (chance <= entry.dropChance && entry.itemPrefab != null)
            {
                dropPosition.position = new Vector2(dropPosition.position.x, dropPosition.position.y + 0.1f);
                Instantiate(entry.itemPrefab, dropPosition.position, Quaternion.identity);
                Log($"Dropped: {entry.itemPrefab.name}");
            }
        }

    }

    private void Log(string msg)
    {
        if (showLogs)
            Debug.Log(msg);
        else
            return;
    }
}
[System.Serializable]
public class LootTableEntry
{
    public GameObject itemPrefab;
    [Range(0, 100)]
    public int dropChance;
}
