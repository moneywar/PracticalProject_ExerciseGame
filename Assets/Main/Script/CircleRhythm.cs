using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRhythm : MonoBehaviour
{
  [SerializeField] private GameObject _fullCircle;
  public void UpdateSize(float t)
  {
    var scale = t * 100;
    _fullCircle.transform.localScale = new Vector3(scale, scale, scale);
    Debug.Log(scale);
  }
}
