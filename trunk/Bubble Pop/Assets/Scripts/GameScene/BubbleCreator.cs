﻿using UnityEngine;
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
	public bool isGameOver;

	private float m_timeOfNextBubble = 0f;
	private bool m_checkGoodBubblesCount;

	void Awake() {
		instance = this;
	}

	void Start() {
		Init ();
	}

	private void Init() {
		m_timeOfNextBubble = Time.timeSinceLevelLoad;
		m_checkGoodBubblesCount = false;
	}

	void Update() {
		if(isGameOver) return;

		if(generateBubbles && m_timeOfNextBubble < Time.timeSinceLevelLoad) {

			m_timeOfNextBubble += m_bubbleTimeInterval;

			CreateBubble(BubbleType.randomBubble);
		}

		goodBubblesCount = m_goodBubblesHolder.childCount;
		badBubblesCount = m_badBubblesHolder.childCount;

		if(m_checkGoodBubblesCount) {
			if(goodBubblesCount == 0) {
				isGameOver = true;
				GameController.instance.timeModeSuccess = true;
				GameController.instance.ChangeState(GameState.gameOver);
			}
		}
	}

	public void CreateStartingBubbles(int p_count, BubbleType p_bubbleType) {
		for(int i = 0; i < p_count; i++) {
			CreateBubble(p_bubbleType);
		}
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