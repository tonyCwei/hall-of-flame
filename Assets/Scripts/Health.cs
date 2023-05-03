using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{   
    public float health;
    //public float numofHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Player playerScript;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

 
    void Update()
    {
        health = playerScript.curHP;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
