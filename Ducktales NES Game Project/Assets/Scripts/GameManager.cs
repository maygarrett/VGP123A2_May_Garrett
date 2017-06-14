using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    static GameManager _instance = null;

    public GameObject playerPrefab;

    int _score;
    public int _lives;
    

    // Use this for initialization
    void Start()
    {
        if (instance)
            DestroyImmediate(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        lives = 3;
        score = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Moon_Level")
                SceneManager.LoadScene("Title_Screen");
            else if (SceneManager.GetActiveScene().name == "Title_Screen")
                SceneManager.LoadScene("Moon_Level");
        }

       
        if(SceneManager.GetActiveScene().name == "Moon_Level" && lives > 2)
        {
            
            spawnPlayer();
        }
        if (SceneManager.GetActiveScene().name != "Moon_Level")
        {
            lives = 3;
            score = 0;
        }
        


    }


    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public int score
    {
        get { return _score; }
        set { _score = value; }
    }
    public int lives
    {
        get { return _lives; }
        set { _lives = value; }
    }
    

    public void spawnPlayer()
    {

        if (lives > 0)
        {
            lives -= 1;
            Debug.Log("lives are now at " + lives);
            Transform spawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else if (lives <= 0)
        {
            lives = 3;
            gameOver();
        }
    }

    public void destroySpawn(GameObject thing)
    {
        Destroy(thing);
        spawnPlayer();
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