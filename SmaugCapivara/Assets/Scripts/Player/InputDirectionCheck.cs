using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDirectionCheck
{
    public static DirectionChecked checkDirection(float horizontalInput, float verticalInput)
    {
        if(horizontalInput == 0 && verticalInput == 0)
        {
            return DirectionChecked.NEUTRAL;
        }
        else
        {
            if (verticalInput > 0)
            {
                if(horizontalInput == 0)
                {
                    return DirectionChecked.UP;
                }
                else
                {
                    if (horizontalInput > 0)
                        return DirectionChecked.UP_RIGHT;
                    else
                        return DirectionChecked.UP_LEFT;
                }
            }
            if (verticalInput < 0)
            {
                if (horizontalInput == 0)
                {
                    return DirectionChecked.DOWN;
                }
                else
                {
                    if (horizontalInput > 0)
                        return DirectionChecked.DOWN_RIGHT;
                    else
                        return DirectionChecked.DOWN_LEFT;
                }
            }
            else
            {
                if (horizontalInput > 0)
                    return DirectionChecked.RIGHT;
                else
                    return DirectionChecked.LEFT;
            }
        }
    }
}

public enum DirectionChecked
{
    UP_LEFT,
    UP,
    UP_RIGHT,
    LEFT,
    NEUTRAL,
    RIGHT,
    DOWN_LEFT,
    DOWN,
    DOWN_RIGHT
}
