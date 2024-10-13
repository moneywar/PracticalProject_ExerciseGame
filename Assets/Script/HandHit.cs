using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHit : MonoBehaviour
{
  [SerializeField] private UIPlayerManager _playerUI;
  private void OnCollisionEnter(Collision other)
  {
    Debug.Log("HIT!!!!");
    if (gameObject.CompareTag("LeftHand") && other.gameObject.CompareTag("LeftTarget"))
    {
      Destroy(other.gameObject);
      if (_playerUI)
      {
        _playerUI.AddScore();
      }
    }
    else if (gameObject.CompareTag("RightHand") && other.gameObject.CompareTag("RightTarget"))
    {
      Destroy(other.gameObject);
      if (_playerUI)
      {
        _playerUI.AddScore();
      }
    }
    else
    {
      Destroy(other.gameObject);
    }
  }
}
