using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{   

    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void StartGame(){
        SceneManager.LoadScene("GameScene");
    }

    public void Restart() {
        GameManager.instance.Restart();
        SoundManager.instance.PlayBGM();
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu() {
        GameManager.instance.Restart();
        SoundManager.instance.SetBGMVolume(1f);
        SoundManager.instance.PlayBGM();
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void Pause() {
        SoundManager.instance.PauseBGM();
        GameManager.pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        SoundManager.instance.ResumeBGM();
        GameManager.pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
