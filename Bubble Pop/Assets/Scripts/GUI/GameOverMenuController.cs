using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenuController : MonoBehaviour {

	public static GameOverMenuController instance {get; private set;}

	[SerializeField] private GameObject m_failedPanel;
	[SerializeField] private GameObject m_currentTimeScoreImg;
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
	[SerializeField] private GameObject m_blocker;

	private Animator m_targetBubbleAnimator;

	void Awake() {
		instance = this;
	}

	void Start() {
		Init() ;
		SetScore();
	}

	private void Init() {
//		SoundController.instance.PlayMenuBGM();
		m_failedPanel.SetActive(false);
		m_timeMode50Img.SetActive(false);
		m_timeMode100Img.SetActive(false);
		m_timeMode150Img.SetActive(false);
		m_endlessMode5Img.SetActive(false);
		m_endlessMode25Img.SetActive(false);
		m_endlessMode50Img.SetActive(false);
		m_blocker.SetActive(false);
	}

	private void SetScore() {
		float _bestTime = 0f;
		GameMode _gameMode = GameController.instance.gameMode;
		switch(_gameMode) {
		case GameMode.timeMode50:
			m_timeMode50Img.SetActive(true);
			m_timeModePanel.SetActive(true);
			m_endlessModePanel.SetActive(false);
			m_targetBubbleAnimator = m_timeMode50Img.GetComponent<Animator>();
			_bestTime = GameController.instance.TimeMode50BestTime;
			break;
		case GameMode.timeMode100:
			m_timeMode100Img.SetActive(true);
			m_timeModePanel.SetActive(true);
			m_endlessModePanel.SetActive(false);
			m_targetBubbleAnimator = m_timeMode100Img.GetComponent<Animator>();
			_bestTime = GameController.instance.TimeMode100BestTime;
			break;
		case GameMode.timeMode150:
			m_timeMode150Img.SetActive(true);
			m_timeModePanel.SetActive(true);
			m_endlessModePanel.SetActive(false);
			m_targetBubbleAnimator = m_timeMode150Img.GetComponent<Animator>();
			_bestTime = GameController.instance.TimeMode150BestTime;
			break;
		case GameMode.endlessMode5:
			m_endlessMode5Img.SetActive(true);
			m_timeModePanel.SetActive(false);
			m_endlessModePanel.SetActive(true);
			m_targetBubbleAnimator = m_endlessMode5Img.GetComponent<Animator>();
			_bestTime = GameController.instance.EndlessMode5BestTime;
			break;
		case GameMode.endlessMode25:
			m_endlessMode25Img.SetActive(true);
			m_timeModePanel.SetActive(false);
			m_endlessModePanel.SetActive(true);
			m_targetBubbleAnimator = m_endlessMode25Img.GetComponent<Animator>();
			_bestTime = GameController.instance.EndlessMode25BestTime;
			break;
		case GameMode.endlessMode50:
			m_endlessMode50Img.SetActive(true);
			m_timeModePanel.SetActive(false);
			m_endlessModePanel.SetActive(true);
			m_targetBubbleAnimator = m_endlessMode50Img.GetComponent<Animator>();
			_bestTime = GameController.instance.EndlessMode50BestTime;
			break;
		}
		m_targetBubbleAnimator.SetTrigger("Show");

		GameModeType _gameModeType = GameController.instance.gameModeType;
		switch(_gameModeType) {
		case GameModeType.timeMode:
			if(GameController.instance.timeModeSuccess) {
				if(GameController.instance.playerTimeScore <= _bestTime) {
					SoundController.instance.PlaySuccessBGM();
				} else {
					SoundController.instance.PlayMenuBGM(1.5f);
				}
				m_timeScoreLbl.text = GameController.instance.playerTimeScore.ToString("F2");
			} else {
				SoundController.instance.PlayGameOverBGM();
				m_failedPanel.SetActive(true);
				m_currentTimeScoreImg.SetActive(false);
			}
			if(_bestTime == -1f) {
				m_bestTimeScoreLbl.text = "";
			} else {
				m_bestTimeScoreLbl.text = _bestTime.ToString("F2");
			}
			break;
		case GameModeType.endlessMode:
			if(GameController.instance.playerTimeScore >= _bestTime) {
				SoundController.instance.PlayNewBestTimeBGM();
			} else {
				SoundController.instance.PlayMenuBGM(1);
//				SoundController.instance.PlayGameOverBGM();
			}
			m_timeScoreLbl.text = GameController.instance.playerTimeScore.ToString("F2");
			m_bestTimeScoreLbl.text = _bestTime.ToString("F2");
			break;
		}
	}

	public void ReturnToMain() {
//		m_targetBubbleAnimator.SetTrigger("Pop");
//		GameController.instance.ChangeState(GameState.main);
		StartCoroutine(ChangeState(GameState.main, 1f));
	}

	public void ShareGame() {
		Debug.Log("Share score!");
		m_targetBubbleAnimator.Play("Bubble_Pop");
		SoundController.instance.PlayBubblePoppedSFX();
		SoundController.instance.StopMusic();
	}

	public void PlayAgain() {
//		m_targetBubbleAnimator.SetTrigger("Pop");
//		GameController.instance.ChangeState(GameState.startGame);
		StartCoroutine(ChangeState(GameState.startGame, 1f));
	}

	private IEnumerator ChangeState(GameState p_gameState, float p_delay) {
		SoundController.instance.PlayBubblePoppedSFX();
		SoundController.instance.StopMusic();
		m_targetBubbleAnimator.Play("Bubble_Pop");
		m_blocker.SetActive(true);
		yield return new WaitForSeconds(p_delay);
		GameController.instance.ChangeState(p_gameState);
	}
}
