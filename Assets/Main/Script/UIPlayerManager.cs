using System.Collections;
using System.Collections.Generic;
using Mediapipe.Unity.Sample.PoseTracking;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIPlayerManager : MonoBehaviour
{
  [SerializeField] private TargetGenerate _targetGenerate;
  [SerializeField] private TextMeshProUGUI _scoreText;
  [SerializeField] private TextMeshProUGUI _hpText;
  [SerializeField] private PoseTrackingSolution _poseTrackingSolution;
  [SerializeField] private GameUIManager _gameUIManager;
  [SerializeField] private GameObject _popupText;
  [SerializeField] private Transform _scoreSpawnPoint;
  [SerializeField] private Transform _fameSpawnPoint;
  private int _scorePoint = 0;
  private int _healthPoint = 100;
  private bool _isGameOver = false;

  private void Update()
  {
    _scoreText.text = "Score : " + _scorePoint.ToString();
    _hpText.text = "Fame : " + _healthPoint.ToString();
    IsGameover();
  }
  public void AddScore()
  {
    _scorePoint++;
    var scorePopUp = Instantiate(_popupText, _scoreSpawnPoint.position, Quaternion.identity, gameObject.transform);
    var textSpawn = scorePopUp.GetComponentInChildren<TextSpawn>();

    var fameDownPopUp = Instantiate(_popupText, _fameSpawnPoint.position, Quaternion.identity, gameObject.transform);
    var textSpawnFame = fameDownPopUp.GetComponentInChildren<TextSpawn>();

    textSpawn.SetupText(1);
    textSpawnFame.SetupText(1);
  }
  public void LoseHealth(int hp)
  {
    _healthPoint -= hp;
    var scorePopUp = Instantiate(_popupText, _fameSpawnPoint.position, Quaternion.identity, gameObject.transform);
    var textSpawn = scorePopUp.GetComponentInChildren<TextSpawn>();
    textSpawn.SetupText(-hp);
    if (_healthPoint < 0)
    {
      _healthPoint = 0;
    }
  }
  public void AddHealth(int hp) => _healthPoint += hp;
  private void IsGameover()
  {
    if (_healthPoint <= 0 && !_isGameOver)
    {
      _isGameOver = true;
      // _targetGenerate.StopGen();
      // SceneManager.LoadScene("MainMenu");
      // _poseTrackingSolution.Stop();
      // Debug.Log("Game Over!!");
      _gameUIManager.GameOver(_scorePoint);
    }
  }
}
