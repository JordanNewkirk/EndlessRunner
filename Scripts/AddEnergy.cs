using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnergy : MonoBehaviour
{
    public float energyDuration = 5f;
    public float energyMultiplier = 2f;

    public AudioSource powerUpAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !EnergyBoostManager.Instance.IsBoostActive())
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            powerUpAudio.Play();

            EnergyBoostManager.Instance.StartEnergyBoost(player, energyMultiplier, energyDuration);

            Destroy(this.gameObject);
        }
    }
}
