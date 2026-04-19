using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip gameMusic;
    public AudioClip pauseMusic;
    public GameObject pauseMenu;
    public GameObject confirmMenu;
    public GameObject gameUI;

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
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        musicSource.clip = pauseMusic;
        musicSource.Play();
    }

    public void Resume(){
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        musicSource.clip = gameMusic;
        musicSource.Play();
    }

    public void ClickAndLoadMainMenu(){
        Time.timeScale = 1;
        Invoke("LoadMainMenu", 0.3f);
    }
    public void LoadMainMenu(){
        SceneManager.LoadScene("Main Menu");
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
