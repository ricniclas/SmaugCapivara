using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveProgress(StageProgressionInfo[] stageProgressionInfo, string saveName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + Strings.dataPathPlayerProgressionFolder;
        FileStream stream = new FileStream(path, FileMode.Create);

        ProgressionData data = new ProgressionData(stageProgressionInfo, saveName);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ProgressionData LoadProgress()
    {
        string path = Application.persistentDataPath + Strings.dataPathPlayerProgressionFolder;
        if (File.Exists(path))
        {
            Debug.Log("File exists:\n" + Application.persistentDataPath + Strings.dataPathPlayerProgressionFolder);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ProgressionData saveData = formatter.Deserialize(stream) as ProgressionData;
            stream.Close();
            return saveData;
        }
        else
        {
            Debug.Log("File does not exists");
            return null;
        }
    }

    public static void DeleteProgress()
    {
        string path = Application.persistentDataPath + Strings.dataPathPlayerProgressionFolder;
        if (File.Exists(path))
        {
            Debug.Log("File exists:\n" + Application.persistentDataPath + Strings.dataPathPlayerProgressionFolder);
            File.Delete(path);

            #if UNITY_EDITOR
                        UnityEditor.AssetDatabase.Refresh();
            #endif

            Debug.Log("File deleted");
        }
        else
        {
            Debug.Log("File doesn't exists");
        }
    }
}
