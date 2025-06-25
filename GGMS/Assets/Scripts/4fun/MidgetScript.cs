using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidgetScript : MonoBehaviour
{
    public CircleCollider2D circleCollider;
    public AudioSource anthem;

    public float rotationSpeed;
    public float scaleSpeed;
    public Vector3 rotation;

    private bool canRotate;
    private float spinTimer;
    private float spinDuration;


  
 
    private void Update()
    {
        if (canRotate)
        {
            transform.Rotate(rotation * Time.deltaTime * rotationSpeed);
            if (spinTimer <= 0)
            {
                SetNewSpinDuration();
            }
        }
        spinTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anthem.Play();
            canRotate = true;
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anthem.Pause();
            canRotate = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void SetNewSpinDuration()
    {
        spinDuration = Random.Range(1, 5);
        spinTimer = spinDuration;
        rotationSpeed *= -1;
    }
  
}


