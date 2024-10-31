using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGM : MonoBehaviour
{
  public AudioClip bgm;
  private SoundPlayer _soundPlayer;
  private void Awake() => _soundPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundPlayer>();
  void Start()
  {
    if (_soundPlayer)
    {
      _soundPlayer.PlayMusic(bgm);
    }
  }
}
