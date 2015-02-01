using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUDController : MonoBehaviour {

	public static GameHUDController instance;

	[SerializeField] private GameObject m_timeModeImg;
	[SerializeField] private GameObject m_endlessModeImg;
	[SerializeField] private GameObject m_startBtn;
	[SerializeField] private CustomText m_stopWatch;

	private float m_timeStopWatch;
	private bool m_isTimeRunning;

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

		m_startBtn.SetActive(true);
		ResetTime();
	}

	public void StartGame() {
		m_startBtn.SetActive(false);
		GameController.instance.ChangeState(GameState.playing);
		LevelController.instance.StartGame();
		StartTime();
	}

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
		m_stopWatch.SetText("0s");
		m_isTimeRunning = false;
	}

	void Update() {
		if(m_isTimeRunning) {
			m_timeStopWatch += Time.deltaTime;
			m_stopWatch.SetText(m_timeStopWatch.ToString("F2") + "s");
		}
	}
}
