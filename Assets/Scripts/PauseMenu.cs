using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject confirmMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void ClickAndLoadMainMenu(){
        Time.timeScale = 1;
        Invoke("LoadMainMenu", 0.3f);
    }
    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToPauseMenu(){

        confirmMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void GoToConfirmMenu(){
        pauseMenu.SetActive(false);
        confirmMenu.SetActive(true);
    }
}
