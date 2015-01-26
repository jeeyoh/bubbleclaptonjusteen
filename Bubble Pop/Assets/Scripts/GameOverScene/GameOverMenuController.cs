using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenuController : MonoBehaviour {

	public static GameOverMenuController instance;

	[SerializeField] private GameObject m_timeModePanel;
	[SerializeField] private GameObject m_timeMode50Img;
	[SerializeField] private GameObject m_timeMode100Img;
	[SerializeField] private GameObject m_timeMode150Img;
	[SerializeField] private GameObject m_endlessModePanel;
	[SerializeField] private GameObject m_endlessMode5Img;
	[SerializeField] private GameObject m_endlessMode25Img;
	[SerializeField] private GameObject m_endlessMode50Img;
	[SerializeField] private Text m_timeScoreLbl;
	[SerializeField] private Text m_bestTimeScoreLbl;

	void Awake() {
		instance = this;
	}

	void Start() {
		Init() ;
		SetScore();
	}

	private void Init() {
		m_timeMode50Img.SetActive(false);
		m_timeMode100Img.SetActive(false);
		m_timeMode150Img.SetActive(false);
		m_timeMode50Img.SetActive(false);
		m_timeMode100Img.SetActive(false);
		m_timeMode150Img.SetActive(false);
	}

	private void SetScore() {
		GameMode _gameMode = GameController.instance.gameMode;
		switch(_gameMode) {
		case GameMode.timeMode50:
			m_timeMode50Img.SetActive(true);
			m_timeModePanel.SetActive(true);
			m_endlessModePanel.SetActive(false);
			break;
		case GameMode.timeMode100:
			m_timeMode100Img.SetActive(true);
			m_timeModePanel.SetActive(true);
			m_endlessModePanel.SetActive(false);
			break;
		case GameMode.timeMode150:
			m_timeMode150Img.SetActive(true);
			m_timeModePanel.SetActive(true);
			m_endlessModePanel.SetActive(false);
			break;
		case GameMode.endlessMode5:
			m_endlessMode5Img.SetActive(true);
			m_timeModePanel.SetActive(false);
			m_endlessModePanel.SetActive(true);
			break;
		case GameMode.endlessMode25:
			m_endlessMode25Img.SetActive(true);
			m_timeModePanel.SetActive(false);
			m_endlessModePanel.SetActive(true);
			break;
		case GameMode.endlessMode50:
			m_endlessMode50Img.SetActive(true);
			m_timeModePanel.SetActive(false);
			m_endlessModePanel.SetActive(true);
			break;
		}
		m_timeScoreLbl.text = GameController.instance.playerTimeScore.ToString("F2");
		m_bestTimeScoreLbl.text = GameController.instance.TimeModeBestScore.ToString("F2");
	}

	public void ReturnToMain() {
		GameController.instance.ChangeState(GameState.main);
	}

	public void ShareGame() {
		Debug.Log("Share score!");
	}

	public void PlayAgain() {
		GameController.instance.ChangeState(GameState.startGame);
	}
}
