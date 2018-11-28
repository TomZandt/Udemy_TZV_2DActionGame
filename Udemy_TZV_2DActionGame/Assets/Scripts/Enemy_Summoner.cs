using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Summoner : Enemy
{
    [Space(20)]
    [Header("'Enemy_Summoner.cs' Specific:")]
    [Space(5)]
    [Header("World Area for the summon position")]
    public float minWorldX = -11f;
    public float maxWorldX = 11f;
    public float minWorldY = -5f;
    public float maxWorldY= 5f;

    [Header("The rate at which the summomer spawns enemies in seconds")]
    public float summonRate = 5f;

    [Header("The enemy prefab to spawn")]
    public GameObject enemyToSummon;

    [Header("The speed of the melee attack")]
    public float attackSpeed = 3.5f;

    [Header("The distance the enemy can be before melee attacking")]
    public float stopDistance = 2f;

    private Vector2 targetSummonPosition;
    private Animator myAnimator;
    private float summonTime = 0f;
    private float attackTime = 0f;

    //****************************************************************************************************
    public override void Start()
    {
        // Call start function from inherited class
        base.Start();

        // Setup random summon location
        float randomX = Random.Range(minWorldX, maxWorldX);
        float randomY = Random.Range(minWorldY, maxWorldY);
        targetSummonPosition = new Vector2(randomX, randomY);

        // Assign animator
        myAnimator = GetComponent<Animator>();
    }

    //****************************************************************************************************
    private void Update()
    {
        // If the player is not dead
        if (player != null)
        {
            // If we are not at our intended summon position
            if (Vector2.Distance(transform.position, targetSummonPosition) > 0.5f)
            {
                // Move towards target position
                transform.position = Vector2.MoveTowards(transform.position, targetSummonPosition, enemySpeed * Time.deltaTime);

                // Set bool for animation
                myAnimator.SetBool("isRunning", true);
            }
            else
            {
                // Set bool for animation
                myAnimator.SetBool("isRunning", false);

                if (Time.time >= summonTime)
                {
                    summonTime = Time.time + summonRate;

                    // Trigger animation
                    myAnimator.SetTrigger("Summon");
                }
            }

            // If we are close to the player
            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);

                if (Time.time >= attackTime)
                {
                    StartCoroutine(Attack());

                    attackTime = Time.time + enemyAttackRate;
                }
            }
        }
    }

    //****************************************************************************************************
    private void Summon()
    {
        // If player is not dead
        if(player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
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
