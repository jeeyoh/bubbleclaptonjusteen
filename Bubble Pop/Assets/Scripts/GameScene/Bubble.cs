using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	void OnClick() {
		Debug.Log("Bubble POP!!!");
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Bubble POP!!!");
	}
}
