using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : Enemy
{
    public float stopDistance = 2f;
    public float attackSpeed = 3.5f;
    private float attackTime;

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
