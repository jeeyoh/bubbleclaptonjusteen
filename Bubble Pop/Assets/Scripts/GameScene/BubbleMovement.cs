using UnityEngine;
using System.Collections;

public class BubbleMovement : MonoBehaviour {

	public Vector3 targetLocalPosition;
	public float speed;

	public void OnEnable() {
		StartCoroutine("ChangeTargetPosition");
	}

	void OnDisable() {
		StopCoroutine("ChangeTargetPosition");
	}

	private IEnumerator ChangeTargetPosition() {
		yield return null;
		SetTargetPosition();
		float _distance = Vector3.Distance (this.transform.localPosition, targetLocalPosition);
		float _time = _distance / speed;
		yield return new WaitForSeconds(_time);
		StartCoroutine("ChangeTargetPosition");
	}

	private void SetTargetPosition() {
		BubbleArea _bubbleArea = BubbleCreator.instance.bubbleArea;
		float _x = Random.Range(_bubbleArea.left, _bubbleArea.right);
		float _y = Random.Range(_bubbleArea.top, _bubbleArea.bottom);
//		float _z = Random.Range(0f, 1f);
		float _z = this.transform.position.z;
		targetLocalPosition = new Vector3(_x, _y, _z);
	}

	void Update() {
		this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, targetLocalPosition, speed * Time.deltaTime);
	}
}
