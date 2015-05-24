using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RewardMeter : MonoBehaviour {

	[SerializeField] private Slider m_rewardMeter;

	public void SetMeter(int p_initialValue, int p_finalValue) {
		m_rewardMeter.value = p_initialValue;
		StartCoroutine ("IncreaseMeter", p_finalValue);
	}

	private IEnumerator IncreaseMeter(int p_finalValue) {
		while (m_rewardMeter.value < p_finalValue) {
			m_rewardMeter.value += Time.deltaTime * 2f;
			yield return null;
		}
	}
}
