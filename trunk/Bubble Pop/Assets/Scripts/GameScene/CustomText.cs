using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomText : MonoBehaviour {

	[SerializeField] private Text m_mainText;
	[SerializeField] private Text m_shadow;
	[SerializeField] private Text m_outline;

	public void SetText(string p_text) {
		m_mainText.text = p_text;
		m_shadow.text = p_text;
		m_outline.text = p_text;
	}
}
