using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GoToInstructions()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void quit(){
        Application.Quit();
    }
}
