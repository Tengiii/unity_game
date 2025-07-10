using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_Script : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite closedSprite;
    public Sprite openedSprite;

    public List<GameObject> items;

    private bool isOpend;
    private bool playerInRange;
    private Vector3 offset;

    private void Start()
    {
        sr.sprite = closedSprite;    
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange && !isOpend)
            OpenChest();
    }

    public void OpenChest()
    {
        isOpend = true;
        sr.sprite = openedSprite;
        var i = 0;
        
        foreach (var item in items)
        {
            if(item != null)
            {
                if(i == 0)
                    offset = new Vector3(transform.position.x, transform.position.y - 1.0f);
                else
                    offset = new Vector3(offset.x, offset.y - 0.1f);
                Instantiate(item, offset, Quaternion.identity);
            }else
            {
                return;
            }
            i++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerInRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }
}
