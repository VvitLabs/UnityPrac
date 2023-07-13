using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = 0.05f;
    public static GameManager instance = null;
    public float playerHp = 5;
    public int playerScore = 0;
    public bool nts = true;

    [HideInInspector] public bool playersTurn = true;

    private Text levelText;
    private BoardManager boardScript;
    private GameObject levelImage;
    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        nts = true;
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
    }
    private void Start()
    {
        
        //InitGame(level);
    }

    void InitGame(int level)
    {
        
        doingSetup = true;
        
        enemies.Clear();
        boardScript.SetupScene(level);
    }
    void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame(level);
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);

    }
}
