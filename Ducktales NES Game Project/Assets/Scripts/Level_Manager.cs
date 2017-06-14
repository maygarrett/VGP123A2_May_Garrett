using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void quitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void levelSelect()
    {
        SceneManager.LoadScene("Level_Select");
        SoundManager.instance.playMusic(SoundManager.instance.levelMusic);
    }

    public void moonLevel()
    {
        GameManager.instance.lives = 3;
        SceneManager.LoadScene("Moon_Level");
        SoundManager.instance.playMusic(SoundManager.instance.moonMusic);
    }

    public void credits()
    {
        SceneManager.LoadScene("Credits");
        SoundManager.instance.playMusic(SoundManager.instance.creditMusic);
    }

    public void titleScreen()
    {
        SceneManager.LoadScene("Title_Screen");
        SoundManager.instance.playMusic(SoundManager.instance.titleMusic);
    }

    public void gameOver()
    {
       
        SceneManager.LoadScene("Game_Over");
        SoundManager.instance.playMusic(SoundManager.instance.overMusic);
    }
}
