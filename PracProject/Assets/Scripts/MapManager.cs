using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static Tile[,] map;
}

public class Tile{
    public int xPos;
    public int yPos;
    public GameObject baseObject;

}

