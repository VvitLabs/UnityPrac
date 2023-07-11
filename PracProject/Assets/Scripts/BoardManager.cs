using System.Collections;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [System.Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count ( int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = Random.Range(8, 11);
    public int rows = Random.Range(8, 11);
    public Count wallCount = new Count (4,11);
    public Count healthpackcount = new Count(1, 5);
    public GameObject[] doorTiles;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] healthTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] bossTiles;

    private Transform boardholder;

    private List<Vector3> gridPos = new List<Vector3>();

    void initializeLst()
    {
        gridPos.Clear();
        for(int x = 1; x < columns; x++)
        {
            for (int y = 1; y < rows; y++)
            {
                gridPos.Add(new Vector3(x, y, 0f));
            }
        }
    }
    void BoardSetup()
    {
        boardholder = new GameObject("Board").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3 (x,y,0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardholder);
            }
        }
    }
    Vector3 RandomPos()
    {
        int randIdx = Random.Range(0, gridPos.Count);
        Vector3 randomPos = gridPos[randIdx];
        gridPos.RemoveAt(randIdx);

        return randomPos;
    }

    void LayoutObjAtRand(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectC = Random.Range (minimum, maximum);  
        for(int i = 0; i <objectC; i++)
        {
            Vector3 randomPos = RandomPos();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPos, Quaternion.identity);

        }
    }

    void RandomDoors(GameObject[] tileArray, int minimum = 1, int maximum = 4)
    {
        int C = Random.Range (minimum, maximum);
        for (int i = 0;i < C;i++)
        {
            Vector3 pos = new Vector3(Random.Range(1, columns - 1) * ((i + 1) % 2), Random.Range(1, rows - 1) * (i % 2), 0f);
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate (tileChoice, pos, Quaternion.identity);
        }
    }
    public void SetupScene (int level)
    {
        BoardSetup();
        initializeLst();

        LayoutObjAtRand (wallTiles, wallCount.minimum, wallCount.maximum);

        LayoutObjAtRand (healthTiles, healthpackcount.minimum, healthpackcount.maximum);
        int enemyCount = (int)Mathf.Log(level, 2f);
        int bossCount = 0;
        if (level % 5 == 0) bossCount = 1;
        LayoutObjAtRand(enemyTiles, enemyCount, enemyCount);
        LayoutObjAtRand(bossTiles, bossCount, bossCount);
        RandomDoors(doorTiles);

    }
}
