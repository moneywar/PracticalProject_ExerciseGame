using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
  [SerializeField] private AudioSource _musicPlayer;
  [SerializeField] private AudioSource _sfxPlayer;

  // public AudioClip _backgroundMusic;
  // public AudioClip _ingameMusic;
  public AudioClip _hitSFX;
  public AudioClip _missSFX;
  public AudioClip _gameoverSFX;
  private static SoundPlayer _instance;
  private void Awake()
  {
    if (_instance == null)
    {
      _instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject); // Destroy duplicate instance
    }
  }

  // private void Start()
  // {
  //   _musicPlayer.clip = _backgroundMusic;
  //   _musicPlayer.Play();
  // }

  public void PlayMusic(AudioClip clip)
  {
    _musicPlayer.clip = clip;
    _musicPlayer.Play();
  }
  public void PlaySFX(AudioClip clip)
  {
    _sfxPlayer.PlayOneShot(clip);
  }

  public AudioSource GetMusicPlayer() => _musicPlayer;
  public AudioSource GetSFXPlayer() => _sfxPlayer;
}
