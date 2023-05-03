using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    private  TMP_Text levelText;
    private  TMP_Text coinText;
    private  TMP_Text attackDamageText;
    private  TMP_Text attackSpeedText;
    private  TMP_Text bulletSpeedText;
    private  TMP_Text moveSpeedText;
    
    
    private Player playerScript;

    void Start()
    {   
        //Update level text
        levelText = GameObject.Find("LevelText").GetComponent<TMP_Text>();
        levelText.text = "LEVEL : " + GameManager.level;

        //Get Player Script
        playerScript = GameObject.Find("Player").GetComponent<Player>();

        //Get Player Stats Texts
        coinText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
        attackDamageText = GameObject.Find("AttackDamageText").GetComponent<TMP_Text>();
        attackSpeedText = GameObject.Find("AttackSpeedText").GetComponent<TMP_Text>();
        bulletSpeedText = GameObject.Find("BulletSpeedText").GetComponent<TMP_Text>();
        moveSpeedText = GameObject.Find("MoveSpeedText").GetComponent<TMP_Text>();


    }


    void Update()
    {
        coinText.text = "COINS : " + playerScript.coinAmount;
        attackDamageText.text = "ATTACK DAMAGE : \n" + playerScript.minPlayerBulletDamage 
                                + " - " + playerScript.maxPlayerBulletDamage;
        attackSpeedText.text = "ATTACK SPEED : \n" + playerScript.attackSpeed;
        bulletSpeedText.text = "FIREBALL SPEED : \n" + playerScript.bulletSpeed;
        moveSpeedText.text = "MOVE SPEED : \n" + playerScript.moveSpeed;
        

    }



}
