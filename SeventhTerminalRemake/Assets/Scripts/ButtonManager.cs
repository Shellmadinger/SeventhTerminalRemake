using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;

    public void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            mainMenu.SetActive(true);
            credits.SetActive(false);
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
