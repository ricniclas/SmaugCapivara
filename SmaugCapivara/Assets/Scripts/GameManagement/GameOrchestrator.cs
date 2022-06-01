using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameOrchestrator : MonoBehaviour
{
    public int currentStageIndex;
    public SaveCarrier saveCarrier;
    public StageProgressionInfo currentStageInfo;
    public CanvasManager canvasManager;
    public AudioManager audioManager;
    private StageController currentStageController;
    public static GameOrchestrator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Strings.sceneLoadBanks)
        {
            saveCarrier.initialLoadBehaviour();
            //currentSaveIndex = saveCarrier.returnLastSaved();
        }
        if (scene.name == Strings.sceneTests)
        {
            currentStageController = GameObject.FindGameObjectWithTag(Strings.tagStageController).GetComponent<StageController>();
            currentStageController.initializeCheckpoints(currentStageInfo.checkpointReached);
            currentStageController.spawnPlayer(currentStageInfo.checkpointReached);
            audioManager.setCurrentMusic(Strings.FMODEventPathMUSBoss);
            audioManager.playMusic();
        }
        if(scene.name == Strings.sceneMenu)
        {
            audioManager.setCurrentMusic(Strings.FMODEventPathMUSStage1);
            audioManager.playMusic();
        }
    }

    public void changeStageProgressionInfo(int checkpointIndex, bool finishedStage)
    {
        if(currentStageInfo.finished == false)
        {
            currentStageInfo.finished = finishedStage;
            currentStageInfo.checkpointReached = checkpointIndex;
            saveCarrier.changeStageProgressionData(currentStageInfo, currentStageIndex);
        }
    }


    public void loadScene(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.MENU:
                SceneManager.LoadScene(Strings.sceneMenu, LoadSceneMode.Single);
                break;
            case SceneName.GAMEPLAY:
                SceneManager.LoadScene(Strings.sceneTests, LoadSceneMode.Single);
                break;
            case SceneName.LOAD_BANKS:
                SceneManager.LoadScene(Strings.sceneLoadBanks, LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }

    public void playerDeath()
    {
        currentStageController.spawnPlayer(currentStageInfo.checkpointReached);
    }

    public StageController getStageController()
    {
        return currentStageController;
    }
}

public enum SceneName
{
    MENU,
    GAMEPLAY,
    LOAD_BANKS,
}
