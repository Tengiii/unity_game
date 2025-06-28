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

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public Animator anim;
    public AudioSource soundfx;

    private int damage;

    void Start()
    {
        rb.velocity = dir * speed;
        RotateFireball();
        damage = Mathf.CeilToInt(initDamage * StatsMgr.Instance.magic);
        anim.SetBool("isFlying", true);
        soundfx = GetComponent<AudioSource>();
        soundfx.Play();
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
            anim.SetBool("isFlying", false);
            anim.SetBool("hitATarget", true);
            Debug.Log("Fire ball hit the player");
            Debug.Log("Damage: "+damage);

        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            //AttachToTarget(collision.gameObject.transform);
            anim.SetBool("isFlying", false);
            anim.SetBool("hitATarget", true);
            Debug.Log("Fire ball hit an obstacle");
        }
    }

    /*private void AttachToTarget(Transform target)
    {
        //sr.sprite = buriedSprite;
        

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        transform.SetParent(target);
    }*/
}
