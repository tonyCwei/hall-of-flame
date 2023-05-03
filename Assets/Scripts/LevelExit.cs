using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] GameObject Runes;

    

    private BoxCollider2D myBoxCollider;
    
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        
    }

    
    void Update()
    {   
        CheckOpen();
        
    }


    void CheckOpen() {
        if (GameManager.enemyCount == 0) {
            Runes.SetActive(true);
            myBoxCollider.enabled = true;
        }
    }



}
