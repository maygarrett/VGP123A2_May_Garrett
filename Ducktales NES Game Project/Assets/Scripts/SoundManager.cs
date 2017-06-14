using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    static SoundManager _instance = null;

    public AudioSource musicSource;
    public AudioSource musicSource2;
    public AudioSource fxSource;
    public AudioSource fxSource2;
    public AudioClip titleMusic;
    public AudioClip levelMusic;
    public AudioClip moonMusic;
    public AudioClip overMusic;
    public AudioClip creditMusic;

    public AudioClip playerJump;
    public AudioClip playerAttack;
    public AudioClip playerLand;
    public AudioClip playerDeath;

    public AudioClip octopusAttack;
    public AudioClip projectileDestroyed;
    public AudioClip enemyDeath;
    public AudioClip duckJump;

    public AudioClip coinCollected;
    public AudioClip chestCollected;

    public AudioClip boxDestroyed;
    public AudioClip boxHit;

    public AudioClip climbing;

    // Use this for initialization
    void Start () {

        if (instance)
            DestroyImmediate(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        if (!musicSource)
            Debug.Log("no music source specified");
        if (!fxSource)
            Debug.Log("no fx source specified");

        stopMusic2();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static SoundManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public void playMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void playMusic2()
    {
        musicSource2.enabled = true;
        
    }

    public void stopMusic2()
    {
        musicSource2.enabled = false;
    }

    public void playSound(AudioClip sound)
    {
        fxSource.clip = sound;
        fxSource.Play();
    }

    public void playSound2(AudioClip sound)
    {
        fxSource2.clip = sound;
        fxSource2.Play();
    }
}
