using UnityEngine;
using System.Collections;

[System.Serializable]
public class StageBubble {
	public Sprite backgroundImg;
	public GameObject bubble;
}

[System.Serializable]
public class BubbleArea {
	public float left;
	public float right;
	public float top;
	public float bottom;
}

[System.Serializable]
public enum BubbleType {
	randomBubble,
	goodBubble,
	badBubble,
}

[System.Serializable]
public class TimeModeSettings {
	public int goodBubblesCount;
	public int badBubblesCount;
}

[System.Serializable]
public class EndlessModeSettings {
	public float addtionalSpeedPerIncrease;
	public float intervalBetweenSpeedIncrease;
}

public class BubbleCreator : MonoBehaviour {

	public static BubbleCreator instance;

	[SerializeField] private StageBubble[] m_stageBubbles;
	[SerializeField] private GameObject m_badBubble;
	[SerializeField] private float m_bubbleTimeInterval;
	[SerializeField] private int m_badBubbleChance; // int 0 - 100
	[SerializeField] private Transform m_goodBubblesHolder;
	[SerializeField] private Transform m_badBubblesHolder;

	public bool generateBubbles;

	public BubbleArea bubbleArea;
	public int goodBubblesCount;
	public int badBubblesCount;
	public TimeModeSettings timeModeSettings;
	public EndlessModeSettings endlessModeSettings;
	public bool isGameOver;

	private float m_timeOfNextBubble = 0f;
	private bool m_checkGoodBubblesCount;
	private GameModeType gameModeType;

	void Awake() {
		instance = this;
	}

	void Start() {
		Init ();
		timeModeSettings.goodBubblesCount = 3;
	}

	private void Init() {
		m_timeOfNextBubble = Time.timeSinceLevelLoad;
		m_checkGoodBubblesCount = false;
		gameModeType = GameController.instance.gameModeType;
	}

	void Update() {
		if(isGameOver) return;

		goodBubblesCount = m_goodBubblesHolder.childCount;
		badBubblesCount = m_badBubblesHolder.childCount;

		if(gameModeType == GameModeType.timeMode) {
			TimeModeUpdate();
		} else if(gameModeType == GameModeType.endlessMode) {
			EndlessModeUpdate();
		}
	}

	private void TimeModeUpdate() {
		if(m_checkGoodBubblesCount) {
			if(goodBubblesCount == 0) {
				isGameOver = true;
				GameController.instance.timeModeSuccess = true;
				GameController.instance.ChangeState(GameState.gameOver);
			}
		}
	}

	private void EndlessModeUpdate() {
		if(generateBubbles && m_timeOfNextBubble < Time.timeSinceLevelLoad) {
			
			m_timeOfNextBubble += m_bubbleTimeInterval;
			
			CreateBubble(BubbleType.randomBubble);
		}
	}

	public void StartTimeMode(int p_goodBubblesCount, int p_badBubblesCount) {
		CreateStartingBubbles(p_goodBubblesCount, BubbleType.goodBubble);
		CreateStartingBubbles(p_badBubblesCount, BubbleType.badBubble);
		generateBubbles = false;
	}

	public void StartEndlessMode() {

	}

	public void CreateStartingBubbles(int p_count, BubbleType p_bubbleType) {
		StartCoroutine(CreateBubbles(p_count, p_bubbleType));
	}

	private IEnumerator CreateBubbles(int p_count, BubbleType p_bubbleType) {
		yield return null;
		for(int i = 0; i < p_count; i++) {
			CreateBubble(p_bubbleType);
			yield return new WaitForSeconds(0.05f);
		}
		m_checkGoodBubblesCount = true;
	}

	public void CheckGoodBubblesCount() {
		m_checkGoodBubblesCount = true;
	}

	private void CreateBubble(BubbleType p_bubbleType) {

		int _randomGoodBuble = Random.Range(0, m_stageBubbles.Length);
		GameObject _bubbleType = m_stageBubbles[_randomGoodBuble].bubble;

		if(p_bubbleType == BubbleType.randomBubble) {
			bool _isBadBubble = (m_badBubbleChance >= Random.Range(0, 100)) ? true : false;
			if(_isBadBubble) {
				_bubbleType = m_badBubble;
			}
		} else if(p_bubbleType == BubbleType.badBubble) {
			_bubbleType = m_badBubble;
		}

		float _x = Random.Range(bubbleArea.left, bubbleArea.right);
		float _y = Random.Range(bubbleArea.top, bubbleArea.bottom);
		float _z = Random.Range(0f, 1f);
		Vector3 _pos = new Vector3(_x, _y, _z);
		float _zRot = _z * 360;
		Vector3 _rot = new Vector3(0f, 0f, _zRot);
		
		GameObject _bubble = _bubbleType.Spawn();
		if(_bubbleType == m_badBubble) _bubble.transform.parent = m_badBubblesHolder;
		else _bubble.transform.parent = m_goodBubblesHolder;
		_bubble.transform.localPosition = _pos;
		_bubble.transform.localEulerAngles = _rot;
	}
}
