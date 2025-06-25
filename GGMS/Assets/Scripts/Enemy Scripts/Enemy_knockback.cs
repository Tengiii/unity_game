using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_movement enemy_movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_movement = GetComponent<Enemy_movement>();
    }

    public void knockback(Transform playerTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemy_movement.ChangeState(EnemyState.Knockback);
        StartCoroutine(stunTimer(stunTime,knockbackTime));
        Vector2 dir = (transform.position - playerTransform.position).normalized;
        rb.velocity = dir * knockbackForce;
    }

    IEnumerator stunTimer(float stunTime, float knockbackTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_movement.ChangeState(EnemyState.Idle);
    }

}
