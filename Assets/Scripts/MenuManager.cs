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

    public void Restart() {
        GameManager.instance.Restart();
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu() {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void Pause() {
        GameManager.pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        GameManager.pause.SetActive(false);
        Time.timeScale = 1f;
    }


}
