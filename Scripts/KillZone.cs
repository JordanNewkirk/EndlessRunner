using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth killPlayer = collision.gameObject.GetComponent<PlayerHealth>();
            killPlayer.TakeDamage(3);

            if(killPlayer.invincibilityCounter > 0)
            {
                SceneManager.LoadScene("DeathMenu");
            }
        }
    }
}
