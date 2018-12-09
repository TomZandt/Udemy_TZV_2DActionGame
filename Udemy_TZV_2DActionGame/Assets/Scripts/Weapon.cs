using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("The projectile prefab to shoot")]
    public GameObject projectile;

    [Header("The transform to shoot from")]
    public Transform barrel;

    [Header("The time between shots in seconds")]
    public float timeBetweenShots = 0.3f;

    [Header("The shoot effect")]
    public GameObject particleEffect;

    private float shotTime;

    //****************************************************************************************************
    private void Update()
    {
        // Point the weapon to the players mouse
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        transform.rotation = rotation;

        // If the player requests to shoot
        if(Input.GetMouseButton(0))
        {
            // If the shot time is greater than the shotTime
            if(Time.time >= shotTime)
            {
                Instantiate(projectile, barrel.position, transform.rotation);
                Instantiate(particleEffect, barrel.position, Quaternion.identity);

                shotTime = Time.time + timeBetweenShots;
            }
        }

    }
}
