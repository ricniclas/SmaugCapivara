using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    #region Variables
    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button deleteSaveButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button closeOptionsButton;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider brightnessSlider;

    [Header("Windows")]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject warningWindow;
    private WarningValues warningValues;


    public MenuInputHandler menuInputHandler;

    private float masterSliderValue;
    private float musSliderValue;
    private float sfxSliderValue;
    private float brightnessSliderValue;

    private CurrentWindowOpen currentWindowOpen;
    #endregion

    private void Start()
    {
        warningValues = warningWindow.GetComponent<WarningValues>();
        setSelectedObject(startGameButton.gameObject);
        optionsMenu.SetActive(false);
        warningWindow.SetActive(false);
        getStoredValues();
        setSliderPositions();
        currentWindowOpen = CurrentWindowOpen.DEFAULT;
    }

    private void setSelectedObject(GameObject gameObj)
    {
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(gameObj);
    }

    public void toggleOptionsMenu(bool activate)
    {
        optionsMenu.SetActive(activate);
        startGameButton.gameObject.SetActive(!activate);
        deleteSaveButton.gameObject.SetActive(!activate);
        optionsButton.gameObject.SetActive(!activate);
        if (activate)
        {
            setSelectedObject(masterSlider.gameObject);
            currentWindowOpen = CurrentWindowOpen.OPTIONS;
        }
        else
        {
            setSelectedObject(startGameButton.gameObject);
            currentWindowOpen = CurrentWindowOpen.DEFAULT;
        }
    }

    public void toggleWarningWindowActive(bool value)
    {
        if (value)
        {
            warningValues.setValues("Vai Funcionar?", "Sim", "Não", () => testResult("Eureka!"), () => toggleWarningWindowActive(false), () => toggleWarningWindowActive(false));
            warningWindow.SetActive(value);
        }
        else
        {
            warningWindow.SetActive(false);
        }
    }

    public void testResult(string value)
    {
        Debug.Log(value);
        toggleWarningWindowActive(false);
    }

    public void loadScene(int stageIndex)
    {
        GameOrchestrator.Instance.currentStageInfo = GameOrchestrator.Instance.saveCarrier.currentStageProgression[stageIndex];
        GameOrchestrator.Instance.currentStageIndex = stageIndex;
        GameOrchestrator.Instance.loadScene(SceneName.GAMEPLAY);
    }

    public void resetSave()
    {
        SaveSystem.DeleteProgress();
        SaveSystem.SaveProgress(GameOrchestrator.Instance.saveCarrier.originalStagesList.stageList, "Save1");
    }

    #region Sound and Sliders

    private void setSliderPositions()
    {
        masterSlider.value = masterSliderValue;
        musicSlider.value = musSliderValue;
        sfxSlider.value = sfxSliderValue;
        brightnessSlider.value = brightnessSliderValue;
    }

    private void setStoredVolume(BankName bankName, float volume)
    {
        switch (bankName)
        {
            case BankName.MUS:
                musSliderValue = volume;
                PlayerPrefs.SetFloat(Strings.playerPrefsVolumeMUS, volume);
                break;
            case BankName.SFX:
                sfxSliderValue = volume;
                PlayerPrefs.SetFloat(Strings.playerPrefsVolumeSFX, volume);
                break;
            case BankName.MASTER:
                masterSliderValue = volume;
                PlayerPrefs.SetFloat(Strings.playerPrefsVolumeMaster, volume);
                break;
            default:
                break;
        }
        GameOrchestrator.Instance.audioManager.SetBankVolume(volume, bankName);
    }

    private void getStoredValues()
    {
        masterSliderValue = PlayerPrefs.GetFloat(Strings.playerPrefsVolumeMaster, 0);
        musSliderValue = PlayerPrefs.GetFloat(Strings.playerPrefsVolumeMUS, 0);
        sfxSliderValue = PlayerPrefs.GetFloat(Strings.playerPrefsVolumeSFX, 0);
        brightnessSliderValue = PlayerPrefs.GetFloat(Strings.playerPrefsPostProcessBrightness, 0);
    }

    public void setMasterVolume(float volume)
    {
        setStoredVolume(BankName.MASTER, volume);
    }
    public void setMUSVolume(float volume)
    {
        setStoredVolume(BankName.MUS, volume);
    }

    public void setSFXVolume(float volume)
    {
        setStoredVolume(BankName.SFX, volume);
    }
    #endregion

    public void pressedStart()
    {

    }

    public void pressedAction()
    {

    }

    public void pressedBack()
    {
        switch (currentWindowOpen)
        {
            case CurrentWindowOpen.OPTIONS:
                toggleOptionsMenu(false);
                break;
            case CurrentWindowOpen.CREDITS:
                break;
            case CurrentWindowOpen.WARNING:
                break;
            case CurrentWindowOpen.DEFAULT:
                break;
            default:
                break;
        }
    }

    public void closeGame()
    {
        Application.Quit();
    }
}

public enum CurrentWindowOpen{
    OPTIONS,
    CREDITS,
    WARNING,
    DEFAULT
}