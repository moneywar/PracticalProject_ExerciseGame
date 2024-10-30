using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHit : MonoBehaviour
{
  [SerializeField] private UIPlayerManager _playerUI;
  [SerializeField] private GameObject _hitParticle;
  private void OnCollisionEnter(Collision other)
  {
    TriggerParticle();
    if (gameObject.CompareTag("LeftHand") && other.gameObject.CompareTag("LeftTarget"))
    {
      Destroy(other.gameObject);
      if (_playerUI)
      {
        _playerUI.AddScore();
        _playerUI.AddHealth(1);
      }
    }
    else if (gameObject.CompareTag("RightHand") && other.gameObject.CompareTag("RightTarget"))
    {
      Destroy(other.gameObject);
      if (_playerUI)
      {
        _playerUI.AddScore();
        _playerUI.AddHealth(1);
      }
    }
    else
    {
      Destroy(other.gameObject);
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
