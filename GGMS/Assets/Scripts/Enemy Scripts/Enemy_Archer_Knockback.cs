using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Archer_Movement enemy_archer_movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_archer_movement = GetComponent<Enemy_Archer_Movement>();

    }

    public void knockback(Transform playerTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemy_archer_movement.ChangeState(ArcherState.Knockback);
        StartCoroutine(stunTimer(stunTime, knockbackTime));
        Vector2 dir = (transform.position - playerTransform.position).normalized;
        rb.velocity = dir * knockbackForce;
    }

    IEnumerator stunTimer(float stunTime, float knockbackTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_archer_movement.ChangeState(ArcherState.Idle);
    }

}
