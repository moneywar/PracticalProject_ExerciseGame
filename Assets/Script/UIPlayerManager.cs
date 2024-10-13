using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerManager : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _scoreText;
  [SerializeField] private TextMeshProUGUI _hpText;
  private int _scorePoint = 0;

  private void Update() => _scoreText.text = "Score : " + _scorePoint.ToString();
  public void AddScore() => _scorePoint++;
}
