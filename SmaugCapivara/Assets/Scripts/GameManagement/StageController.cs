using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class StageController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject[] checkpointListGameObject;
    public Checkpoint[] checkpointListScript;
    public GameObject playerPrefab;
    public PlayerController playerController;
    public CanvasManager canvasManager;
    public StageInputHandler stageInputHandler;

    private void Awake()
    {
        for(int i = 0; i < checkpointListGameObject.Length; i++)
        {
            checkpointListScript[i] = checkpointListGameObject[i].GetComponent<Checkpoint>();
        }
    }

    private void Start()
    {
        canvasManager.fadeOut();
        stageInputHandler.setVariables(playerController, this);
    }

    public void setPauseScreen(bool isActive)
    {
        stageInputHandler.setIsPaused(isActive);
        canvasManager.setPauseScreen(isActive);
    }

    public void spawnPlayer(int index)
    {
        if(playerController == null)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab, checkpointListScript[index].spawnPoint.transform.position,Quaternion.Euler(0,0,0));

            playerController = spawnedPlayer.GetComponent<PlayerController>();
            playerController.spawn(checkpointListScript[index].spawnPoint.position);
            cinemachineVirtualCamera.Follow = playerController.transform;
            cinemachineVirtualCamera.LookAt = playerController.transform;
        }
        else
        {
            canvasManager.fadeIn();
            StartCoroutine(deathTransition(index));
        }
    }

    public void initializeCheckpoints(int checkpointReached)
    {
        for (int i = 0; i < checkpointListGameObject.Length; i++)
        {
            if(checkpointListScript[i].checkpointIndex < checkpointReached)
            {
                checkpointListScript[i].changeCheckpointColor(true);
            }
            else
            {
                checkpointListScript[i].changeCheckpointColor(false);
            }
        }
    }

    public void backToMenu()
    {
        GameOrchestrator.Instance.loadScene(SceneName.MENU);
    }
    
    public IEnumerator deathTransition(int index)
    {
        yield return new WaitForSeconds(1f);
        canvasManager.fadeOut();
        playerController.spawn(checkpointListScript[index].spawnPoint.position);
        cinemachineVirtualCamera.Follow = playerController.transform;
        cinemachineVirtualCamera.LookAt = playerController.transform;
    }
}
