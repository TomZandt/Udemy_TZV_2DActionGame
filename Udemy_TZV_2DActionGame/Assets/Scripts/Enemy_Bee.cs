using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : Enemy
{
    [Space(20)]
    [Header("'Enemy_Bee.cs' Specific:")]
    [Space(5)]
    [Header("The speed of the melee attack")]
    public float attackSpeed = 3.5f;
    
    [Header("The distance the enemy can be before melee attacking")]
    public float stopDistance = 2f;

    private float attackTime = 0f;

    //****************************************************************************************************
    private void Update()
    {
        // IF the player is not dead
        if (player != null)
        {
            // If we are far from the player
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= attackTime)
                {
                    StartCoroutine(Attack());

                    attackTime = Time.time + enemyAttackRate;
                }
            }
        }
    }

    //****************************************************************************************************
    IEnumerator Attack()
    {
        player.GetComponent<MainCharacter>().TakeDamage(enemyDamage);

        Vector2 originalPositon = transform.position;

        Vector2 targetPosition = player.position;

        float animationPercent = 0f;

        while (animationPercent <= 1)
        {
            animationPercent += Time.deltaTime * attackSpeed;

            float formula = (-Mathf.Pow(animationPercent, 2) + animationPercent) * 4;

            transform.position = Vector2.Lerp(originalPositon, targetPosition, formula);

            yield return null;
        }
    }
}
