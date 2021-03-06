﻿using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	[SerializeField] private GameObject m_popAnimation;

	private SpriteRenderer m_spriteRenderer;
	private CircleCollider2D m_circleCollider2D;
	private TweenScale m_tweenScale;
		
	void Awake() {
		m_spriteRenderer = this.GetComponent<SpriteRenderer>();
		m_circleCollider2D = this.GetComponent<CircleCollider2D>();
		m_tweenScale = this.GetComponent<TweenScale>();
	}

	void OnEnable() {
		if(GameController.instance.gameState == GameState.gameOver) {
			Pop ();
			return;
		}

		Init();
		GameController.instance.OnGameOver += Pop;
	}

	void OnDisable() {
		m_popAnimation.SetActive(false);
		GameController.instance.OnGameOver -= Pop;
	}

	private void Init() {
		m_spriteRenderer.enabled = true;
		m_circleCollider2D.enabled = true;
		float _randomScale = Random.Range(1f, 1.8f);
		m_tweenScale.to = new Vector3(_randomScale, _randomScale, 1f);
		m_tweenScale.ResetToBeginning();
		m_tweenScale.PlayForward();
	}

	private void Deactivate() {
		this.gameObject.Recycle();
	}

	public void Pop() {
		if(this.tag != "BadBubble") {
			SoundController.instance.PlayBubblePoppedSFX();
		} else {
			if(!GameController.instance.timeModeSuccess) SoundController.instance.PlayBlackBubblePoppedSFX();
		}
		this.transform.parent = BubbleCreator.instance.GetPoppedBubblesHolder();
		m_spriteRenderer.enabled = false;
		m_circleCollider2D.enabled = false;
		m_popAnimation.SetActive(true);
		Invoke("Deactivate", 1f);
	}

	public void BadBubbleClicked() {
		if(this.tag != "BadBubble") return;

		m_tweenScale.from = m_tweenScale.to;
		m_tweenScale.to = new Vector3(2.3f, 2.3f, 1);
		m_tweenScale.ResetToBeginning();
		m_tweenScale.PlayForward();
		this.GetComponent<TweenColor>().PlayForward();
		Vector3 _localPos = this.transform.localPosition;
		_localPos.z = 0;
		this.transform.localPosition = _localPos;
		this.GetComponent<SpriteRenderer>().color = new Color(128, 128, 128, 128);
	}
}
