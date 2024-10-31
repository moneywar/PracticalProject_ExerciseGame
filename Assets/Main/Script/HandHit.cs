using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHit : MonoBehaviour
{
  [SerializeField] private UIPlayerManager _playerUI;
  [SerializeField] private GameObject _hitParticle;
  private SoundPlayer _soundPlayer;
  private void Awake() => _soundPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundPlayer>();
  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.CompareTag("LeftTarget") || other.gameObject.CompareTag("RightTarget"))
    {
      TriggerParticle();
      if (_soundPlayer)
      {
        _soundPlayer.PlaySFX(_soundPlayer._hitSFX);
      }

      // Check if the collision is with a matching target
      var isLeftMatch = gameObject.CompareTag("LeftHand") && other.gameObject.CompareTag("LeftTarget");
      var isRightMatch = gameObject.CompareTag("RightHand") && other.gameObject.CompareTag("RightTarget");

      if (isLeftMatch || isRightMatch)
      {
        // Destroy the target using TargetMovementProjectile if available
        if (other.gameObject.TryGetComponent<TargetMovementProjectile>(out var targetMovement))
        {
          targetMovement.DestroyObject();
        }

        // Update player UI
        if (_playerUI)
        {
          _playerUI.AddScore();
          _playerUI.AddHealth(1);
        }
      }
      else
      {
        // Destroy any non-matching object
        if (other.gameObject.TryGetComponent<TargetMovementProjectile>(out var targetMovement))
        {
          targetMovement.DestroyObject();
        }
      }
    }
  }


  private void TriggerParticle()
  {
    var allParticles = _hitParticle.GetComponentsInChildren<ParticleSystem>();
    foreach (var ps in allParticles)
    {
      ps.Play();
    }
  }
}
