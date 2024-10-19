using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerate : MonoBehaviour
{
  [SerializeField] private GameObject _targetLeft;
  [SerializeField] private GameObject _targetRight;
  private float _spawnOffsetX;
  private float _spawnOffsetY;

  [SerializeField] private float _noteSpawnSpeed = 1.5f;
  [SerializeField] private float _noteSpeed = 20f;

  public void Start()
  {
    _spawnOffsetX = transform.localScale.x / 2f;
    _spawnOffsetY = transform.localScale.y / 2f;

    InvokeRepeating(nameof(StartGen), 1f, _noteSpawnSpeed);
  }

  public void StartGen()
  {
    var randomX = Random.Range(transform.position.x - _spawnOffsetX, transform.position.x + _spawnOffsetX); // Random X position
    var randomY = Random.Range(transform.position.y - _spawnOffsetY, transform.position.y + _spawnOffsetY); // Random Y position

    var spawnPosition = new Vector3(randomX, randomY, transform.position.z); // Create the spawn position
    var spawnRotation = Quaternion.identity; // No rotation

    var target = randomX < transform.position.x
      ? Instantiate(_targetRight, spawnPosition, spawnRotation)
      : Instantiate(_targetLeft, spawnPosition, spawnRotation);
    target.GetComponent<MoveObject>().SetSpeed(_noteSpeed);
  }

  public void StopGen() => CancelInvoke(nameof(StartGen));
}
