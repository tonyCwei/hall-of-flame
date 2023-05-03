using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    [Serializable]
    public class Count {
        public int minimum;
        public int maximum;

        public Count (int min, int max) {
            minimum = min; 
            maximum = max;
        }
    }


    public int columns = 14;
    public int rows = 8;
    
    public GameObject[] floorTiles;
    public GameObject[] hazardTiles;
    public GameObject[] normalEnemyTiles;
    public GameObject[] upgradeEnemyTiles;
    public Count hazardCount = new Count(5,10);


    private Transform boardHolder;
    private List <Vector3> gridPositions = new List<Vector3>();

   

    //fill list with vector3 of the grid positions
    void InitialiseList() {
        gridPositions.Clear();
        for (int x = 0; x < columns; x++) {
            for (int y = 0 ; y < rows; y++) {
                gridPositions.Add(new Vector3(x,y,0f));
            }
        }
    }


    //Set up floors
    void BoardSetup() {
        boardHolder = new GameObject("Board").transform;
        for (int x = 0; x < columns; x++) {
            for (int y = 0 ; y < rows; y++) {
                GameObject toInstantiate = floorTiles[Random.Range(0,floorTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    //Place random enemies
    Vector3 RandomPosition() {
        int randomIndex = Random.Range(1, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex); // to avoid spawning enemy in the same grid
        return randomPosition;
    }

    int LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum) {
        int objectCount = Random.Range (minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++) {
            Vector3 randomPosition = RandomPosition();
            if ( randomPosition.x == 0 || randomPosition.x == 13 || randomPosition.y == 0 || randomPosition.y == 7){
                i--;
                continue;
            }
            GameObject titleChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(titleChoice, randomPosition, Quaternion.identity);
        }
        return objectCount;
    }

    void LayoutHazardAtRandom(GameObject[] tileArray, int minimum, int maximum) {
        int objectCount = Random.Range (minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++) {
            Vector3 randomPosition = RandomPosition();
            if ( randomPosition.x >= 10 && randomPosition.x <= 13  && randomPosition.y >= 4 && randomPosition.y <= 7 
               ) 
            {
                continue;
            }
            GameObject titleChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(titleChoice, randomPosition, Quaternion.identity);
        }
    }

    public int SetupScene(int level) {
        BoardSetup();
        InitialiseList();
        
        int enemyRange = (int)Mathf.Sqrt(level); // log growth of enemy
        int enemyCount;

        if (level == 0 || level % 5 == 0) {
            return 0;
        }

        if (level == 1 || level == 2) {
            enemyCount = LayoutObjectAtRandom(normalEnemyTiles, 1, 1);
            LayoutHazardAtRandom(hazardTiles, 2, 2);
        }  else if (level <= 10){
            enemyCount = LayoutObjectAtRandom(normalEnemyTiles, enemyRange, enemyRange + 1);
            LayoutHazardAtRandom(hazardTiles, 2, 4);
        } else if (level > 10 && level <= 30){
            enemyCount = LayoutObjectAtRandom(normalEnemyTiles, enemyRange/2, (enemyRange/2) + 1) + 
                         LayoutObjectAtRandom(upgradeEnemyTiles, enemyRange/2, (enemyRange/2) + 1);
            LayoutHazardAtRandom(hazardTiles, 4, 6);
            
        } else {
            enemyCount = LayoutObjectAtRandom(upgradeEnemyTiles, enemyRange, enemyRange + 1);
            LayoutHazardAtRandom(hazardTiles, hazardCount.minimum, hazardCount.maximum);
        }
        return enemyCount;

    }
    
   


}
