using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //getting player health from the script
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            //reduce player's health
            playerHealth.TakeDamage(damageAmount);
        }

        if(collision.gameObject.CompareTag("SlideCollider"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();

            playerHealth.TakeDamage(damageAmount);
        }
    }
}
