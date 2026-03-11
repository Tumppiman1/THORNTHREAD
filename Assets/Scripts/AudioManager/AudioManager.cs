using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine.Audio;
using System;


public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds, ambSounds;
    public AudioSource musicSource, sfxSource, ambSource;
    [SerializeField] float pitchVariance = 0.5f;

    [SerializeField] private AudioMixer audioMixer;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var data = AudioSaveManager.instance.Data;

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(data.masterVolume) * 20f);
        audioMixer.SetFloat("SoundEffects", Mathf.Log10(data.sfxVolume) * 20f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(data.musicVolume) * 20f);



        musicSource.volume = data.musicVolume;
        sfxSource.volume = data.sfxVolume;



        PlayMusic(musicSource.name);
    }




    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);


        if (s == null)
        {
            Debug.Log("Sound not found");


        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();

        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);


        if (s == null)
        {
            Debug.Log("Sound not found");


        }

        else
        {
            float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
            sfxSource.clip = s.clip;
            sfxSource.pitch = randomPitch;
            sfxSource.Play();


        }
    }

    public void PlayPianoNote(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Piano sound not found: " + name);
            return;
        }

        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(s.clip);
    }


    public void StopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }


}