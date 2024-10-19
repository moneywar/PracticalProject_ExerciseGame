using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
  [SerializeField] private float _moveSpeed = 5f;  // Speed of movement

  // Enum to define possible movement directions
  public enum Direction
  {
    Up,
    Down,
    Left,
    Right,
    Forward,
    Backward
  }

  // Direction to choose in the Inspector
  public Direction moveDirection;

  void Update()
  {
    // Move the object in the selected direction
    transform.Translate(GetDirectionVector(moveDirection) * _moveSpeed * Time.deltaTime);
  }

  public void SetSpeed(float speed) => _moveSpeed = speed;

  // Returns a Vector3 based on the chosen direction
  private Vector3 GetDirectionVector(Direction direction)
  {
    switch (direction)
    {
      case Direction.Up:
        return Vector3.up;
      case Direction.Down:
        return Vector3.down;
      case Direction.Left:
        return Vector3.left;
      case Direction.Right:
        return Vector3.right;
      case Direction.Forward:
        return Vector3.forward;
      case Direction.Backward:
        return Vector3.back;
      default:
        return Vector3.zero;
    }
  }
}
