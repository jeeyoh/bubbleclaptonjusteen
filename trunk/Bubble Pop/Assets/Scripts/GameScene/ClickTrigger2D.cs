using UnityEngine;
using System.Collections;

public class ClickTrigger2D : MonoBehaviour {

	private RaycastHit2D myHit;
//	private SoundHandler soundHandler;
//	private Pause pause;

	public bool isGameOver;

	void Start() {
		isGameOver = false;
		GameController.instance.OnGameOver += GameOver;
//		isPlaying = false;
//		soundHandler = this.GetComponent<SoundHandler>();
//		pause = GameObject.Find("Pause").GetComponent<Pause>();
	}

//	void OnEnable() {
//		GameController.instance.OnGameOver += GameOver;
//	}

	void OnDisable() {
		GameController.instance.OnGameOver -= GameOver;
	}

	private void GameOver() {
		isGameOver = true;
	}

	void Update () {
		if(isGameOver) return;

		myHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

//		float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
//		float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

		// check for other objects hit
		if(myHit.collider != null && Input.GetMouseButtonDown(0)) {
			if(myHit.collider.tag == "Bubble") {
				myHit.collider.GetComponent<Bubble>().Pop();
			} else if(myHit.collider.tag == "BadBubble") {
//				BubbleCreator.instance.isGameOver = true;
				myHit.collider.GetComponent<Bubble>().BadBubbleClicked();
				isGameOver = true;
				Invoke("SetGameOver", 1f);
			}
		}
	}

	private void SetGameOver() {
		GameController.instance.ChangeState(GameState.gameOver);
	}
}
