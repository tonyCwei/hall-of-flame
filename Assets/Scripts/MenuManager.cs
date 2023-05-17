using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{   
    public GameObject tutorial;

    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void StartGame(){
        GameManager.isPaused = false;
        SceneManager.LoadScene("GameScene");
    }

    public void ToggleTutorial() {
       tutorial.SetActive(!tutorial.activeSelf);
        
    }
    public void Restart() {
        GameManager.instance.Restart();
        SoundManager.instance.PlayBGM();
        GameManager.isPaused = false;
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu() {
        GameManager.instance.Restart();
        SoundManager.instance.SetBGMVolume(1f);
        SoundManager.instance.PlayBGM();
        GameManager.isPaused = false;
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void Pause() {
        SoundManager.instance.PauseBGM();
        GameManager.pause.SetActive(true);
        GameManager.isPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume() {
        SoundManager.instance.ResumeBGM();
        GameManager.pause.SetActive(false);
        GameManager.isPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
