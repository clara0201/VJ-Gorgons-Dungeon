using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogicManager : MonoBehaviour
{
    

    public GameObject GameOverScreen;
    public GameObject WinningScreen;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        GameOverScreen.SetActive(false);

        WinningScreen.SetActive(false);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {         

        //Temp
        if (Input.GetKeyDown("1"))
        {
            WinGame();
        }
        else if (Input.GetKeyDown("2"))
        {
            LooseGame();
        }
        
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        WinningScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LooseGame()
    {

        Time.timeScale = 0;
        GameOverScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("Level2Scene");
    }

    public void LoadScene3()
    {
        PlayerStats.Instance.swordActive = true;
        SceneManager.LoadScene("Level3Scene");
    }

    public void LoadSceneBoss()
    {
        SceneManager.LoadScene("LevelBossScene");
    }
}
