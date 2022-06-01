using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCarrier : MonoBehaviour
{
    public StageProgressionScriptableObject originalStagesList;
    public StageProgressionInfo[] currentStageProgression;

    public void initialLoadBehaviour()
    {
        ProgressionData progressionData = SaveSystem.LoadProgress();
        if (progressionData == null)
        {
            Debug.Log("First Save not found. Creating a new one");
            SaveSystem.SaveProgress(originalStagesList.stageList, "Save1");
            currentStageProgression = originalStagesList.stageList;
        }
        else 
        {
            currentStageProgression = progressionData.stageProgressioninfo;
        }
    }

    public void changeStageProgressionData(StageProgressionInfo stageProgressionInfo, int index)
    {
        currentStageProgression[index] = stageProgressionInfo;
        SaveSystem.SaveProgress(currentStageProgression, "Save1");
    }
}
