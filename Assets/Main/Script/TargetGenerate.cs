using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerate : MonoBehaviour
{
  [SerializeField] private GameObject _targetLeft;
  [SerializeField] private GameObject _targetRight;
  [SerializeField] private Transform _targetZone;
  private float _spawnOffsetX;
  private float _spawnOffsetY;

  [SerializeField] private float _noteSpawnSpeed = 1.5f;
  [SerializeField] private float _noteSpeed = 20f;

  public void Start()
  {
    _spawnOffsetX = _targetZone.localScale.x / 2f;
    _spawnOffsetY = _targetZone.localScale.y / 2f;

    InvokeRepeating(nameof(StartGen), 1f, _noteSpawnSpeed);
  }

  public void StartGen()
  {
    var randomX = Random.Range(_targetZone.position.x - _spawnOffsetX, _targetZone.position.x + _spawnOffsetX); // Random X position
    var randomY = Random.Range(_targetZone.position.y - _spawnOffsetY, _targetZone.position.y + _spawnOffsetY); // Random Y position

    var targetPosition = new Vector3(randomX, randomY, _targetZone.position.z); // Create the spawn position
    var spawnRotation = Quaternion.identity; // No rotation

    var target = randomX < transform.position.x
      ? Instantiate(_targetRight, transform.position, spawnRotation)
      : Instantiate(_targetLeft, transform.position, spawnRotation);
    target.GetComponent<TargetMovementProjectile>().Init(_noteSpeed, targetPosition);
  }

  public void StopGen() => CancelInvoke(nameof(StartGen));
}
