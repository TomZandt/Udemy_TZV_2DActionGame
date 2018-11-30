using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [Header("The movement speed of the character")]
    public float movementSpeed = 10f;

    [Header("The health of the character")]
    public float myHealth = 100f;

    private Rigidbody2D myRB;
    private Vector2 moveAmount;
    private Animator myAnimator;

    //****************************************************************************************************
    private void Start()
    {
        // Assign RigidBody
        myRB = GetComponent<Rigidbody2D>();

        // Assign Animator
        myAnimator = GetComponent<Animator>();
    }

    //****************************************************************************************************
    private void Update()
    {
        // Assign players x and y input to a vector
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Calculate the amount of movement the player has asked for
        moveAmount = playerInput.normalized * movementSpeed;

        // If the player is moving
        if (playerInput != Vector2.zero)
        {
            // start run animation
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            // stop run animation
            myAnimator.SetBool("isRunning", false);
        }
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        if (moveAmount != Vector2.zero)
        {
            // Move rigidbody
            myRB.MovePosition(myRB.position + moveAmount * Time.fixedDeltaTime);
        }
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

    //****************************************************************************************************
    public void ChangeWeapon(Weapon weaponToEquip)
    {
        // Destroy the existing weapon with the tag
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));

        // Equip the new weapon
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }
}
