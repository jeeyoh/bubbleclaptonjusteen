using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUDController : MonoBehaviour {

	public static GameHUDController instance;

	[SerializeField] private Text m_stopWatch;
	private float m_timeStopWatch;
	private bool m_isTimeRunning;

	void Awake() {
		instance = this;
	}

	void Start() {
		ResetTime();
	}

	public void StartGame() {
		StartTime();
	}

	public void StartTime() {
		m_isTimeRunning = true;
	}

	public void StopTime() {
		m_isTimeRunning = false;
	}

	public void ResetTime() {
		m_timeStopWatch = 0;
		m_stopWatch.text = "0s";
		m_isTimeRunning = false;
	}

	void Update() {
		if(m_isTimeRunning) {
			m_timeStopWatch += Time.deltaTime;
			m_stopWatch.text = m_timeStopWatch.ToString("F2") + "s";
		}
	}
}
