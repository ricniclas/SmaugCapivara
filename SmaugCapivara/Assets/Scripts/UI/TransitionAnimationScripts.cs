using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimationScripts : MonoBehaviour
{
    [SerializeField] private CanvasManager canvasManager;
    public void setTimeScaleTrue()
    {
        canvasManager.setGameplayTimeScale(true);
    }
}
