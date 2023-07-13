using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("UI")]
    public GameObject pauseScreen;

    public Text currentHP;
    public Text currentMS;
    public Text currentRec;
    public Text currentMight;
    public Text currentProjSpeed;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else
            Destroy(gameObject);
        DisableScreens();
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

    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    public GameState currentState;
    public GameState prevState;

    public void ChangeState(GameState state)
    {
        currentState = state;
    }
    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            prevState = currentState;
            ChangeState(GameState.Paused);
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            Time.timeScale = 0f;
            Debug.Log("Paused");
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(prevState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            Debug.Log($"{pauseScreen}");
            Debug.Log("Unpaused");
        }
    }

    void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void DisableScreens()
    {
        pauseScreen.SetActive(false);
    }

    public void DisableGame()
    {
        GameManager.instance.gameObject.SetActive(false);
        Destroy(GameManager.instance.gameObject);
    }
}
