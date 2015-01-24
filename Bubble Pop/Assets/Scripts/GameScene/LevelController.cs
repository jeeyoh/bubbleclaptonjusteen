using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public static LevelController instance;

	void Awake() {
		instance = this;
	}

	void Start() {
		StartGame();
	}

	public void StartGame() {
		GameMode _gameMode = GameController.instance.gameMode;
		switch(_gameMode) {
		case GameMode.timeMode50:
			BubbleCreator.instance.CreateStartingBubbles(50);
			BubbleCreator.instance.generateBubbles = false;
			break;
		case GameMode.timeMode100:
			BubbleCreator.instance.CreateStartingBubbles(100);
			break;
		case GameMode.timeMode150:
			BubbleCreator.instance.CreateStartingBubbles(150);
			break;
		case GameMode.endlessMode5:
			break;
		case GameMode.endlessMode25:
			break;
		case GameMode.endlessMode50:
			break;
		}
	}
}
