using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    private float volumeMUS;
    private float volumeSFX;
    private float volumeMASTER;

    FMOD.Studio.Bus busMUS;
    FMOD.Studio.Bus busSFX;
    FMOD.Studio.Bus busMaster;

    public FMOD.Studio.EventInstance currentMusic;

    private void Start()
    {
        busMaster = FMODUnity.RuntimeManager.GetBus(Strings.busPathMaster);
        busMUS = FMODUnity.RuntimeManager.GetBus(Strings.busPathMUS);
        busSFX = FMODUnity.RuntimeManager.GetBus(Strings.busPathSFX);
    }

    public void SetBankVolume(float volume, BankName bankName)
    {

        switch (bankName)
        {
            case BankName.MUS:
                busMUS.setVolume(volume);
                volumeMUS = volume;
                break;
            case BankName.SFX:
                busSFX.setVolume(volume);
                volumeSFX = volume;
                break;
            case BankName.MASTER:
                busMaster.setVolume(volume);
                volumeMASTER = volume;
                break;
            default:
                break;
        }
    }

    public float GetBankVolume(BankName bankName)
    {
        switch (bankName)
        {
            case BankName.MUS:
                return volumeMUS;
            case BankName.SFX:
                return volumeSFX;
            case BankName.MASTER:
                return volumeMASTER;
            default:
                return 0;
        }
    }


    #region Play and Stop
    public void setCurrentMusic(string eventPath)
    {
        if (isMusicPlaying(currentMusic))
        {
            currentMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        currentMusic = FMODUnity.RuntimeManager.CreateInstance(eventPath);
    }

    public void playMusic()
    {
        if (isMusicPlaying(currentMusic))
        {
            currentMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        currentMusic.start();
    }

    public void stopMusic(FMOD.Studio.STOP_MODE stopMode)
    {
        currentMusic.stop(stopMode);
    }

    private bool isMusicPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
    #endregion

}

public enum BankName
{
    MUS,
    SFX,
    MASTER
}