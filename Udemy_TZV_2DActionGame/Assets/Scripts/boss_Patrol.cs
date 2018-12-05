using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_Patrol : StateMachineBehaviour
{
    

    [Header("The speed at which to move to each patrol point")]
    public float bossPatrolSpeed = 5f;

    private GameObject[] patrolPoints;

    private int randomPatrolPoint = 0;

    //****************************************************************************************************
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // Assign the patrol points
        patrolPoints = GameObject.FindGameObjectsWithTag("Boss Patrol Point");

        // assign a random number based upon the patrol points array length
        randomPatrolPoint = Random.Range(0, patrolPoints.Length);
    }

    //****************************************************************************************************
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // Make the boss patrol to a random point in the array
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolPoints[randomPatrolPoint].transform.position, bossPatrolSpeed * Time.deltaTime);

        // Check to see if the boss has reached the patrol point
        if (Vector2.Distance(animator.transform.position, patrolPoints[randomPatrolPoint].transform.position) < 0.1f)
        {
            // Update the random point for next target
            randomPatrolPoint = Random.Range(0, patrolPoints.Length);
        }
    }

    //****************************************************************************************************
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
}
