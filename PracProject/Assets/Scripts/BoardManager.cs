using System.Collections;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.UIElements;
using JetBrains.Annotations;

    public class BoardManager : MonoBehaviour
    {
       
        public int columns;
        public int rows;
        public int wallCount_min = 4;
        public int wallCount_max = 9;
        public int healthpackcount_min = 1;
        public int healthpackcount_max = 3;
        public GameObject[] doorTiles;
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] healthTiles;
        public GameObject[] enemyTiles;
        public GameObject[] outerWallCorner;
        public GameObject[] outerWallTilesLeft;
        public GameObject[] outerWallTilesRight;
        public GameObject[] outerWallTilesTop;
        public GameObject[] outerWallTilesBottom;
        public GameObject[] bossTiles;

        private Transform boardholder;

        private List<Vector3> gridPos = new List<Vector3>();

        void Awake()
        {
            columns = Random.Range(25, 50);
        rows = Random.Range(25, 50);
        }

        void initializeLst()
        {
            gridPos.Clear();
            for (int x = 1; x < columns; x++)
            {
                for (int y = 1; y < rows; y++)
                {
                    gridPos.Add(new Vector3(x, y, 0f));
                }
            }
        }
        void BoardSetup(int xX = 0, int yY = 0)
        {
            boardholder = new GameObject("Board").transform;
            for (int x = xX+columns; x > xX-2; x--)
            {
                for (int y = yY+rows; y > yY-2; y--)
                {
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    /*if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    }*/

                    GameObject wall;
                    if (x == xX-1)
                    {
                        if (y == yY-1) wall = outerWallCorner[0];
                        else if (y == yY + rows) wall = outerWallCorner[1];
                        else wall = outerWallTilesLeft[Random.Range(0,outerWallTilesLeft.Length)];
                        GameObject wallinstance = Instantiate(wall, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                        wallinstance.transform.SetParent(boardholder);
                    }
                    else if(x == xX+columns)
                    {
                        if (y == yY-1) wall = outerWallCorner[2];
                        else if (y == yY + rows) wall = outerWallCorner[3];
                        else wall = outerWallTilesRight[Random.Range(0, outerWallTilesRight.Length)];
                        GameObject wallinstance = Instantiate(wall, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                        wallinstance.transform.SetParent(boardholder);
                    }
                    else if(y == yY-1)
                    {
                        if (x == xX - 1) wall = outerWallCorner[0];
                        else if (x == xX+columns) wall = outerWallCorner[2];
                        else wall = outerWallTilesBottom[Random.Range(0,outerWallTilesBottom.Length)];
                        GameObject wallinstance = Instantiate(wall, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
                        wallinstance.transform.SetParent(boardholder);
                    }
                    else if(y == yY + rows)
                    {
                        if (x == xX-1) wall = outerWallCorner[1];
                        else if (x == xX+columns) wall = outerWallCorner[3];
                        else wall = outerWallTilesTop[Random.Range(0,outerWallTilesTop.Length)];
                        GameObject wallinstance = Instantiate(wall, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
                        wallinstance.transform.SetParent(boardholder);
                    }
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardholder);
                }
            }
        }
        Vector3 RandomPos()
        {
            int randomIndex = Random.Range(0, gridPos.Count);
            Vector3 randomPosition = gridPos[randomIndex];
            gridPos.RemoveAt(randomIndex);
            return randomPosition;
        }

        void LayoutObjAtRand(GameObject[] tileArray, int minimum, int maximum)
        {
            int objectCount = Random.Range(minimum, maximum + 1);

            for (int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = RandomPos();

                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }

        void RandomDoors(GameObject[] tileArray, int minimum = 1, int maximum = 4)
        {
            int C = Random.Range(minimum, maximum);
            for (int i = 0; i < C; i++)
            {
                Vector3 pos = new Vector3(Random.Range(0, columns-1), rows, 0f);
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
                Instantiate(tileChoice, pos, Quaternion.identity);
            }
        }
        public void SetupScene(int level, int x = 0, int y = 0)
        {
            BoardSetup(x,y);
            initializeLst();

            LayoutObjAtRand(wallTiles, wallCount_min, wallCount_max);

            LayoutObjAtRand(healthTiles, healthpackcount_min, healthpackcount_max);
            int enemyCount = (int)Mathf.Log(level, 2f);
            int bossCount = 0;
            if (level % 5 == 0)
            {
                bossCount = 1;
                enemyCount = 0;
            }
        //Debug.Log($"{enemyCount}{bossCount}{level}");
            LayoutObjAtRand(enemyTiles, enemyCount, enemyCount);
            LayoutObjAtRand(bossTiles, bossCount, bossCount);
            RandomDoors(doorTiles);

        }
        
    
    }