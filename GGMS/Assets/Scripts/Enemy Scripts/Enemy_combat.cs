using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float knockbackForce = 5.0f;
    public float stunTime = 1.0f;
    private int changeForResilience;
    private int damageAfterResilience;

    Vector2 weaponRange = new Vector2(2.0f, 1.0f);
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //if(collision.gameObject.tag == "Player")
           // collision.gameObject.GetComponent<PlayerHealth>().changeHealth(-damage);
    }

    public void Attack()
    {
        //Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position,weaponRange,1.0f, playerLayer);

        changeForResilience = Random.Range(1, 100);
        if (changeForResilience <= 20)
        {
            damageAfterResilience = damage;
            damageAfterResilience -= Mathf.CeilToInt(StatsMgr.Instance.resilience);
            if (damageAfterResilience <= 0)
            {
                Debug.Log("Resilience negated damage");
            }
            else
            {
                if (hits.Length > 0)
                {
                    hits[0].GetComponent<PlayerHealth>().changeHealth(-damageAfterResilience);
                    hits[0].GetComponent<PlayerMovement>().knockback(transform, knockbackForce, stunTime);

                }
            }

        }
        else
        {
            if(hits.Length > 0)
            {
                hits[0].GetComponent<PlayerHealth>().changeHealth(-damage);
                hits[0].GetComponent<PlayerMovement>().knockback(transform, knockbackForce, stunTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        float alpha = 0.3f;
        Gizmos.color = new Color(1f, 0f, 0f, alpha);
        Gizmos.DrawCube(attackPoint.position, weaponRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, weaponRange);
    }
}
