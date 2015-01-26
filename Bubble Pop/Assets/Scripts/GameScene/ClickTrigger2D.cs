using UnityEngine;
using System.Collections;

public class ClickTrigger2D : MonoBehaviour {

	private RaycastHit2D myHit;
//	private SoundHandler soundHandler;
//	private Pause pause;

	public bool isGameOver;

	void Start() {
		isGameOver = false;
//		isPlaying = false;
//		soundHandler = this.GetComponent<SoundHandler>();
//		pause = GameObject.Find("Pause").GetComponent<Pause>();
	}

	void Update () {
		if(isGameOver) return;

		myHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

//		float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
//		float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

		// check for other objects hit
		if(myHit.collider != null && Input.GetMouseButtonDown(0)) {
			if(myHit.collider.tag == "Bubble") {
				myHit.collider.Recycle();
			} else if(myHit.collider.tag == "BadBubble") {
				isGameOver = true;
				BubbleCreator.instance.isGameOver = true;
				GameController.instance.ChangeState(GameState.gameOver);
			}
		}
	}
}
