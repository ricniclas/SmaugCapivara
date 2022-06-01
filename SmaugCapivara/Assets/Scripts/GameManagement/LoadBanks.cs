using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBanks : MonoBehaviour
{
    private void Awake()
    {
        FMODUnity.RuntimeManager.LoadBank("Master", true);
        FMODUnity.RuntimeManager.LoadBank("Master.strings", true);
        FMODUnity.RuntimeManager.LoadBank("MUS", true);
        FMODUnity.RuntimeManager.LoadBank("SFX", true);
    }

    private void Update()
    {
        if (FMODUnity.RuntimeManager.HasBankLoaded("Master") &&
            FMODUnity.RuntimeManager.HasBankLoaded("Master.strings") &&
            FMODUnity.RuntimeManager.HasBankLoaded("MUS") &&
            FMODUnity.RuntimeManager.HasBankLoaded("SFX"))
        {
            Debug.Log("Master Bank Loaded");
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
