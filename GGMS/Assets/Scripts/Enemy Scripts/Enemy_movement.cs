using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_movement : MonoBehaviour
{
    public float speed = 4;
    public float attackRange = 1.0f;
    public float attackCooldown = 2.0f;
    public float playerDetectRange = 5.0f;
    
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private int facingDir = 1;
    private EnemyState enemyState;
    private float attackCooldownTimer;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();  
        ChangeState(EnemyState.Idle);
    }

   
    void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            CheckForPlayer();

            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }

            if (enemyState == EnemyState.Moving)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Chase()
    {
        if (player.position.x > transform.position.x && facingDir == -1 || player.position.x < transform.position.x && facingDir == 1)
        {
            Flip();
        }
        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }


    void Flip()
    {
        facingDir *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.transform.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Moving);
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }    
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == EnemyState.Idle)
            anim.SetBool("Idle", false);
        else if (enemyState == EnemyState.Moving)
            anim.SetBool("Moving", false); 
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("Attacking", false);

        enemyState = newState;

        if (enemyState == EnemyState.Idle)
            anim.SetBool("Idle", true);
        else if (enemyState == EnemyState.Moving)
            anim.SetBool("Moving", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("Attacking", true);
    }

}

public enum EnemyState { Idle, Moving, Attacking, Knockback}

