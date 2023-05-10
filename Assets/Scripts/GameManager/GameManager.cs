using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; 
    //"public static" makes sure this GameManger obj can be accessed throughout the program 
    //and Make GameManager obj we created to be gamemanger itself instead of an instance

    // public  float defaultMaxHP = 5;
    // public  int defaultCoinAmount = 0;
    // public  float defaultBulletSpeed = 10;
    // public  float defaultBulletDamage = 1;
    // public  float defaultAttackSpeed = 1;
    // public  float defaultMoveSpeed = 5;
    
    
    public static float playerMaxHP = 5;
    public static float playerCurHP = 5;
    public static int playerCoinAmount = 0;
    public static float playerBulletSpeed = 10;
    public static float minPlayerBulletDamage = 1;
    public static float maxPlayerBulletDamage = 3;
    public static float playerAttackSpeed = 1;
    public static float playerMoveSpeed = 5;


    public static int level = 1;
    
    public static int enemyCount;
    public BoardManager boardScript;


    //Debug only
    public int enemy;
    public int curLevel;

    private GameObject shop;
    public static bool inShop = false;
    
    
    public static GameObject gameOver;
    public static GameObject pause;

    
   
    void Awake()
    {   
        
        // Make sure we don't end up with two instance of Game Manager
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        
        //Make sure GameManger persist when going to next level
        DontDestroyOnLoad(gameObject);

        gameOver = GameObject.FindWithTag("GameOver"); 
        pause = GameObject.Find("Pause");
        shop = GameObject.Find("Shop");
        boardScript = GetComponent<BoardManager>();
        InitGame(); 
    }

    void InitGame(){
        enemyCount = boardScript.SetupScene(level);
        SoundManager.instance.SetBGMVolume(0.5f);
        
        if (level % 5 == 0) {
            ActivateShop();
        } else {
            shop.SetActive(false);
            inShop = false;
        }
        
        pause.SetActive(false);

        gameOver.SetActive(false);
        for (int i = 0; i < gameOver.transform.childCount; i++)
        {
            gameOver.transform.GetChild(i).gameObject.SetActive(false);
        }
        
    }

    void ActivateShop(){
        shop.SetActive(true);
        inShop = true;
    }

    public void GameOver() {
        Debug.Log("Game is over");
        //enabled = false;
        SoundManager.instance.PauseBGM();
        SoundManager.instance.PlayGameOver();
        gameOver.SetActive(true);
        for (int i = 0; i < gameOver.transform.childCount; i++)
        {
            gameOver.transform.GetChild(i).gameObject.SetActive(true);
        }
    }



    public void Restart(){
        playerMaxHP = 5;
        playerCurHP = playerMaxHP;
        playerCoinAmount = 0;
        playerBulletSpeed = 10;
        minPlayerBulletDamage = 1;
        maxPlayerBulletDamage = 3;
        playerAttackSpeed = 1;
        playerMoveSpeed = 5;
        level = 1;
        Time.timeScale = 1f;
    }
    




   
    void Update()
    {
        //Debug only
        enemy = enemyCount;
        curLevel = level;
    }
}
