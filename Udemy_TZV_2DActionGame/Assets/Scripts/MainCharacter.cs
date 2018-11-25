using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public float movementSpeed;

    private Rigidbody2D myRB;
    private Vector2 moveAmount;

    //****************************************************************************************************
    private void Start()
    {
        // Assign rigid body to local variable
        myRB = GetComponent<Rigidbody2D>();
    }

    //****************************************************************************************************
    private void Update()
    {
        // Assign players x and y input to a vector
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Calculate the amount of movement the player has asked for
        moveAmount = playerInput.normalized * movementSpeed;
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        // Move rigidbody
        myRB.MovePosition(myRB.position + moveAmount * Time.fixedDeltaTime);
    }
}
