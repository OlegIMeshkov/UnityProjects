using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public bool m_musicEnabled = true;
	public bool m_fxEnabled = true;

	[Range(0,1)]
	public float m_musicVolume = 1.0f;

	[Range(0,1)]
	public float m_fxVolume = 1.0f;

	public AudioClip m_clearRowSound;
	public AudioClip m_moveSound;
	public AudioClip m_dropSound;
	public AudioClip m_gameOverSound;
	public AudioClip m_backgroundMusic;
	public AudioSource m_musicSource;

	public AudioClip[] m_musicClips;
	private AudioClip m_randomMusicClip;



	// Use this for initialization
	void Start () {
		PlayBackgroundMusic (GetRandomClip (m_musicClips));
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateMusic ();
	}


	public void PlayBackgroundMusic (AudioClip musicClip)
	{
		if (!m_musicEnabled || !musicClip || !m_musicSource) //проверяем наличие ресурсов
		{
			return;
		}

		m_musicSource.Stop (); //останавливаем музыку, если она есть

		m_musicSource.clip = musicClip; 
		m_musicSource.volume = m_musicVolume;
		m_musicSource.loop = true;
		m_musicSource.Play ();
	}

	void UpdateMusic ()
	{
		if (m_musicSource.isPlaying != m_musicEnabled) 
		{
			if (m_musicEnabled) 
			{
				PlayBackgroundMusic (m_backgroundMusic);
			} else {
				m_musicSource.Stop ();
			}
		}
	}

	public void ToggleMusic ()
	{
		m_musicEnabled = !m_musicEnabled;
		UpdateMusic ();
	}

	public void ToogleFX()
	{
		m_fxEnabled = !m_fxEnabled;
	}


	public AudioClip GetRandomClip (AudioClip[] clips)
	{
		AudioClip randomClip = clips [Random.Range (0, clips.Length)];
		return randomClip;
	}
}
