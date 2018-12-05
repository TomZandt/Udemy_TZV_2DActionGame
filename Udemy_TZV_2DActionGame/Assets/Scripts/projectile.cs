using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [Header("The damage dealt by the projectile")]
    public int damageDealt = 26;

    [Header("The speed of the projectile")]
    public float speed = 15f;

    [Header("The lifetime of the projectile in seconds")]
    public float lifeTime = 3f;

    [Header("The particle effect when colliding")]
    public GameObject particleCollideEffect;

    //****************************************************************************************************
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    //****************************************************************************************************
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    //****************************************************************************************************
    private void DestroyProjectile()
    {
        Instantiate(particleCollideEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damageDealt);

            DestroyProjectile();
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().TakeDamage(damageDealt);

            DestroyProjectile();
        }
    }
}
