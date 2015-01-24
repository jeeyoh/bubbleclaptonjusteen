using UnityEngine;
using System.Collections;

public class ClickTrigger2D : MonoBehaviour {
	
//	public GameObject hitMarker;
//	public bool isPlaying;

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

		float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

		// check for other objects hit
		if(myHit.collider != null && Input.GetMouseButtonDown(0)) {
			Debug.Log(myHit.collider.name);
			if(myHit.collider.tag == "Bubble") {
				Debug.Log("Bubble HIT!!! " + myHit.collider.name);
//				Destroy(myHit.collider.gameObject);
				myHit.collider.Recycle();
			}
//
//			if(myHit.collider.name == "Pause")
//			{
//				// play button clicked sound
//				soundHandler.playButtonClick();
//
//				isPlaying = false;
//				myHit.collider.SendMessage("PauseGame");
//			}
//
//			if(!isPlaying && pause.isPaused)
//			{
//				if(myHit.collider.name == "BTN_Menu")
//				{
//					soundHandler.playButtonClick();
//					Application.LoadLevel(0);
//				}
//
//				if(myHit.collider.name == "BTN_Games")
//				{
//					soundHandler.playButtonClick();
//				}
//
//				if(myHit.collider.name == "BTN_Ready")
//				{
//					soundHandler.playButtonClick();
//					isPlaying = true;
//					GameObject.Find("Pause").SendMessage("ResumeGame");
//				}
//			}
		}
	}
}
