// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using UnityEngine;
public class ModelRigging : MonoBehaviour
{
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

  public void MapModel(Quaternion q1113, Quaternion q1315, Quaternion q1214, Quaternion q1416)
  {
    // transform.LookAt(new Vector3(0, 0, 0));  
    _leftArm.rotation = q1113;
    _leftArm.transform.Rotate(90, 0, 0);
    _leftFore.rotation = q1315;
    _leftFore.transform.Rotate(90, 0, 0);

    _rightArm.rotation = q1214;
    _rightArm.Rotate(90, 0, 0);
    _rightFore.rotation = q1416;
    _rightFore.Rotate(90, 0, 0);
    // _leftLeg.Rotate(0, 90, 0);
    // _leftArm.transform.localRotation = a;
    // _leftFore.transform.localRotation = b;
  }
}
