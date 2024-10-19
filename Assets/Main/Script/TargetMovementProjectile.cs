using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovementProjectile : MonoBehaviour
{
  public float heightOffset = 5f; // The height offset above the target
  private float duration = 1.5f; // Duration of the projectile's flight
  private Vector3 _targetPosition;
  private Vector3 startPoint;
  private Vector3 controlPoint;
  private Vector3 endPoint;
  private float elapsedTime = 0f;
  private bool _isSetup = false;

  private void Update()
  {
    if (elapsedTime < duration && _isSetup)
    {
      elapsedTime += Time.deltaTime;
      var t = elapsedTime / duration;

      // Get the position on the Bezier curve
      var position = BezierQuadratic(startPoint, controlPoint, endPoint, t);
      transform.position = position;

      // Optionally rotate the projectile to face the target
      transform.LookAt(_targetPosition);
    }
  }

  private void Setup()
  {
    startPoint = transform.position;
    endPoint = _targetPosition; // Set end point higher than the target
    controlPoint = ((startPoint + endPoint) / 2) + (Vector3.up * heightOffset); // Adjust control point to make it higher
    _isSetup = true;
  }

  public void Init(float speed, Vector3 targetPosition)
  {
    duration = speed;
    _targetPosition = targetPosition;
    Setup();
  }

  private Vector3 BezierQuadratic(Vector3 p0, Vector3 p1, Vector3 p2, float t)
  {
    var u = 1 - t;
    return (u * u * p0) + (2 * u * t * p1) + (t * t * p2);
  }
}
