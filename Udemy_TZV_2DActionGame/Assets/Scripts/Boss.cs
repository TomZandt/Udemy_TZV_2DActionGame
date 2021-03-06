using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("The health of the boss")]
    public int bossHealth = 200;

    [Header("The enemies to spawn when hit")]
    public Enemy[] enemies;

    [Header("The offset to spawn the enemy")]
    public Vector3 enemyOffset;

    [Header("The damage dealth by the boss")]
    public int damage = 1;

    [Header("The death effect")]
    public GameObject deathEffect;
    public GameObject blood;

    private int halfHealth = 100;
    private Animator anim;
    private Slider healthBar;

    private SceneTransitions sceneTransition;

    //****************************************************************************************************
    private void Start()
    {
        halfHealth = bossHealth / 2;
        anim = GetComponent<Animator>();

        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = bossHealth;
        healthBar.value = bossHealth;

        sceneTransition = FindObjectOfType<SceneTransitions>();
    }

    //****************************************************************************************************
    public void TakeDamage(int damageAmount)
    {
        bossHealth -= damageAmount;

        healthBar.value = bossHealth;

        // Iff the boss is dead
        if (bossHealth <= 0)
        {
            // Play particle effect
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(blood, transform.position, Quaternion.identity);

            healthBar.gameObject.SetActive(false);

            Destroy(gameObject);

            sceneTransition.LoadScene("You_Won");
        }

        // If the boss is at half health
        if (bossHealth <= halfHealth)
        {
            anim.SetTrigger("Stage 2");
        }

        // Assign a random number based on the length of the array
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];

        // Spawn an enemy at the boss position
        Instantiate(randomEnemy, transform.position + enemyOffset, transform.rotation);

    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we collide with the player
        if (collision.tag == "Player")
        {
            // Deal damage to the player
            collision.GetComponent<MainCharacter>().TakeDamage(damage);
        }
    }
}

