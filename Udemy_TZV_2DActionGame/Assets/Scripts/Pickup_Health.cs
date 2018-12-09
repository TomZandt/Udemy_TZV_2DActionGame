using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Health : MonoBehaviour
{
    [Header("The amount to heal the player")]
    public int amountToHeal = 1;

    private MainCharacter playerScript;

    [Header("Pickup particle effect")]
    public GameObject particleEffect;


    //****************************************************************************************************
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>();
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we have collided with the player
        if (collision.tag == "Player")
        {
            Instantiate(particleEffect, transform.position, Quaternion.identity);

            // Heal the player
            playerScript.Heal(amountToHeal);

            // Destroy
            Destroy(gameObject);
        }
    }
}
