using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    private Player playerScript;
    private GameObject shop;
    private GameObject coinMessage;
    private GameObject hpMessage;

    public GameObject FireballSpeedButton;
    public GameObject MoveSpeedButton;
    public GameObject AttackDmgButton;
    public GameObject AttackSpeedButton;
    public GameObject restoreButton;
    

  
    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        shop = GameObject.Find("Shop");
        coinMessage = GameObject.Find("CoinMessage");
        if (coinMessage != null) {
            coinMessage.SetActive(false);
        }

        hpMessage = GameObject.Find("HpMessage");
        if (hpMessage != null) {
            hpMessage.SetActive(false);
        }
        
        InitShop();
    }

    void InitShop(){
        float ran1 = Random.Range (1, 10);
        float ran2 = Random.Range (1,10);

        FireballSpeedButton.SetActive(ran1 <= 5);
        MoveSpeedButton.SetActive(ran1 > 5);

        AttackDmgButton.SetActive(ran2 <= 5);
        AttackSpeedButton.SetActive(ran2 > 5);
    }
    
   
    public void DeactivateShop(){
        GameManager.inShop = false;
        shop.SetActive(false);
    }

    public bool CheckCoins(int purchaseAmount){
        if (!playerScript.EnoughCoin(purchaseAmount)) {
            StartCoroutine(CoinMessage());
            return false;
        } else {
            return true;
        }
    }

    IEnumerator CoinMessage() {
        coinMessage.SetActive(true);
        yield return new WaitForSeconds(1f);
        coinMessage.SetActive(false);
    }

    public void Skip(){
        DeactivateShop();
    }

    public void FireballSpeed(){
        int purchaseAmount = 3;
        if (CheckCoins(purchaseAmount)){
            playerScript.LoseCoin(purchaseAmount);
            playerScript.bulletSpeed += 2;
            FireballSpeedButton.SetActive(false);
        }
    }
    public void MoveSpeed(){
        int purchaseAmount = 3;
        if (CheckCoins(purchaseAmount)){
            playerScript.LoseCoin(purchaseAmount);
            playerScript.moveSpeed += 0.5f;
            MoveSpeedButton.SetActive(false);
        }
    }

    public void AttackDamage(){
        int purchaseAmount = 6;
        if (CheckCoins(purchaseAmount)){
            playerScript.LoseCoin(purchaseAmount);
            playerScript.minPlayerBulletDamage++;
            playerScript.maxPlayerBulletDamage++;
            AttackDmgButton.SetActive(false);
        } 
    }

    public void AttackSpeed(){
        int purchaseAmount = 6;
        if (CheckCoins(purchaseAmount)){
            playerScript.LoseCoin(purchaseAmount);
            playerScript.attackSpeed += 0.3f;
            AttackSpeedButton.SetActive(false);
        } 
    }

    public void RestoreHP(){
        int purchaseAmount = 9;
        if (CheckCoins(purchaseAmount)) {
            if (playerScript.curHP == playerScript.maxHP){
                StartCoroutine(HpMessage());
            } else {
                playerScript.LoseCoin(purchaseAmount);
                playerScript.AddCurHP(3);
                restoreButton.SetActive(false);
            }
            
        }
        
    }

    IEnumerator HpMessage(){
        hpMessage.SetActive(true);
        yield return new WaitForSeconds(1f);
        hpMessage.SetActive(false);
    }


    

    

}
