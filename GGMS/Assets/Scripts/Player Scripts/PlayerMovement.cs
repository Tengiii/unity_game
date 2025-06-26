using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int facingDir = 1;

    public Rigidbody2D rb;
    public Animator anim;
    public PlayerCombat playerCombat;
    public TrailRenderer tr;
    public Slider dashCooldownSlider;

    private bool isKnockedBack;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 25.0f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 5.0f;
    private float dashingTimer;

    private void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            playerCombat.Attack();
        }

        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(DashHorizontal());
            dashingTimer = dashingCooldown;
        }
        dashingTimer -= Time.deltaTime;
        UpdateUI();
    }


    void FixedUpdate()
    {   
        if(!isKnockedBack && !isDashing) { 
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            rb.velocity = new Vector2(horizontal,vertical) * StatsMgr.Instance.speed;
            
        }
    }

    void Flip()
    {
        facingDir *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void knockback(Transform enemy, float knockbackForce, float stunTime)
    {
        isKnockedBack = true;
        Vector2 dir = (transform.position - enemy.transform.position).normalized;
        rb.velocity = dir * knockbackForce;
        StartCoroutine(KnockbackCounter(stunTime));
    }

    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
    
    private void UpdateUI()
    {
        dashCooldownSlider.maxValue = dashingCooldown;
        dashCooldownSlider.value = dashingTimer;
    }

    private IEnumerator DashHorizontal()
    { 
        Debug.Log("DASH");
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
