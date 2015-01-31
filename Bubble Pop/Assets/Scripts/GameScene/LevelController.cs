using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public static LevelController instance;

	void Awake() {
		instance = this;
	}

	public void StartGame() {
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
			BubbleCreator.instance.generateBubbles = true;
			break;
		case GameMode.endlessMode25:
			BubbleCreator.instance.generateBubbles = true;
			break;
		case GameMode.endlessMode50:
			BubbleCreator.instance.generateBubbles = true;
			break;
		}
	}
}
