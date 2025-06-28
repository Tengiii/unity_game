using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Animator anim;
    public LayerMask enemyLayer;
    public AudioSource audiofx;

    public float lifeSpan = 0.15f;
    public int initDamage;
    public float stunTime;

    private int damage;

    private void Start()
    {
        damage = Mathf.CeilToInt(initDamage * StatsMgr.Instance.magic);
        anim.SetBool("isStrinking", true);
        audiofx = GetComponent<AudioSource>();
        audiofx.Play();
        Destroy(gameObject, lifeSpan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        { 
            if(collision is CapsuleCollider2D)
            {
                collision.gameObject.GetComponent<Enemy_Health>().changeHealth(-damage);
                collision.gameObject.GetComponent<Enemy_knockback>().knockback(transform, 0.0f, 0.0f, stunTime);
                Debug.Log("Lightning hit an enemy and dealt: " + damage + " damage");
            }
        }
    }
}
