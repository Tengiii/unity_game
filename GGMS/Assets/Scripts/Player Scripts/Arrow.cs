using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 dir = Vector2.right;
    public float lifeSpan = 2;
    public float speed;
    public int damage = 1;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public SpriteRenderer sr;
    public Sprite buriedSprite;
    void Start()
    {
        rb.velocity = dir * speed;
        RotateArrow(); 
        Destroy(gameObject, lifeSpan);
    }

    private void RotateArrow()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<Enemy_Health>().changeHealth(-damage);
            collision.gameObject.GetComponent<Enemy_knockback>().knockback(transform,knockbackForce,knockbackTime,stunTime);
            AttachToTarget(collision.gameObject.transform);

        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
    }

    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        transform.SetParent(target);
    }

}
