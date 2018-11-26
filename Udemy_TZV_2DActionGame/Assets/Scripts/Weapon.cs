using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform barrel;
    public float timeBetweenShots = 0.3f;

    private float shotTime;

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

                shotTime = Time.time + timeBetweenShots;
            }
        }

    }
}
