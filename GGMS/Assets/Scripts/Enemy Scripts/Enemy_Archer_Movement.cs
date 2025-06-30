using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer_Movement : MonoBehaviour
{
    public float speed;
    public float attackRange;
    public float attackCooldown;
    public float playerDetectRange;

    public Transform detectionPoint;
    public LayerMask playerLayer;

    private int facingDir = 1;
    private ArcherState state;
    private float attackCooldownTimer;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(ArcherState.Idle);
    }

    void Update()
    {
 
        CheckForPlayer();

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (state == ArcherState.Run)
        {
            FollowPlayer();
        }

        else if (state == ArcherState.Shooting)
        {
            rb.velocity = Vector2.zero;
            if (player.position.x> transform.position.x && facingDir == -1 ||
                player.position.x < transform.position.x && facingDir == 1)
            {
                Flip();
            }
        } 
    }

    void FollowPlayer()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > attackRange * 0.7f)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.velocity = dir * speed;

            if ((player.position.x > transform.position.x && facingDir == -1) ||
                (player.position.x < transform.position.x && facingDir == 1))
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(ArcherState.Idle);
        }
    }

    void Flip()
    {
        facingDir *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer()
    {
        Collider2D[] enter = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (enter.Length > 0)
        {
            player = enter[0].transform;

            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && attackCooldownTimer <= 0)
            {
                ChangeState(ArcherState.Shooting);
                attackCooldownTimer = attackCooldown;
            }
            else if (Vector2.Distance(transform.position, player.transform.position) > attackRange && state != ArcherState.Shooting)
            {
                ChangeState(ArcherState.Run);
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(ArcherState.Idle);
        }
    }


    public void ChangeState(ArcherState newState)
    {

        state = newState;
        anim.SetBool("Idle", false);
        anim.SetBool("Run", false);
        anim.SetBool("Shooting", false);
        if (state == ArcherState.Idle)
            anim.SetBool("Idle", true);
        else if (state == ArcherState.Run)
            anim.SetBool("Run", true);
        else if (state == ArcherState.Shooting)
            anim.SetBool("Shooting", true);
    }
    void OnDrawGizmosSelected()
    {
        if (detectionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
        }
    }
}


public enum ArcherState
{
    Idle,
    Run,
    Shooting
}