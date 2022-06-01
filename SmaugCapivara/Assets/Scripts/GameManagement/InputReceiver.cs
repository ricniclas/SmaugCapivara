using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReceiver : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    public void jump(InputAction.CallbackContext context)
    {
        if (context.started)
            playerController.onJump();

        if (context.canceled)
            playerController.onReleaseJump();
    }

    public void movement(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            playerController.setDirectionalInputs(new Vector2(0,0));
        }
        else
        {
            playerController.setDirectionalInputs(context.ReadValue<Vector2>());
        }
    }

    public void attack(InputAction.CallbackContext context)
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