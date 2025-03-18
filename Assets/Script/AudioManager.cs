using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgm;
    public AudioClip bgmClip;
    private AudioSource sfx;
    public AudioClip[] sfxs;
    public AudioClip[] footstepClips;

    private float masterVol;
    private float bgmVol;
    private float sfxVol;

    public float MasterVol => masterVol;
    public float BgmVol => bgmVol;
    public float SFXVol => sfxVol;
    //public List<AudioSource> SFXList => sfxList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        masterVol = 1.0f;
        bgmVol = 0.3f; 
        sfxVol = 1.0f;

        bgm = GetComponent<AudioSource>();
        sfx = GetComponent<AudioSource>();
        bgm.clip = bgmClip;
        bgm.volume = bgmVol * masterVol;

    }

    private void Start()
    {
        bgm.loop = true;
        bgm.Play();
    }

    public void ChangeMasterVolume(float figure)
    {
        masterVol = figure;
        bgm.volume = bgmVol * masterVol;
        sfx.volume = sfxVol * masterVol;
    }

    public void ChangeBGMVolume(float figure)
    {
        bgmVol = figure;
        bgm.volume = bgmVol * masterVol;
    }

    public void ChangeSFXVolume(float figure)
    {
        sfxVol = figure;
        sfx.volume = sfxVol * masterVol;
    }

    public void PlaySFX(int id)
    {
        sfx.clip = sfxs[id];
        sfx.Play();
    }

    public void PlayFootSteps()
    {
        sfx.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
    }

}
