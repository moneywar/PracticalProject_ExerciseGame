using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
  [SerializeField] private Slider _musicVolume;
  [SerializeField] private Slider _sfxVolume;
  private SoundPlayer _soundPlayer;
  private void Awake() => _soundPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundPlayer>();
  private void Update() => UpdateVolumeToAudioPlayer();
  public void UpdateVolumeToSlider()
  {
    if (_soundPlayer)
    {
      _musicVolume.value = _soundPlayer.GetMusicPlayer().volume;
      _sfxVolume.value = _soundPlayer.GetSFXPlayer().volume;
    }
  }

  public void UpdateVolumeToAudioPlayer()
  {
    if (_soundPlayer)
    {
      _soundPlayer.GetMusicPlayer().volume = _musicVolume.value;
      _soundPlayer.GetSFXPlayer().volume = _sfxVolume.value;
    }
  }
}
