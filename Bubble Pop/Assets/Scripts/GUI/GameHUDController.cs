﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUDController : MonoBehaviour {

	public static GameHUDController instance;

	[SerializeField] private GameObject m_timeModeImg;
	[SerializeField] private GameObject m_endlessModeImg;
//	[SerializeField] private GameObject m_startBtn;
	[SerializeField] private Text m_stopWatchText;
	[SerializeField] private Text m_bubblesToPopText;
	[SerializeField] private Text m_bubblesCountText;

	private float m_timeStopWatch;
	private bool m_isTimeRunning;
	private int m_bubblesCount;
	private int m_maxBubbles;

	void Awake() {
		instance = this;
	}

	void Start() {
		Init ();
	}

	private void Init() {
		if(GameController.instance.gameModeType == GameModeType.timeMode) {
			m_timeModeImg.SetActive(true);
			m_endlessModeImg.SetActive(false);
		} else if(GameController.instance.gameModeType == GameModeType.endlessMode) {
			m_timeModeImg.SetActive(false);
			m_endlessModeImg.SetActive(true);
		}

//		m_startBtn.SetActive(true);
		ResetTime();
		InitBubbleCounter();
	}

	private void InitBubbleCounter() {
		GameModeType _gameModeType = GameController.instance.gameModeType;
		GameMode _gameMode = GameController.instance.gameMode;
		switch(_gameModeType) {
		case GameModeType.timeMode:
			switch(_gameMode) {
			case GameMode.timeMode50:
				m_bubblesCount = 0;
				m_maxBubbles = 50;
				break;
			case GameMode.timeMode100:
				m_bubblesCount = 0;
				m_maxBubbles = 100;
				break;
			case GameMode.timeMode150:
				m_bubblesCount = 0;
				m_maxBubbles = 150;
				break;
			}
			m_bubblesToPopText.text = m_bubblesCount + "/" + m_maxBubbles;
			break;
		case GameModeType.endlessMode:
			switch(_gameMode) {
			case GameMode.endlessMode5:
				m_bubblesCount = 5;
				m_maxBubbles = 5;
				break;
			case GameMode.endlessMode25:
				m_bubblesCount = 25;
				m_maxBubbles = 25;
				break;
			case GameMode.endlessMode50:
				m_bubblesCount = 50;
				m_maxBubbles = 50;
				break;
			}
			m_bubblesCountText.text = m_bubblesCount + "/" + m_maxBubbles;
			break;
		}
	}

//	public void StartGame() {
//		m_startBtn.SetActive(false);
//		GameController.instance.ChangeState(GameState.playing);
//		LevelController.instance.StartGame();
//		StartTime();
//	}

	public void StartTime() {
		m_isTimeRunning = true;
	}

	public void StopTime() {
		m_isTimeRunning = false;
	}

	public float GetTime() {
		return m_timeStopWatch;
	}

	public void ResetTime() {
		m_timeStopWatch = 0;
		m_stopWatchText.text = "0s";
		m_isTimeRunning = false;
	}

	public void SetBubblesCount(int p_count) {
		m_bubblesCount = p_count;
	}

	void Update() {
		if(m_isTimeRunning) {
			m_timeStopWatch += Time.deltaTime;
			m_stopWatchText.text = m_timeStopWatch.ToString("F2") + "s";
		}

		m_bubblesCount = BubbleCreator.instance.goodBubblesCount;
		m_bubblesToPopText.text = m_bubblesCount + "/" + m_maxBubbles;
		m_bubblesCountText.text = m_bubblesCount + "/" + m_maxBubbles;
	}
}
