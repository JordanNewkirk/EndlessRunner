using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    public int healthToAdd = 1;

    public AudioSource powerUpAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //this.gameObject.SetActive(false);
            PlayerHealth addHealth = collision.gameObject.GetComponent<PlayerHealth>();
            powerUpAudio.Play();
            addHealth.AddHealth(healthToAdd);

            Destroy(this.gameObject);
        }
    }
}
