using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 dir = Vector2.right;
    public float lifeSpan = 2f;
    public float speed = 10f;
    public int damage = 1;
    public LayerMask playerLayer;
    public SpriteRenderer sr;
    public Sprite buriedSprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir.normalized * speed;
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

        if ((playerLayer.value> 0))
        {
            var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.changeHealth(-damage);

            AttachToTarget(collision.transform);
        }
    }
    public void Launch(Vector2 direction)
    {
        dir = direction;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir.normalized * speed;
        RotateArrow();
        Destroy(gameObject, lifeSpan);
    }

    private void AttachToTarget(Transform target)
    {
        if (sr != null && buriedSprite != null)
            sr.sprite = buriedSprite;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        transform.SetParent(target);
        Destroy(gameObject, lifeSpan);
    }
    void OnDrawGizmos()
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);

    }

}