using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Movement : MonoBehaviour
{
    public float speed;
    public float attackRange;
    public float attackCooldown;
    public float playerDetectRange;
    
    private int facingDir = 1;
    private float attackCooldownTimer;

    public Transform detectionPoint;
    public LayerMask playerLayer;

    private BossState state;
    
    public Animator anim;
    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(BossState.Idle);
    }

    void Update()
    {

        CheckForPlayer();

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (state == BossState.Walk)
        {
            FollowPlayer();
        }

        else if (state == BossState.Cast)
        {
            rb.velocity = Vector2.zero;
            if (player.position.x > transform.position.x && facingDir == -1 ||
                player.position.x < transform.position.x && facingDir == 1)
            {
                Flip();
            }
        }
    }

    void FollowPlayer()
    {
        Debug.Log("Following Player");
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
            ChangeState(BossState.Idle);
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
                ChangeState(BossState.Cast);
                attackCooldownTimer = attackCooldown;
                Debug.Log("Boss is Casting");
            }
            else if (Vector2.Distance(transform.position, player.transform.position) > attackRange && state != BossState.Cast)
            {
                ChangeState(BossState.Walk);
                Debug.Log("Boss is Walking");
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(BossState.Idle);
            Debug.Log("Boss Idles");
        }
    }

    public void ChangeState(BossState newState)
    {
        state = newState;
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isCasting", false);
        if (state == BossState.Idle)
            anim.SetBool("isIdle", true);
        else if (state == BossState.Walk)
            anim.SetBool("isWalking", true);
        else if (state == BossState.Cast)
            anim.SetBool("isCasting", true);
    }
}

public enum BossState
{
    Idle,
    Walk,
    Cast
}