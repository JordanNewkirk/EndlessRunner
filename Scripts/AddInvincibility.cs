using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInvincibility : MonoBehaviour
{
    public int invincibilityToAdd = 5;

    public AudioSource powerUpAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //this.gameObject.SetActive(false);
            PlayerHealth invincibility = collision.gameObject.GetComponent<PlayerHealth>();
            powerUpAudio.Play();
            invincibility.Invincibility(invincibilityToAdd);

            Destroy(this.gameObject);
        }
    }
}
