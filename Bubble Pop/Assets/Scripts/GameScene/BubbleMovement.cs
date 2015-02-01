using UnityEngine;
using System.Collections;

public class BubbleMovement : MonoBehaviour {

	public Vector3 targetLocalPosition;
	public float speed;

	[SerializeField] private bool m_customMovement;
	[SerializeField] private float m_borderTop;
	[SerializeField] private float m_borderBottom;
	[SerializeField] private float m_borderLeft;
	[SerializeField] private float m_borderRight;

	private Vector3 m_defaultLocalPosition;

	void Awake() {
		m_defaultLocalPosition = this.transform.localPosition;
	}

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
		float _x = 0;
		float _y = 0;
		float _z = 0;
		if(m_customMovement) {
			_x = (m_defaultLocalPosition.x - m_borderLeft) + Random.Range(m_borderLeft, m_borderRight);
			_y = (m_defaultLocalPosition.y + m_borderTop) + Random.Range(m_borderTop, m_borderBottom);
			_z = 1;
		} else {
			BubbleArea _bubbleArea = BubbleCreator.instance.bubbleArea;
			_x = Random.Range(_bubbleArea.left, _bubbleArea.right);
			_y = Random.Range(_bubbleArea.top, _bubbleArea.bottom);
			_z = this.transform.position.z;
		}
		targetLocalPosition = new Vector3(_x, _y, _z);
	}

	void Update() {
		this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, targetLocalPosition, speed * Time.deltaTime);
	}
}
