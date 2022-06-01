using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Animator TransitionScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject resumeButton;
    private EventSystem eventSystem;


    private void Start()
    {
        eventSystem = EventSystem.current;
        setPauseScreen(false);
    }

    public void setPauseScreen(bool activate)
    {
        eventSystem.SetSelectedGameObject(resumeButton);
        pauseScreen.SetActive(activate);
        if (activate)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void fadeIn()
    {
        TransitionScreen.SetTrigger(Strings.animParamTransitionScreenFadeIn);
    }

    public void fadeOut()
    {
        TransitionScreen.SetTrigger(Strings.animParamTransitionScreenFadeOut);
    }

    public void setGameplayTimeScale(bool move)
    {
        if (move)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
