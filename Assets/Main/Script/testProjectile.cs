using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Transform target; // The target object
    public float heightOffset = 5f; // The height offset above the target
    public float speed = 10f; // Speed of the projectile
  public CircleRhythm circleRhythm = null;

  private Vector3 startPoint;
    private Vector3 controlPoint;
    private Vector3 endPoint;
    private float totalDistance;
    private float traveledDistance = 0f;

    void Start()
    {
        startPoint = transform.position;
        endPoint = target.position;
        controlPoint = (startPoint + endPoint) / 2 + Vector3.up * heightOffset;
        totalDistance = ApproximateCurveLength(startPoint, controlPoint, endPoint);
    }

    void Update()
    {
        // Move the projectile along the curve at a constant speed
        traveledDistance += speed * Time.deltaTime;
        var t = traveledDistance / totalDistance;
        if (t <= 1f)
        {
            // Move along the Bezier curve
            Vector3 position = BezierQuadratic(startPoint, controlPoint, endPoint, t);
            transform.position = position;

            // Rotate to face the direction of movement along the curve
            Vector3 tangent = BezierQuadraticDerivative(startPoint, controlPoint, endPoint, t);
            transform.rotation = Quaternion.LookRotation(tangent);
            if (circleRhythm != null)
            {
                circleRhythm.UpdateSize(t);
            }
        }
        else
        {
            if (circleRhythm != null)
            {
                Destroy(circleRhythm.gameObject);
                circleRhythm = null; // Set to null to prevent further access
            }
            // After reaching the target, continue in the last direction of the curve
            Vector3 tangent = BezierQuadraticDerivative(startPoint, controlPoint, endPoint, 1f).normalized;
            transform.position += tangent * speed * Time.deltaTime;

            // Optional: Keep rotation to face direction of movement
            transform.rotation = Quaternion.LookRotation(tangent);
        }
    }

    Vector3 BezierQuadratic(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    Vector3 BezierQuadraticDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return 2 * (1 - t) * (p1 - p0) + 2 * t * (p2 - p1);
    }

    float ApproximateCurveLength(Vector3 p0, Vector3 p1, Vector3 p2, int segments = 20)
    {
        float length = 0f;
        Vector3 previousPoint = p0;
        for (int i = 1; i <= segments; i++)
        {
            float t = (float)i / segments;
            Vector3 currentPoint = BezierQuadratic(p0, p1, p2, t);
            length += Vector3.Distance(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
        return length;
    }
}
