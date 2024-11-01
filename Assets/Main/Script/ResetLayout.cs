using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetLayout : MonoBehaviour
{
  private RectTransform _layout;
  private void Awake() => _layout = gameObject.GetComponent<RectTransform>();
  private void Update() => LayoutRebuilder.ForceRebuildLayoutImmediate(_layout);
}
