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
    public AudioClip[] FootGrass, FootBridge, FootBurned, FootStone;
    public AudioSource musicSource, sfxSource, ambSource;
    [SerializeField] float pitchVariance = 0.5f;
    private AudioClip activeSound;
    public AudioSource forestSource;
    public AudioSource riverSource;
    public AudioSource torchSource;
    public AudioSource castleSource;
    public AudioSource caldronSource;

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

  /*  private void Start()
    {
        var data = AudioSaveManager.instance.Data;

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(data.masterVolume) * 20f);
        audioMixer.SetFloat("SoundEffects", Mathf.Log10(data.sfxVolume) * 20f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(data.musicVolume) * 20f);



        musicSource.volume = data.musicVolume;
        sfxSource.volume = data.sfxVolume;



        PlayMusic(musicSource.name);
    }


    */

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);


        if (s == null)
        {
            Debug.Log("Sound not found");
            return;

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

            return;
        }

        else
        {
            float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
            sfxSource.PlayOneShot(s.clip);
            sfxSource.pitch = randomPitch;
            sfxSource.Play();


        }
    }

    public void PlayGrass()
    {


        activeSound = FootGrass[Random.Range(0, FootGrass.Length)];

        float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
        sfxSource.PlayOneShot(activeSound);
        sfxSource.pitch = randomPitch;
        sfxSource.Play();
        Debug.Log(activeSound);
    }

    public void PlayBridge()
    {
        
        activeSound = FootBridge[Random.Range(0, FootBridge.Length)];
        float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
        sfxSource.PlayOneShot(activeSound);
        sfxSource.pitch = randomPitch;
        sfxSource.Play();
        Debug.Log(activeSound);
    }

    public void PlayBurned()
    {
      
        activeSound = FootBurned[Random.Range(0, FootBurned.Length)];

        float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
        sfxSource.PlayOneShot(activeSound);
        sfxSource.pitch = randomPitch;
        sfxSource.Play();
        Debug.Log(activeSound);
    }

    public void PlayStone()
    {
    
        activeSound = FootStone[Random.Range(0, FootStone.Length)];

        float randomPitch = Random.Range(1f - pitchVariance, 1f + pitchVariance);
        sfxSource.PlayOneShot(activeSound);
        sfxSource.pitch = randomPitch;
        sfxSource.Play();
        Debug.Log(activeSound);
    }


    public void PlayForest(string name)
    {
        Sound s = Array.Find(ambSounds, x => x.name == name);
        if (s == null) return;

        if (forestSource.clip == s.clip) return;

        forestSource.clip = s.clip;
        forestSource.loop = true;
        forestSource.Play();
    }

    public void PlayRiver(string name)
    {
        Sound s = Array.Find(ambSounds, x => x.name == name);
        if (s == null) return;

        if (riverSource.clip == s.clip) return;

        riverSource.clip = s.clip;
        riverSource.loop = true;
        riverSource.Play();
    }

    public void PlayTorch(string name)
    {
        Sound s = Array.Find(ambSounds, x => x.name == name);
        if (s == null) return;

        if (torchSource.clip == s.clip) return;

        torchSource.clip = s.clip;
        torchSource.loop = true;
        torchSource.Play();
    }

    public void PlayCastle(string name)
    {
        Sound s = Array.Find(ambSounds, x => x.name == name);
        if (s == null) return;

        if (castleSource.clip == s.clip) return;

        castleSource.clip = s.clip;
        castleSource.loop = true;
        castleSource.Play();
    }

    public void PlayCaldron(string name)
    {
        Sound s = Array.Find(ambSounds, x => x.name == name);
        if (s == null) return;

        if (caldronSource.clip == s.clip) return;

        caldronSource.clip = s.clip;
        caldronSource.loop = true;
        caldronSource.Play();
    }


    public void PlayAmb(string name)
    {
        Sound s = Array.Find(ambSounds, x => x.name == name);


        if (s == null)
        {
            Debug.Log("Sound not found");
            return;

        }
        if (ambSource.clip == s.clip) return; // prevent restart

        ambSource.clip = s.clip;
        ambSource.loop = true;
        ambSource.Play();

    }
        public void StopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }


}