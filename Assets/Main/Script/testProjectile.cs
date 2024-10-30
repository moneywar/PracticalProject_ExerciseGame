using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Transform target; // The target object
    public float heightOffset = 5f; // The height offset above the target
    public float duration = 2f; // Duration of the projectile's flight
    public float speedAfterTarget = 2f; // Speed after reaching the target

    private Vector3 startPoint;
    private Vector3 controlPoint;
    private Vector3 endPoint;
    private float elapsedTime = 0f;

    void Start()
    {
        startPoint = transform.position;
        endPoint = target.position; // Set end point higher than the target
        controlPoint = (startPoint + endPoint) / 2 + Vector3.up * heightOffset; // Adjust control point to make it higher
    }

    void Update()
    {
        // Determine how far along the path we are
        float t = elapsedTime / duration;

        if (t <= 1f)
        {
            // Move along the Bezier curve for the first part
            elapsedTime += Time.deltaTime;
            Vector3 position = BezierQuadratic(startPoint, controlPoint, endPoint, t);
            transform.position = position;

            // Optionally rotate the projectile to face the target
            // transform.LookAt(target);
        }
        else
        {
            // Continue moving along the curve after reaching the target
            // Calculate the new t value that continues along the curve
            elapsedTime += Time.deltaTime;
            float overshootT = t - 1; // t now is greater than 1
            Vector3 position = BezierQuadratic(startPoint, controlPoint, endPoint, 1 + overshootT * speedAfterTarget);
            Debug.Log(position);
            transform.position = position;

            // Continue to look at the target or adjust direction if necessary
            // transform.LookAt(target);
        }
    }

    Vector3 BezierQuadratic(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }
}