using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBoostManager : MonoBehaviour
{
    private static EnergyBoostManager instance;
    public static EnergyBoostManager Instance { get { return instance; } }

    private bool isBoostActive = false;
    private PlayerController player;

    private float originalJumpForce;
    private float originalSlideSpeed;
    private float originalMoveSpeed;
    private float originalSlideDuration;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartEnergyBoost(PlayerController playerController, float energyMultiplier, float duration)
    {
        if (!isBoostActive)
        {
            isBoostActive = true;
            player = playerController;

            originalJumpForce = player.jumpForce;
            originalSlideSpeed = player.slideSpeed;
            originalMoveSpeed = player.moveSpeed;
            originalSlideDuration = player.slideDuration;

            ApplyEnergyBoost(energyMultiplier);

            StartCoroutine(EndEnergyBoost(duration));
        }
    }

    private void ApplyEnergyBoost(float energyMultiplier)
    {
        player.jumpForce *= energyMultiplier;
        player.slideSpeed *= energyMultiplier;
        player.moveSpeed *= energyMultiplier;
        player.slideDuration *= energyMultiplier;
    }

    private IEnumerator EndEnergyBoost(float duration)
    {
        yield return new WaitForSeconds(duration);

        ResetEnergyBoost();
    }

    private void ResetEnergyBoost()
    {
        if (isBoostActive)
        {
            player.jumpForce = originalJumpForce;
            player.slideSpeed = originalSlideSpeed;
            player.moveSpeed = originalMoveSpeed;
            player.slideDuration = originalSlideDuration;

            isBoostActive = false;
            player = null;
        }
    }

    public bool IsBoostActive()
    {
        return isBoostActive;
    }
}
