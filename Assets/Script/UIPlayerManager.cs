using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerManager : MonoBehaviour
{
  [SerializeField] private TargetGenerate _targetGenerate;
  [SerializeField] private TextMeshProUGUI _scoreText;
  [SerializeField] private TextMeshProUGUI _hpText;
  private int _scorePoint = 0;
  private int _healthPoint = 100;

  private void Update() {
    _scoreText.text = "Score : " + _scorePoint.ToString();
    _hpText.text = "Health : " + _healthPoint.ToString();
    IsGameover();
  }
  public void AddScore() => _scorePoint++;
  public void LoseHealth(int hp)
  {
    _healthPoint -= hp;
    if (_healthPoint < 0)
    {
      _healthPoint = 0;
    }
  }
  public void AddHealth(int hp) => _healthPoint += hp;
  private void IsGameover()
  {
    if (_healthPoint <= 0)
    {
      _targetGenerate.StopGen();
      Debug.Log("Game Over!!");
    }
  }
}
