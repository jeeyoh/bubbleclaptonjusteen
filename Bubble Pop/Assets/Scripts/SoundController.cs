using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public static SoundController instance {get; private set;}

	[SerializeField] private AudioClip BGM_MENU_CLIP; 
	[SerializeField] private AudioClip BGM_INGAME_CLIP; 

	[SerializeField] private AudioClip BUBBLE_POPPED_CLIP;
	[SerializeField] private AudioClip BLACK_BUBBLE_POPPED_CLIP;
	[SerializeField] private AudioClip NEW_BEST_TIME_CLIP;
	[SerializeField] private AudioClip POPS_ALL_BUBBLES_ON_TIME_CLIP;
	[SerializeField] private AudioClip GAMEOVER_CLIP;

	private AudioSource audioSource;
	private bool m_isMusicOn;
	private bool m_isSoundFXOn;

	void Awake() {
		if(instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		}
		
		instance = this;
		DontDestroyOnLoad(this.gameObject);

		audioSource = this.GetComponent<AudioSource>();
	}

//	void Start() {
//		Init();
//	}
//
//	private void Init() {
//		audioSource = this.GetComponent<AudioSource>();
//	}

	public void SetSounds(bool p_isMusicOn, bool p_isSoundFXOn) {
		m_isMusicOn = p_isMusicOn;
		m_isSoundFXOn = p_isSoundFXOn;
		if(p_isMusicOn) {
			if(!audioSource.isPlaying && audioSource.clip != null) {
				audioSource.Play();
				audioSource.loop = true;
				audioSource.volume = 1;
			}
		} else {
			audioSource.Pause();
			audioSource.volume = 0;
		}
	}

	public void PlayMenuBGM(float p_pitch) {
		if(!m_isMusicOn || (audioSource.isPlaying && audioSource.clip == BGM_MENU_CLIP)) return;
		audioSource.clip = BGM_MENU_CLIP;
		audioSource.loop = true;
		audioSource.pitch = p_pitch;
		audioSource.volume = 0.5f;
		audioSource.Play();
	}

	public void PlayInGameBGM(float p_pitch) {
		if(!m_isMusicOn) return;
		audioSource.clip = BGM_INGAME_CLIP;
		audioSource.loop = true;
		audioSource.pitch = p_pitch;
		audioSource.volume = 1;
		audioSource.Play();
	}

	public void PlayNewBestTimeBGM() {
//		if(!m_isSoundFXOn) return;
//		audioSource.PlayOneShot(NEW_BEST_TIME);
		if(!m_isMusicOn) return;
		audioSource.clip = NEW_BEST_TIME_CLIP;
		audioSource.loop = false;
		audioSource.pitch = 1f;
		audioSource.volume = 1;
		audioSource.Play();
	}
	
	public void PlaySuccessBGM() {
//		if(!m_isSoundFXOn) return;
//		audioSource.PlayOneShot(POPS_ALL_BUBBLES_ON_TIME);
		if(!m_isMusicOn) return;
		audioSource.clip = POPS_ALL_BUBBLES_ON_TIME_CLIP;
		audioSource.loop = false;
		audioSource.pitch = 1f;
		audioSource.volume = 1;
		audioSource.Play();
	}
	
	public void PlayGameOverBGM() {
//		if(!m_isSoundFXOn) return;
//		audioSource.PlayOneShot(GAMEOVER);
		if(!m_isMusicOn) return;
		audioSource.clip = GAMEOVER_CLIP;
		audioSource.loop = false;
		audioSource.pitch = 1f;
		audioSource.volume = 1;
		audioSource.Play();
	}

	public void PlaySFX() {
		if(!m_isSoundFXOn) return;

	}

	public void StopMusic() {
		if(audioSource.isPlaying) audioSource.Pause();
//		audioSource.volume = 0;
	}

	public void PlayBubblePoppedSFX() {
		if(!m_isSoundFXOn) return;
		audioSource.PlayOneShot(BUBBLE_POPPED_CLIP, 1f);
	}

	public void PlayBlackBubblePoppedSFX() {
		if(!m_isSoundFXOn) return;
//		audioSource.PlayOneShot(BLACK_BUBBLE_POPPED_CLIP);
		audioSource.Stop();
		audioSource.clip = BLACK_BUBBLE_POPPED_CLIP;
		audioSource.loop = false;
		audioSource.pitch = 1f;
		audioSource.volume = 0.9f;
		audioSource.Play();
	}

//	public void ToggleSoundFX(bool p_isOn) {
//		m_isSoundFXOn = p_isOn;
//	}
}
