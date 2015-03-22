using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public static LevelController instance {get; private set;}

	void Awake() {
		instance = this;

		if(!GameController.instance.AllowSound) {
			GetComponent<AudioSource>().enabled = false;
		}
	}

	void Start() {
		Init ();
	}

	public void Init() {
		Invoke("StartGame", 1f);
	}

	public void StartGame() {
		GameController.instance.ChangeState(GameState.playing);
		bool _isNoBlackBubbles = GameController.instance.IsNoBlackBubbles();
		Debug.Log("No Black Bubbles : " + _isNoBlackBubbles);
		int _blackBubblesCount = 0;

		GameMode _gameMode = GameController.instance.gameMode;
		switch(_gameMode) {
		case GameMode.timeMode50:
			if(!_isNoBlackBubbles) _blackBubblesCount = 5;
			BubbleCreator.instance.StartTimeMode(50, _blackBubblesCount);
			break;
		case GameMode.timeMode100:
			if(!_isNoBlackBubbles) _blackBubblesCount = 10;
			BubbleCreator.instance.StartTimeMode(100, _blackBubblesCount);
			break;
		case GameMode.timeMode150:
			if(!_isNoBlackBubbles) _blackBubblesCount = 15;
			BubbleCreator.instance.StartTimeMode(150, _blackBubblesCount);
			break;
		case GameMode.endlessMode5:
			if(!_isNoBlackBubbles) _blackBubblesCount = 2;
			EndlessModeSettings _endless5Settings = new EndlessModeSettings(1, 0.1f, 5, 5, _blackBubblesCount);
			BubbleCreator.instance.StartEndlessMode(_endless5Settings);
			break;
		case GameMode.endlessMode25:
			if(!_isNoBlackBubbles) _blackBubblesCount = 5;
			EndlessModeSettings _endless25Settings = new EndlessModeSettings(1, 0.1f, 5, 25, _blackBubblesCount);
			BubbleCreator.instance.StartEndlessMode(_endless25Settings);
			break;
		case GameMode.endlessMode50:
			if(!_isNoBlackBubbles) _blackBubblesCount = 10;
			EndlessModeSettings _endless50Settings = new EndlessModeSettings(1, 0.1f, 5, 50, _blackBubblesCount);
			BubbleCreator.instance.StartEndlessMode(_endless50Settings);
			break;
		}

		GameHUDController.instance.StartTime();
	}
}
