using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerate : MonoBehaviour
{
  [SerializeField] private GameObject _target;
  private float _spawnOffsetX;
  private float _spawnOffsetY;

  private readonly float _noteSpawnSpeed = 1f;  // Time interval (1 second)

  public void Start()
  {
    _spawnOffsetX = transform.localScale.x / 2f;
    _spawnOffsetY = transform.localScale.y / 2f;

    InvokeRepeating(nameof(StartGen), 1f, 3f); //!!Must Make this stop when game over!!
  }

  public void StartGen()
  {
    float randomX = Random.Range(transform.position.x - _spawnOffsetX, transform.position.x + _spawnOffsetX); // Random X position
    float randomY = Random.Range(transform.position.y - _spawnOffsetY, transform.position.y + _spawnOffsetY); // Random Y position

    var spawnPosition = new Vector3(randomX, randomY, transform.position.z); // Create the spawn position
    var spawnRotation = Quaternion.identity; // No rotation
    _ = Instantiate(_target, spawnPosition, spawnRotation);
  }
}
