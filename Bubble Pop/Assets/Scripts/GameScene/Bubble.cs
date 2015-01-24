using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	private TweenScale m_tweenScale;
		
	void Awake() {
		m_tweenScale = this.GetComponent<TweenScale>();
	}

	void OnEnable() {
		Init();
	}

	private void Init() {
		float _randomScale = Random.Range(1f, 1.8f);
		m_tweenScale.to = new Vector3(_randomScale, _randomScale, 1f);
		m_tweenScale.ResetToBeginning();
		m_tweenScale.PlayForward();
	}

//	void OnClick() {
//		Debug.Log("Bubble POP!!!");
//	}
//
//	void OnTriggerEnter2D(Collider2D other) {
//		Debug.Log("Bubble POP!!!");
//	}
}
