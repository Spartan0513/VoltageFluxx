﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameState;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour, IGameState
{
    private static LevelManager levelManager;
    [SerializeField] private float timeToBeat = 0.0f;
    private float currentTimer = 0.0f;

    [SerializeField] private int freezeLimit = 0;
    private int timesFrozen = 0;
    [SerializeField] private bool canPlayerFreeze = true;

    private GameStateManager gsManager;
    private e_GAMESTATE state;

    private GameObject GUIObj;
    private GameObject LevelCompleteGUIObj;
    private GameObject StartMenuGUIObj;

    [SerializeField] private List<BaseCollectable> collectables = new List<BaseCollectable>();
    public int CollectableAmount { get; private set; }
    public int CurrentCollectableCount { get; set; }

    public static LevelManager GetInstance()
    {
        if (levelManager == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("LevelManager");

            if (go == null)
            {
                go = new GameObject();
                go.name = "LevelManager";
                go.tag = "LevelManager";
                levelManager = go.AddComponent<LevelManager>();
            }
            else if (go.GetComponent<LevelManager>() == null)
                levelManager = go.AddComponent<LevelManager>();
            else
                levelManager = go.GetComponent<LevelManager>();
        }

        return levelManager;
    }

    void Start()
    {
        levelManager = LevelManager.GetInstance();
        gsManager = GameStateManager.GetInstance();
        state = gsManager.GetGameState();
        gsManager.GameStateSubscribe(this.gameObject);

        GUIObj = GameObject.FindGameObjectWithTag("GUI");

        LevelCompleteGUIObj = GameObject.FindGameObjectWithTag("LevelCompleteGUI");
        LevelCompleteGUIObj.SetActive(false);
        StartMenuGUIObj = GameObject.FindGameObjectWithTag("StartMenuGUI");

        //LevelCompleteGUIObj.SetActive(false);

        CurrentCollectableCount = 0;
        CollectableAmount = collectables.Count;
        if (PersistentGUI.Instance != null)
        {
            PersistentGUI.Instance.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (state == e_GAMESTATE.PLAYING || state == e_GAMESTATE.PAUSED)
        {
            currentTimer += Time.deltaTime;
        }
    }

	public void ChangeState(e_GAMESTATE e_state)
	{
		if (state == e_GAMESTATE.PLAYING && e_state == e_GAMESTATE.LEVELCOMPLETE)
		{
			GUIObj.transform.Find("LevelCompleteGUI").gameObject.SetActive(true);

            double timeTaken = System.Math.Round(currentTimer, 2);
            double recommendedTime = System.Math.Round(timeToBeat, 2);
            LevelCompleteGUIObj.GetComponent<LevelCompleteController>().CompleteLevel(SceneManager.GetActiveScene().buildIndex, timeTaken, recommendedTime, timesFrozen);
		}

        if (state == e_GAMESTATE.MENU && e_state == e_GAMESTATE.PLAYING)
        {
            StartMenuGUIObj.SetActive(false);
            if (PersistentGUI.Instance != null)
            {
                PersistentGUI.Instance.gameObject.SetActive(false);
            }
        }

        state = e_state;
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level5")
            SceneManager.LoadScene("LevelSelect");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToLevelSelect()
    {
        SceneManager.LoadScene("HomeMenu");
    }

    public bool GetPlayerFreezeStatus()
    {
        return canPlayerFreeze;
    }

    public void ToggleLevelFreeze()
    {
        if (state == e_GAMESTATE.PLAYING)
        {
            if (timesFrozen < freezeLimit || freezeLimit < 0)
            {
                gsManager.SetGameState(e_GAMESTATE.PAUSED);
                timesFrozen++;
            }
        }
        else if (state == e_GAMESTATE.PAUSED)
            gsManager.SetGameState(e_GAMESTATE.PLAYING);
    }

    public void StartLevel()
    {
        if(state == e_GAMESTATE.MENU)
        {
            gsManager.SetGameState(e_GAMESTATE.PLAYING);
        }
    }
}
