using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("The weapon to equip")]
    public Weapon weaponToEquip;

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we collide with the player
        if (collision.tag == "Player")
        {
            // Call pickup function
            collision.GetComponent<MainCharacter>().ChangeWeapon(weaponToEquip);

            // Destroy pickup
            Destroy(gameObject);
        }
    }
}
