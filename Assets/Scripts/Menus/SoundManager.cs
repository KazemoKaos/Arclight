using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource EffectsAudioSource;
    [SerializeField]
    public AudioSource MusicAudioSource;
    [SerializeField]
    public AudioSource EnemiessAudioSource;

    public AudioClip UIClickSound;
    public AudioClip MainMenuMusic;
    public AudioClip SpaceLevelMusic;
    public AudioClip BossMusic1;
    public AudioClip DesertLevelMusic;

    private AudioSource[] audios;

    public void PlaySound()
    {
        EffectsAudioSource.clip = UIClickSound;
        EffectsAudioSource.Play();
    }

    public void PlayMainMenuMusic()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            MusicAudioSource.clip = MainMenuMusic;
            MusicAudioSource.loop = true;
            MusicAudioSource.Play(0);
        }
    }

    public void PlaySpaceLevelMusic()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SpaceStation"))
        {
            MusicAudioSource.clip = SpaceLevelMusic;
            MusicAudioSource.loop = true;
            MusicAudioSource.Play(0);
        }
    }

    public void PlayDesertLevelMusic()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("DesertLevel"))
        {
            MusicAudioSource.clip = DesertLevelMusic;
            MusicAudioSource.loop = true;
            MusicAudioSource.Play(0);
        }
    }

    public void PlayBossMusic()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SpaceStation"))
        {
            MusicAudioSource.clip = BossMusic1;
            MusicAudioSource.loop = true;
            MusicAudioSource.Play(0);
        }
    }

    public void PauseAllAudio()
    {
        audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource a in audios)
        {
            a.Pause();
            a.mute = true;
        }
    }

    public void ResumeAllAudio()
    {
        foreach (AudioSource a in audios)
        {
            a.Play();
            a.mute = false;
        }
    }

    private void OnEnable()
    {
        MainMenu.PlayMainMenuMusic += PlayMainMenuMusic;
        WorldSpawner.PlayLevelMusic += PlaySpaceLevelMusic;
        WorldSpawner.PlayDesertMusic += PlayDesertLevelMusic;
        TeleportPad.PlayBossMusic += PlayBossMusic;
        PauseMenu.PauseAudio += PauseAllAudio;
        PauseMenu.ResumeAudio += ResumeAllAudio;
    }

    private void OnDisable()
    {
        MainMenu.PlayMainMenuMusic -= PlayMainMenuMusic;
        WorldSpawner.PlayLevelMusic -= PlaySpaceLevelMusic;
        WorldSpawner.PlayDesertMusic -= PlayDesertLevelMusic;
        TeleportPad.PlayBossMusic -= PlayBossMusic;
        PauseMenu.PauseAudio -= PauseAllAudio;
        PauseMenu.ResumeAudio -= ResumeAllAudio;
    }
}
