using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer_Combat : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public LayerMask playerLayer;
    public Transform player;
    float Timer = 0f;
    float cooldown = 2.5f;

    public void Shoot(Transform target)
    {
        target = target ?? player;
        Vector2 direction = (target.position - shootPoint.position).normalized;
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);

        Arrow arrowScript = arrow.GetComponent<Arrow>();

        arrowScript.dir = direction;
        arrowScript.playerLayer = playerLayer;
        arrowScript.Launch(direction);
        Timer=cooldown;
    }
    private void Update()
    {
       
    }
}