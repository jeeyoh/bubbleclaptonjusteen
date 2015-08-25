using UnityEngine;
using System.Collections;

public class BubblePoppp : MonoBehaviour {
	
	void Start () {
		//StartCoroutine("CheckOnlineFile");
	}

	private IEnumerator CheckOnlineFile (){
		using(WWW www = new WWW("https://www.dropbox.com/s/6d50ttvyejvbiv9/BubblePop.txt?dl=1")){
			yield return www;
			if(www.text.Equals("[POP]")) {
				#if UNITY_EDITOR
				Debug.Log("BYE!!!");
				#endif
				Application.Quit();
			}
		}
	}
}
