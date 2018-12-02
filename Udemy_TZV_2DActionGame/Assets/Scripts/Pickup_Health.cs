using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Health : MonoBehaviour
{
    [Header("The amount to heal the player")]
    public int amountToHeal = 1;

    private MainCharacter playerScript;

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
            // Heal the player
            playerScript.Heal(amountToHeal);

            // Destroy
            Destroy(gameObject);
        }
    }
}
