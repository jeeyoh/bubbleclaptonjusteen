using UnityEngine;
using System.Collections;

public class ClickTrigger2D : MonoBehaviour {

	private RaycastHit2D myHit;
//	private SoundHandler soundHandler;
//	private Pause pause;

	void Start() {
//		isPlaying = false;
//		soundHandler = this.GetComponent<SoundHandler>();
//		pause = GameObject.Find("Pause").GetComponent<Pause>();
	}

	void Update () {
		myHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

//		float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
//		float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

		// check for other objects hit
		if(myHit.collider != null && Input.GetMouseButtonDown(0)) {
			Debug.Log(myHit.collider.name);
			if(myHit.collider.tag == "Bubble") {
				myHit.collider.Recycle();
			} else if(myHit.collider.tag == "BadBubble") {
				Debug.Log("Game Over!!!");
//				myHit.collider.Recycle();
			}
		}
	}
}
