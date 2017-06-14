using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager_MoonLevel : MonoBehaviour {

    public Slider slider;
    public Text text;
    public Canvas canvas;

    void Start()
    {
        if (!canvas)
        {
            Debug.Log("no canvas assigned");
        }
        canvas.enabled = false;
        Time.timeScale =  1;

        if (!text)
            Debug.Log("No text assigned");

        if (!slider)
            Debug.Log("no slider assigned");
    }

    // Update is called once per frame
    void Update ()
    {
        updateScore();
        updateLives();

        if(Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

    }

    private void updateScore()
    {
        text.text = "Score: " + GameManager.instance.score;
    }

    private void updateLives()
    {
        slider.value = GameManager.instance.lives + 1;
    }

    void Pause()
    {
        canvas.enabled = !canvas.enabled;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
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
