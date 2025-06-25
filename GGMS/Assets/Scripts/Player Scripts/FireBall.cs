using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 dir = Vector2.right;
    public float lifeSpan = 2;
    public float speed;
    public int initDamage = 1;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    public float manaCost;

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public Animator anim;

    private int damage;
    private bool isFlying;
    private bool hitATarget;
    void Start()
    {
        rb.velocity = dir * speed;
        RotateFireball();
        damage = Mathf.CeilToInt(initDamage * StatsMgr.Instance.magic);
        Destroy(gameObject, lifeSpan);
    }

    private void RotateFireball()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<Enemy_Health>().changeHealth(-damage);
            collision.gameObject.GetComponent<Enemy_knockback>().knockback(transform, knockbackForce, knockbackTime, stunTime);
            //AttachToTarget(collision.gameObject.transform);
            anim.SetBool("hitATarget", true);
            Debug.Log("Fire ball hit the player");
            Debug.Log("Damage: "+damage);

        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            //AttachToTarget(collision.gameObject.transform);
            anim.SetBool("hitATarget", true);
            Debug.Log("Fire ball hit an obstacle");
        }
    }

    private void spendMana()
    {
        StatsMgr.Instance.curMana -= manaCost;

    }
    /*private void AttachToTarget(Transform target)
    {
        //sr.sprite = buriedSprite;
        

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        transform.SetParent(target);
    }*/
}
