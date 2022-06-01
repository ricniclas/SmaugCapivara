using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressionData
{
    public string saveName;
    public StageProgressionInfo[] stageProgressioninfo;

    public ProgressionData(StageProgressionInfo[] stageProgressioninfo, string saveName)
    {
        this.saveName = saveName;
        this.stageProgressioninfo = stageProgressioninfo;
    }
}
