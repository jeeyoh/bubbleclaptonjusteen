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

		GameMode _gameMode = GameController.instance.gameMode;
		switch(_gameMode) {
		case GameMode.timeMode50:
			BubbleCreator.instance.StartTimeMode(50, 5);
			break;
		case GameMode.timeMode100:
			BubbleCreator.instance.StartTimeMode(100, 10);
			break;
		case GameMode.timeMode150:
			BubbleCreator.instance.StartTimeMode(150, 15);
			break;
		case GameMode.endlessMode5:
			EndlessModeSettings _endless5Settings = new EndlessModeSettings(1, 0.1f, 5, 5, 2);
			BubbleCreator.instance.StartEndlessMode(_endless5Settings);
			break;
		case GameMode.endlessMode25:
			EndlessModeSettings _endless25Settings = new EndlessModeSettings(1, 0.1f, 5, 25, 5);
			BubbleCreator.instance.StartEndlessMode(_endless25Settings);
			break;
		case GameMode.endlessMode50:
			EndlessModeSettings _endless50Settings = new EndlessModeSettings(1, 0.1f, 5, 50, 10);
			BubbleCreator.instance.StartEndlessMode(_endless50Settings);
			break;
		}

		GameHUDController.instance.StartTime();
	}
}
