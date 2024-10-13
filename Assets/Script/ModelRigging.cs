// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe;
using UnityEngine;
using UnityEngine.UIElements;
public class ModelRigging : MonoBehaviour
{
  [SerializeField] private Transform _hip;
  [SerializeField] private Transform _leftHip;
  [SerializeField] private Transform _rightHip;
  [SerializeField] private Transform _leftArm;
  [SerializeField] private Transform _rightArm;
  [SerializeField] private Transform _leftFore;
  [SerializeField] private Transform _rightFore;
  [SerializeField] private Transform _leftHand;
  [SerializeField] private Transform _rightHand;
  [SerializeField] private Transform _leftLeg;
  [SerializeField] private Transform _rightLeg;
  [SerializeField] private Transform _leftFoot;
  [SerializeField] private Transform _rightFoot;

  private Vector3 _hipOriginalPosition;
  public float jumpScale = 3f;
  public float moveDelay = 100f;
  public float moveScale = 2f;

  public void Start() => _hipOriginalPosition = _hip.position;
  public void MapModel(Quaternion q1113, Quaternion q1315, Quaternion q1214, Quaternion q1416, NormalizedLandmark Hip)
  {
    var hipPositionY = Hip.Y > 1 ? 1 : Hip.Y;
    var targetPositionBody = new Vector3((Hip.X - 0.5f) * moveScale, transform.position.y, transform.position.z);
    var targetPositionHip = new Vector3(transform.position.x, _hipOriginalPosition.y + ((1 - hipPositionY) * jumpScale), transform.position.z);

    // Smoothly interpolate the position over time
    transform.position = Vector3.Lerp(transform.position, targetPositionBody, Time.deltaTime * moveDelay); // Adjust speed factor as needed
    // _hip.position = Vector3.Lerp(_hip.position, targetPositionHip, Time.deltaTime * moveDelay);
    // Update rotations as before
    _rightArm.rotation = q1113;
    _rightArm.transform.Rotate(90, 0, 0);
    _rightFore.rotation = q1315;
    _rightFore.transform.Rotate(90, 0, 0);

    _leftArm.rotation = q1214;
    _leftArm.Rotate(90, 0, 0);
    _leftFore.rotation = q1416;
    _leftFore.Rotate(90, 0, 0);
  }

}
