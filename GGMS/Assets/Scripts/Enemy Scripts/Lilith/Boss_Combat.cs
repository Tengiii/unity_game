using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Combat : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform shootPoint;
    public LayerMask playerLayer;
    public Transform player;
    float Timer = 0f;
    float cooldown = 2.0f;

    public void Shoot(Transform target)
    {
        if (Timer <=0)
        {
            target = target ?? player;
            Vector2 direction = (target.position - shootPoint.position).normalized;
            GameObject fireball = Instantiate(fireballPrefab, shootPoint.position, Quaternion.identity);

            FireBall fireBall = fireball.GetComponent<FireBall>();

            fireBall.dir = direction;
            fireBall.lifeSpan = 4.0f;
            fireBall.speed = 5.0f;
            Timer = cooldown;
        }
    }
    private void Update()
    {
        Timer -= Time.deltaTime;
    }
}
