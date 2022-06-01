using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputHandler : MonoBehaviour
{
    private MainMenuController mainMenuController;

    public void setVariables(MainMenuController mainMenuController)
    {
        this.mainMenuController = mainMenuController;
    }

    public void startButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mainMenuController.pressedStart();
        }
    }

    public void backButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mainMenuController.pressedBack();
        }
    }

    public void actionButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mainMenuController.pressedAction();
        }
    }
}

