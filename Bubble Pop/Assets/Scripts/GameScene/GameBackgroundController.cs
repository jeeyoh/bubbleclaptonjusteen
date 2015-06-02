using UnityEngine;
using System.Collections;

public class GameBackgroundController : MonoBehaviour {

	public static GameBackgroundController instance {get; private set;}

	[SerializeField] private SpriteRenderer m_backgroundImageHolder; 
	[SerializeField] private Sprite[] m_backgroundImages; 

	void Awake() {
		instance = this;
	}

	void Start() {
		SetRandomBackground();
	}

	void OnDestroy (){
		GameObject.Destroy (instance);
	}

	public void SetRandomBackground() {
		int _randomImg = Random.Range(0, m_backgroundImages.Length);
		m_backgroundImageHolder.sprite = m_backgroundImages[_randomImg];
	}
}
