using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("'Enemy.cs' Specific:")]
    [Space(5)]
    [Header("The health of the enemy")]
    public int myHealth = 100;

    [HideInInspector]
    public Transform player;

    [Header("The speed of the enemy")]
    public float enemySpeed = 4f;

    [Header("The attack rate of the enemy in seconds")]
    public float enemyAttackRate = 1f;

    [Header("The damage the enemy deals")]
    public int enemyDamage = 40;

    //****************************************************************************************************
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //****************************************************************************************************
    public void TakeDamage(int damageAmount)
    {
        myHealth -= damageAmount;

        if (myHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
