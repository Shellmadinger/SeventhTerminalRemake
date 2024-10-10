using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //This script contains various functions for the buttons
    //Note that the main menu enables and disables panels for the menus to work, which is reflected in some of the functions here
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject pause;
    public GameObject gamePlayUI;
    [SerializeField] HudManager checkPause;

    public void Start()
    {
        //If the active scene isn't the main menu, we don't set the main menu screens
        //This is mainly to prevent an error when going from the main menu to gameplay
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            mainMenu.SetActive(true);
            credits.SetActive(false);
        }

        if(SceneManager.GetActiveScene().name == "BattleArena2")
        {
            checkPause = FindObjectOfType<HudManager>();
        }
        
    }
    public void ResetScene()
    {
        //Reload the scene when the button is pushed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("BattleArena2");
    }

    public void GoToCredits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }

    public void ReturnToMenuFromGameplay()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        gamePlayUI.SetActive(true);
        pause.SetActive(false);
        Time.timeScale = 1;
        checkPause.isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
