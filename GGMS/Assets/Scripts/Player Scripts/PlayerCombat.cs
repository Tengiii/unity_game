using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public StatsUI statsUI;
    public Animator anim;

    public float cooldown = 1.2f;
    
    private float timer;

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {   
        if(timer <= 0) { 
            anim.SetBool("isAttacking", true);
            Sound_Mgr.PlaySound(SoundType.SLASH, 0.1f);
            timer = cooldown;
        }
    }

    public void DealDamage()
    {
       

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsMgr.Instance.weaponRange, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.isTrigger) continue;

            Debug.Log(enemy); 
            Sound_Mgr.PlaySound(SoundType.SWORD_HIT,0.2f);
            enemy.GetComponent<Enemy_Health>().changeHealth(-StatsMgr.Instance.damage);
            enemy.GetComponent<Enemy_knockback>().knockback(transform, StatsMgr.Instance.knockbackForce, StatsMgr.Instance.knockbackTime, StatsMgr.Instance.stunTime);
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmos()
    {
        float alpha = 0.2f;
        Gizmos.color = new Color(1f, 0f, 0f, alpha);
        Gizmos.DrawSphere(attackPoint.position, StatsMgr.Instance.weaponRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPoint.position, StatsMgr.Instance.weaponRange);
    }
    

}
