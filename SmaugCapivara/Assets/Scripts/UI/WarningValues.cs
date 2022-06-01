using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningValues : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button leftButton;
    [SerializeField] private TMP_Text leftButtonText;
    [SerializeField] private Button rightButton;
    [SerializeField] private TMP_Text rightButtonText;
    [SerializeField] private Button middleButton;
    [SerializeField] private TMP_Text middleButtonText;
    [SerializeField] private Button closeButton;

    public void setValues(string messageText, string middleButtonText,Action middleButonCallback, Action closeButtonCallback)
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        middleButton.gameObject.SetActive(true);
        this.messageText.SetText(messageText);
        this.middleButtonText.SetText(middleButtonText);

        middleButton.onClick.RemoveAllListeners();
        middleButton.onClick.AddListener(delegate { middleButonCallback(); });

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(delegate { closeButtonCallback(); });
    }

    public void setValues(string messageText, string leftButtonText, string rightButtonText, Action leftButtonCallback, Action rightButtonCallback, Action closeButtonCallback)
    {
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        middleButton.gameObject.SetActive(false);

        Debug.Log(Strings.FMODEventPathMUSBoss);

        leftButton.onClick.RemoveAllListeners();
        leftButton.onClick.AddListener(delegate { leftButtonCallback();});

        rightButton.onClick.RemoveAllListeners();
        rightButton.onClick.AddListener(delegate { rightButtonCallback(); });

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(delegate { closeButtonCallback(); });

        this.messageText.SetText(messageText);
        this.leftButtonText.SetText(leftButtonText);
        this.rightButtonText.SetText(rightButtonText);
    }
}
