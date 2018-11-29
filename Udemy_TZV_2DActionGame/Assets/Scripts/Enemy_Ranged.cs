using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged : Enemy
{
    [Space(20)]
    [Header("'Enemy_Ranged.cs' Specific:")]
    [Space(5)]
    [Header("The distance the enemy can be before melee attacking")]
    public float stopDistance = 2f;

    [Header("The transform to shoot from")]
    public Transform barrel;

    [Header("The prefab bullet to shoot")]
    public GameObject bulletToShoot;

    private float attackTime = 0f;
    private Animator myAnimator;

    //****************************************************************************************************
    public override void Start()
    {
        // Call start function from inherited class
        base.Start();

        // Assign animator
        myAnimator = GetComponent<Animator>();
    }

    //****************************************************************************************************
    private void Update()
    {
        // If the player is not dead
        if (player != null)
        {
            // If we are close to the player
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
            }

            if (Time.time >= attackTime)
            {
                attackTime = Time.time + enemyAttackRate;

                myAnimator.SetTrigger("Attack");
            }
        }
    }

    //****************************************************************************************************
    private void RangedAttack()
    {
        // Point the weapon to the players mouse
        Vector2 direction = player.position - barrel.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        barrel.rotation = rotation;

        Instantiate(bulletToShoot, barrel.position, barrel.rotation);
    }
}
