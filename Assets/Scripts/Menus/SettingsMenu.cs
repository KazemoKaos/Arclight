using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System;

public class SettingsMenu : MonoBehaviour
{
    public GameObject soundMenuObj, videoMenuObj, controlsMenuObj;
    public AudioMixer mainMixer;
    public Slider volSlider, musicSlider, effectsSlider, enemiesSlider;
    public Toggle fullScreenToggle, vsyncToggle;
    float currVol, currMusicVol, currEffectsVol, currEnemiesVol, currSDist, currSens;
    int currQuality, currAA, currFPSLimit, currRes;
    [SerializeField] Slider sensitivitySlider;

    public static Action<float> UpdateSensitivity;

    void Start()
    {
        //======================================================================
        //Volume
        //Master
        currVol = PlayerPrefs.GetFloat("currentVol", -30);
        volSlider.value = currVol;
        //Music
        currMusicVol = PlayerPrefs.GetFloat("currentMusicVol", -25);
        musicSlider.value = currMusicVol;
        //Effects
        currEffectsVol = PlayerPrefs.GetFloat("currentEffectsVol", 1);
        effectsSlider.value = currEffectsVol;
        //Enemies
        currEnemiesVol = PlayerPrefs.GetFloat("currentEnemiesVol", -25);
        enemiesSlider.value = currEnemiesVol;

        //======================================================================
        //Quality
        currQuality = PlayerPrefs.GetInt("currentQuality", 2);
        //Shadow Distance
        currSDist = PlayerPrefs.GetFloat("currentShadowDistance", 1);
        //AntiAliasing
        currAA = PlayerPrefs.GetInt("currentAA", 2);
        //FPS Limit
        currFPSLimit = PlayerPrefs.GetInt("currentFPSLimit", 2);

        //======================================================================
        //Sensitivity
        currSens = PlayerPrefs.GetFloat("currentSensitivity", 5);
        sensitivitySlider.value = currSens;
        UpdateSensitivity?.Invoke(currSens);
    }

    //Button Functions

    public void OpenSound()
    {
        soundMenuObj.SetActive(true);
        videoMenuObj.SetActive(false);
        controlsMenuObj.SetActive(false);
    }

    public void OpenVideo()
    {
        videoMenuObj.SetActive(true);
        soundMenuObj.SetActive(false);
        controlsMenuObj.SetActive(false);
    }

    public void OpenControls()
    {
        controlsMenuObj.SetActive(true);
        soundMenuObj.SetActive(false);
        videoMenuObj.SetActive(false);
    }


    //Sound Settings
    public void SetVolume(float volume)
    {
        currVol = volume;
        mainMixer.SetFloat("masterVol", currVol);
        PlayerPrefs.SetFloat("currentVol", currVol);
    }

    public void SetMusicVol(float volume)
    {
        currMusicVol = volume;
        mainMixer.SetFloat("musicVol", currMusicVol);
        PlayerPrefs.SetFloat("currentMusicVol", currMusicVol);
    }

    public void SetEffectsVol(float volume)
    {
        currEffectsVol = volume;
        mainMixer.SetFloat("effectsVol", currEffectsVol);
        PlayerPrefs.SetFloat("currentEffectsVol", currEffectsVol);
    }

    public void SetEnemiesVol(float volume)
    {
        currEnemiesVol = volume;
        mainMixer.SetFloat("enemiesVol", currEnemiesVol);
        PlayerPrefs.SetFloat("currentEnemiesVol", currEnemiesVol);
    }

    //Video Settings
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //Quality Settings
    public void SetQuality(int qualityIndex)
    {
        currQuality = qualityIndex;
        QualitySettings.SetQualityLevel(currQuality);
        PlayerPrefs.SetInt("currentQuality", currQuality);
    }

    public void SetVsync(bool isVsync)
    {
        if(isVsync) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;

        if (QualitySettings.vSyncCount == 1)
        {
            vsyncToggle.isOn = true;
        }
        else
        {
            vsyncToggle.isOn = false;
        }
    }

    public void SetShadowDist(float distance)
    {
        currSDist = distance;
        QualitySettings.shadowDistance = currSDist;
        PlayerPrefs.SetFloat("currentShadowDistance", currSDist);
    }

    public void SetAntiAliasing(int qualityIndex)
    {
        switch (qualityIndex)
        {
            case 0: currAA = 0;
                break;
            case 1: currAA = 2;
                break;
            case 2: currAA = 4;
                break;
            case 3: currAA = 8;
                break;
        }
        QualitySettings.antiAliasing = currAA;
        PlayerPrefs.SetInt("currentAA", currAA);
    }

    public void SetFPSLimit(int limitIndex)
    {
        switch (limitIndex)
        {
            case 0:
                currFPSLimit = 0;
                break;
            case 1:
                currFPSLimit = 30;
                break;
            case 2:
                currFPSLimit = 60;
                break;
            case 3:
                currFPSLimit = 120;
                break;
            case 4:
                currFPSLimit = 240;
                break;
        }
        Application.targetFrameRate = currFPSLimit;
        PlayerPrefs.SetInt("currentFPSLimit", currFPSLimit);
    }

    public void SetResolution(int resIndex)
    {
        switch (resIndex)
        {
            case 0:
                Screen.SetResolution(640, 480, false);
                break;
            case 1:
                Screen.SetResolution(720, 480, false);
                break;
            case 2:
                Screen.SetResolution(800, 600, false);
                break;
            case 3:
                Screen.SetResolution(1024, 768, false);
                break;
            case 4:
                Screen.SetResolution(1152, 864, false);
                break;
            case 5:
                Screen.SetResolution(1280, 720, false);
                break;
            case 6:
                Screen.SetResolution(1280, 768, false);
                break;
            case 7:
                Screen.SetResolution(1280, 800, false);
                break;
            case 8:
                Screen.SetResolution(1280, 960, false);
                break;
            case 9:
                Screen.SetResolution(1280, 1024, false);
                break;
            case 10:
                Screen.SetResolution(1360, 768, false);
                break;
            case 11:
                Screen.SetResolution(1366, 768, false);
                break;
            case 12:
                Screen.SetResolution(1600, 900, false);
                break;
            case 13:
                Screen.SetResolution(1600, 1024, false);
                break;
            case 14:
                Screen.SetResolution(1680, 1050, false);
                break;
            case 15:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    public void SetSensitivity(float sens)
    {
        currSens = sens;
        PlayerPrefs.SetFloat("currentSensitivity", currSens);
        sensitivitySlider.value = currSens;
        UpdateSensitivity?.Invoke(currSens);
    }
}
