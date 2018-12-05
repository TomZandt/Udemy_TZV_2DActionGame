using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_Chase : StateMachineBehaviour
{
    [Header("The speed that the boss will chase at")]
    public float bossChaseSpeed = 5f;

    private GameObject player;

    //****************************************************************************************************
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // Find the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //****************************************************************************************************
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // If there is a player
        if(player != null)
        {
            // Move towards the player
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.transform.position, bossChaseSpeed * Time.deltaTime);
        }
    }

    //****************************************************************************************************
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
}
