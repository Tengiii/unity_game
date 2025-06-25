using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBall : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject fireBallPrefab;

    private Vector2 aimDir = Vector2.right;
    public float shootCooldown = 1.5f;
    public float spellCost;
 
    private float shootTimer;

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        HandleAiming();

        if (Input.GetButtonDown("FireBall") && shootTimer <= 0 && StatsMgr.Instance.curMana >= spellCost)
        {
            Cast();
        }
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            aimDir = new Vector2(horizontal, vertical).normalized;
        }
    }

    public void Cast()
    {
        FireBall fireBall = Instantiate(fireBallPrefab, launchPoint.position, Quaternion.identity).GetComponent<FireBall>();
        fireBall.dir = aimDir;
        shootTimer = shootCooldown;
        consumeMana();
    }
    
    private void consumeMana()
    {
        StatsMgr.Instance.curMana -= spellCost;
    }
}
