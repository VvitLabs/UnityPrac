using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = 0.05f;
    public static GameManager instance = null;
    public float playerHp = 5;
    public int playerScore = 0;
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
        DisableScreens();
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
    }
    private void Start()
    {
        InitGame(level);
    }
    void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckPause();
                break;
            case GameState.Paused:
                CheckPause();
                break;
            case GameState.GameOver:
                break;
            default:
                Debug.LogWarning("UnknownState");
                break;
        }
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

    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    public GameState currentState;
    public GameState prevState;

    [Header("UI")]
    public GameObject pausescreen;

    public void ChangeState(GameState state) {
    currentState = state;
    }
    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            prevState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pausescreen.SetActive(true);
            Debug.Log("Paused");
        }
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(prevState);
            Time.timeScale = 1f;
            pausescreen.SetActive(false);
            Debug.Log("Unpaused");
        }
    }

    void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused) ResumeGame();
            else PauseGame();
        }
    }
     
    private void DisableScreens()
    {
        pausescreen.SetActive(false);
    }
}
