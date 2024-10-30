using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissZone : MonoBehaviour
{
  [SerializeField] private UIPlayerManager _uiPlayerManager;
  [SerializeField] private int _healthLossOnMiss = 0;
  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.CompareTag("LeftTarget") || other.gameObject.CompareTag("RightTarget"))
    {
      _uiPlayerManager.LoseHealth(_healthLossOnMiss);
      if (other.gameObject.TryGetComponent<TargetMovementProjectile>(out var targetMovement))
      {
        targetMovement.DestroyObject();
      }
    }

  }
}
