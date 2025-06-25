using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    private Vector2 aimDir = Vector2.right;
    public float shootCooldown = 0.5f;
    public float shootTimer;

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        HandleAiming();

        if (Input.GetButtonDown("ShootArrow") && shootTimer <= 0)
        {
             Shoot();
        }
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(horizontal !=0 || vertical != 0)
        {
            aimDir = new Vector2(horizontal, vertical).normalized;
        }
    }

    public void Shoot()
    {
        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.dir = aimDir;
        shootTimer = shootCooldown;
    }
}
