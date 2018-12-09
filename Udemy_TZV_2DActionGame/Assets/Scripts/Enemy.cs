using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("'Enemy.cs' Specific:")]
    [Space(5)]
    [Header("The health of the enemy")]
    public int myHealth = 1;

    [HideInInspector]
    public Transform player;

    [Header("The speed of the enemy")]
    public float enemySpeed = 4f;

    [Header("The attack rate of the enemy in seconds")]
    public float enemyAttackRate = 1f;

    [Header("The damage the enemy deals")]
    public int enemyDamage = 1;

    [Header("The % chance of dropping a pickup")]
    public int dropPickupChance = 10;
    public int healthPickupChance = 50;

    [Header("The array of possible pickups")]
    public GameObject[] dropPickups;
    public GameObject healthPickup;

    [Header("The death effect")]
    public GameObject deathEffect;
    public GameObject blood;

    //****************************************************************************************************
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //****************************************************************************************************
    public void TakeDamage(int damageAmount)
    {
        // Subtract health
        myHealth -= damageAmount;

        // If i am dead
        if (myHealth <= 0)
        {
            if (dropPickups.Length > 0)
            {
                // Generate a random number between 0 and 100
                int randomNumber = Random.Range(0, 101);

                // If the random number is less than the chance
                if (randomNumber < dropPickupChance)
                {
                    // Assign a random pickup
                    GameObject randomPickup = dropPickups[Random.Range(0, dropPickups.Length)];

                    // Spawn in place of enemy
                    Instantiate(randomPickup, transform.position, transform.rotation);
                }
            }

            // Generate a new random number
            int randomHealth= Random.Range(0, 101);

            if (randomHealth < healthPickupChance)
            {
                // Spawn in place of enemy
                Instantiate(healthPickup, transform.position, transform.rotation);
            }

            // Play particle effect
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(blood, transform.position, Quaternion.identity);

            // Destroy me
            Destroy(gameObject);
        }
    }
}
