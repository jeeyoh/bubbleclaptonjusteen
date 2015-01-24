using UnityEngine;
using System.Collections;

[System.Serializable]
public class StageBubble {
	public Sprite backgroundImg;
	public GameObject bubble;
}

[System.Serializable]
public class BubbleArea {
	public float left;
	public float right;
	public float top;
	public float bottom;
}

public class BubbleCreator : MonoBehaviour {

	public static BubbleCreator instance;

	[SerializeField] private StageBubble[] m_stageBubbles;
	[SerializeField] private GameObject m_badBubble;
	[SerializeField] private BubbleArea m_bubbleArea;
	[SerializeField] private float m_bubbleTimeInterval;
	[SerializeField] private int m_badBubbleChance; // int 0 - 100

	private float m_timeOfNextBubble = 0f;

	void Awake() {
		instance = this;
	}

	void Start() {
		Init ();
	}

	private void Init() {
		m_timeOfNextBubble = m_bubbleTimeInterval + Time.timeSinceLevelLoad;
	}

	void Update() {
		if(m_timeOfNextBubble < Time.timeSinceLevelLoad) {

			m_timeOfNextBubble += m_bubbleTimeInterval;

			int _randomBuble = Random.Range(0, m_stageBubbles.Length);
			GameObject _bubbleType = m_stageBubbles[_randomBuble].bubble;

			bool _isBadBubble = (m_badBubbleChance >= Random.Range(0, 100)) ? true : false;
			if(_isBadBubble) _bubbleType = m_badBubble;

			float _x = Random.Range(m_bubbleArea.left, m_bubbleArea.right);
			float _y = Random.Range(m_bubbleArea.top, m_bubbleArea.bottom);
			float _z = Random.Range(0f, 1f);
			Vector3 _pos = new Vector3(_x, _y, _z);

			GameObject _bubble = _bubbleType.Spawn(); // (GameObject)Instantiate(_bubbleType, Vector3.zero, Quaternion.identity);
			_bubble.transform.parent = this.transform;
			_bubble.transform.localPosition = _pos;
		}
	}
}
