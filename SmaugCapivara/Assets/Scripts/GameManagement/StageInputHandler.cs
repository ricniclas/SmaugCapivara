using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageInputHandler : MonoBehaviour
{
    private bool isPaused = false;

    private PlayerController playerController;
    private StageController stageController;

    #region Input Events
    public void startButton(InputAction.CallbackContext context)
    {
        Debug.Log("StartButton");
        if (context.started)
        {
            Debug.Log("Paused");
            stageController.setPauseScreen(!isPaused);
        }
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            if (context.started)
                playerController.onJump();

            if (context.canceled)
                playerController.onReleaseJump();
        }
    }

    public void movement(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            if (context.canceled)
            {
                playerController.setDirectionalInputs(new Vector2(0, 0));
            }
            else
            {
                playerController.setDirectionalInputs(context.ReadValue<Vector2>());
            }
        }
    }

    public void attack(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            if (context.started)
            {
                playerController.onAttackPressed();
            }
            if (context.performed)
            {
                playerController.onAttackHeld();
            }
            if (context.canceled)
            {
                playerController.onAttackRelease();
            }
        }
    }
    #endregion

    public void setVariables(PlayerController playerController, StageController stageController)
    {
        this.playerController = playerController;
        this.stageController = stageController;
    }

    public void setIsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }
}
