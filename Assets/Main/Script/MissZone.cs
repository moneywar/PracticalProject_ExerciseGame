using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissZone : MonoBehaviour
{
  [SerializeField] private UIPlayerManager _uiPlayerManager;
  private SoundPlayer _soundPlayer;
  private void Awake() => _soundPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundPlayer>();
  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.CompareTag("LeftTarget") || other.gameObject.CompareTag("RightTarget"))
    {
      if (_soundPlayer)
      {
        _soundPlayer.PlaySFX(_soundPlayer._missSFX);
      }
      _uiPlayerManager.IsMiss();
      if (other.gameObject.TryGetComponent<TargetMovementProjectile>(out var targetMovement))
      {
        targetMovement.DestroyObject();
      }
    }

  }
}
