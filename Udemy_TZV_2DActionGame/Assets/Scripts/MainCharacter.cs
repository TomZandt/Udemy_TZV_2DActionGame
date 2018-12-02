using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    [Header("The movement speed of the character")]
    public float movementSpeed = 10f;

    [Header("The health of the character")]
    public int myHealth = 5;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

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

        UpdateHealthUI(myHealth);

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

    //****************************************************************************************************
    private void UpdateHealthUI(int currentHealth)
    {
        // For each number of hearts we have
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    //****************************************************************************************************
    public void Heal(int healAmount)
    {
        if (myHealth + healAmount > 5)
        {
            myHealth = 5;
            UpdateHealthUI(myHealth);
        }
        else if (myHealth + healAmount <= 0 || healAmount < 0)
        {
            return;
        }
        else
        {
            // Add health
            myHealth += healAmount;

            UpdateHealthUI(myHealth);
        }
    }
}
