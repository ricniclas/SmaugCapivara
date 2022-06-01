using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Progression", menuName = "ScriptableObjects/Stage Progression", order = 1)]
public class StageProgressionScriptableObject : ScriptableObject
{
    public string saveName;
    public StageProgressionInfo[] stageList = new StageProgressionInfo[1];
}
