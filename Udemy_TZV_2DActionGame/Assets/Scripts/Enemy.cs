using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int myHealth = 100;

    [HideInInspector]
    public Transform player;

    public float enemySpeed = 4f;

    public float enemyAttackRate = 1f;

    public int enemyDamage = 40;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damageAmount)
    {
        myHealth -= damageAmount;

        if (myHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
