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
	[SerializeField] private GameObject m_rewardPopup;

	private Animator m_targetBubbleAnimator;
	
	[SerializeField] private GameObject m_SharePanel;

	bool isTimeMode = false;

	void Awake() {
		instance = this;
		
		ThirdPartyController.Instance.ShowBanner(true);
		ThirdPartyController.Instance.ShowInterstitial(true);
	}

	void Start() {
		Init() ;
		SetScore();
		Invoke("CheckForReward", 1f);
	}

	private void Init() {
//		SoundController.instance. ();
		m_failedPanel.SetActive(false);
		m_timeMode50Img.SetActive(false);
		m_timeMode100Img.SetActive(false);
		m_timeMode150Img.SetActive(false);
		m_endlessMode5Img.SetActive(false);
		m_endlessMode25Img.SetActive(false);
		m_endlessMode50Img.SetActive(false);
		m_blocker.SetActive(false);
		m_rewardPopup.SetActive(false);
	}

	private void CheckForReward() {
		GameMode _gameMode = GameController.instance.gameMode;
		switch (_gameMode) {
		case GameMode.timeMode50:
			if(GameController.instance.TimeMode50SucceedingWins >= 3) {
				GameController.instance.TimeMode50SucceedingWins = 0;
				GameController.instance.AddNoBlackBubbles(1);
				m_rewardPopup.SetActive(true);
			}
			break;
		case GameMode.timeMode100:
			if(GameController.instance.TimeMode100SucceedingWins >= 3) {
				GameController.instance.TimeMode100SucceedingWins = 0;
				GameController.instance.AddNoBlackBubbles(1);
				m_rewardPopup.SetActive(true);
			}
			break;
		case GameMode.timeMode150:
			if(GameController.instance.TimeMode150SucceedingWins >= 3) {
				GameController.instance.TimeMode150SucceedingWins = 0;
				GameController.instance.AddNoBlackBubbles(1);
				m_rewardPopup.SetActive(true);
			}
			break;
		case GameMode.endlessMode5:
			if(GameController.instance.EndlessMode5SucceedingWins >= 3) {
				GameController.instance.EndlessMode5SucceedingWins = 0;
				GameController.instance.AddNoBlackBubbles(1);
				m_rewardPopup.SetActive(true);
			}
			break;
		case GameMode.endlessMode25:
			if(GameController.instance.EndlessMode25SucceedingWins >= 3) {
				GameController.instance.EndlessMode25SucceedingWins = 0;
				GameController.instance.AddNoBlackBubbles(1);
				m_rewardPopup.SetActive(true);
			}
			break;
		case GameMode.endlessMode50:
			if(GameController.instance.EndlessMode50SucceedingWins >= 3) {
				GameController.instance.EndlessMode50SucceedingWins = 0;
				GameController.instance.AddNoBlackBubbles(1);
				m_rewardPopup.SetActive(true);
			}
			break;
		}
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
			isTimeMode = true;
			break;
		case GameMode.timeMode100:
			m_timeMode100Img.SetActive(true);
			m_timeModePanel.SetActive(true);
			m_endlessModePanel.SetActive(false);
			m_targetBubbleAnimator = m_timeMode100Img.GetComponent<Animator>();
			_bestTime = GameController.instance.TimeMode100BestTime;
			isTimeMode = true;
			break;
		case GameMode.timeMode150:
			m_timeMode150Img.SetActive(true);
			m_timeModePanel.SetActive(true);
			m_endlessModePanel.SetActive(false);
			m_targetBubbleAnimator = m_timeMode150Img.GetComponent<Animator>();
			_bestTime = GameController.instance.TimeMode150BestTime;
			isTimeMode = true;
			break;
		case GameMode.endlessMode5:
			m_endlessMode5Img.SetActive(true);
			m_timeModePanel.SetActive(false);
			m_endlessModePanel.SetActive(true);
			m_targetBubbleAnimator = m_endlessMode5Img.GetComponent<Animator>();
			_bestTime = GameController.instance.EndlessMode5BestTime;
			isTimeMode = false;
			break;
		case GameMode.endlessMode25:
			m_endlessMode25Img.SetActive(true);
			m_timeModePanel.SetActive(false);
			m_endlessModePanel.SetActive(true);
			m_targetBubbleAnimator = m_endlessMode25Img.GetComponent<Animator>();
			_bestTime = GameController.instance.EndlessMode25BestTime;
			isTimeMode = false;
			break;
		case GameMode.endlessMode50:
			m_endlessMode50Img.SetActive(true);
			m_timeModePanel.SetActive(false);
			m_endlessModePanel.SetActive(true);
			m_targetBubbleAnimator = m_endlessMode50Img.GetComponent<Animator>();
			_bestTime = GameController.instance.EndlessMode50BestTime;
			isTimeMode = false;
			break;
		}
		m_targetBubbleAnimator.SetTrigger("Show");

		GameModeType _gameModeType = GameController.instance.gameModeType;
		switch(_gameModeType) {
		case GameModeType.timeMode:
			if(GameController.instance.timeModeSuccess) {
				if(GameController.instance.playerTimeScore <= _bestTime) {
					SoundController.instance.PlayNewBestTimeBGM();
				} else {
					SoundController.instance.PlaySuccessBGM();
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
//			} else if(GameController.instance.isBlackBubbleClicked) {
//				SoundController.instance.PlayGameOverBGM();
			} else {
				SoundController.instance.PlaySuccessBGM();
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

	public void CloseRewardPopup() {
		m_rewardPopup.SetActive(false);
	}

	public void ShareGame() {
		m_targetBubbleAnimator.Play("Bubble_Pop");
		SoundController.instance.PlayBubblePoppedSFX();
		SoundController.instance.StopMusic();
		EnableSharePanel(true);
	}

	public void PlayAgain() {
//		m_targetBubbleAnimator.SetTrigger("Pop");
//		GameController.instance.ChangeState(GameState.startGame);
		StartCoroutine(ChangeState(GameState.startGame, 1f));
	}

	private IEnumerator ChangeState(GameState p_gameState, float p_delay) {
		SoundController.instance.PlayBubblePoppedSFX();
		m_targetBubbleAnimator.Play("Bubble_Pop");
		m_blocker.SetActive(true);
		yield return new WaitForSeconds(p_delay);
		SoundController.instance.StopMusic();
		GameController.instance.ChangeState(p_gameState);
	}

	void EnableSharePanel ( bool isEnable )
	{
		if ( isEnable )
		{
			m_SharePanel.gameObject.SetActive(true);
		}
		else
		{
			m_SharePanel.gameObject.SetActive(false);
		}		
	}

	public void HideSharePanel ()
	{
		EnableSharePanel(false);
	}

	public void ShareToFacebook ()
	{
		float timeScore =  float.Parse(GameController.instance.playerTimeScore.ToString("F2"));

		if ( ThirdPartyController.Instance != null )
		{
			ThirdPartyController.MODE thisMode = ThirdPartyController.MODE.ENDLESS;

			if ( isTimeMode )
				thisMode = ThirdPartyController.MODE.TIME;
			else
				thisMode = ThirdPartyController.MODE.ENDLESS;

			ThirdPartyController.Instance.ShareScoreToFacebook(thisMode, timeScore);
		}
	}

	public void ShareToTwitter ()
	{
		float timeScore = float.Parse(GameController.instance.playerTimeScore.ToString("F2"));

		if ( ThirdPartyController.Instance != null )
		{
			ThirdPartyController.MODE thisMode = ThirdPartyController.MODE.ENDLESS;
			
			if ( isTimeMode )
				thisMode = ThirdPartyController.MODE.TIME;
			else
				thisMode = ThirdPartyController.MODE.ENDLESS;

			ThirdPartyController.Instance.ShareScoreToTwitter(thisMode, timeScore);
		}
	}
}
