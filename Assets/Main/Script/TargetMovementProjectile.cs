using UnityEngine;

public class TargetMovementProjectile : MonoBehaviour
{
  public float heightOffset = 5f; // The height offset above the target
  private float speed = 10f; // Speed of the projectile

  private Vector3 _startPoint;
  private Vector3 _controlPoint;
  private bool _isSetup = false;

  private Vector3 _endPoint;
  private float _totalDistance;
  private float _traveledDistance = 0f;
  private Vector3 _targetPosition;

  public void Init(float speed, Vector3 targetPosition)
  {
    this.speed = speed;
    _targetPosition = targetPosition;
    Setup();
  }

  private void Setup()
  {
    _startPoint = transform.position;
    _endPoint = _targetPosition;
    _controlPoint = ((_startPoint + _endPoint) / 2) + (Vector3.up * heightOffset);
    _totalDistance = ApproximateCurveLength(_startPoint, _controlPoint, _endPoint);
    _isSetup = true;
  }

  private void Update()
  {
    if (_isSetup)
    {
      // Move the projectile along the curve at a constant speed
      _traveledDistance += speed * Time.deltaTime;
      var t = _traveledDistance / _totalDistance;

      if (t <= 1f)
      {
        // Move along the Bezier curve
        var position = BezierQuadratic(_startPoint, _controlPoint, _endPoint, t);
        transform.position = position;

        // Rotate to face the direction of movement along the curve
        var tangent = BezierQuadraticDerivative(_startPoint, _controlPoint, _endPoint, t);
        transform.rotation = Quaternion.LookRotation(tangent);
      }
      else
      {
        // After reaching the target, continue in the last direction of the curve
        var tangent = BezierQuadraticDerivative(_startPoint, _controlPoint, _endPoint, 1f).normalized;
        transform.position += speed * Time.deltaTime * tangent;

        // Optional: Keep rotation to face direction of movement
        transform.rotation = Quaternion.LookRotation(tangent);
      }
    }
  }

  private Vector3 BezierQuadratic(Vector3 p0, Vector3 p1, Vector3 p2, float t)
  {
    var u = 1 - t;
    return (u * u * p0) + (2 * u * t * p1) + (t * t * p2);
  }

  private Vector3 BezierQuadraticDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t) => (2 * (1 - t) * (p1 - p0)) + (2 * t * (p2 - p1));

  private float ApproximateCurveLength(Vector3 p0, Vector3 p1, Vector3 p2, int segments = 20)
  {
    var length = 0f;
    var previousPoint = p0;
    for (var i = 1; i <= segments; i++)
    {
      var t = (float)i / segments;
      var currentPoint = BezierQuadratic(p0, p1, p2, t);
      length += Vector3.Distance(previousPoint, currentPoint);
      previousPoint = currentPoint;
    }
    return length;
  }
}
