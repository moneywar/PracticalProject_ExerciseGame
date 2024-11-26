using System.Collections;
using System.Collections.Generic;
using Mediapipe.Unity.Sample.PoseTracking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerManager : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _scoreText;
  [SerializeField] private TextMeshProUGUI _hpText;
  [SerializeField] private PoseTrackingSolution _poseTrackingSolution;
  [SerializeField] private GameUIManager _gameUIManager;
  [SerializeField] private GameObject _popupText;
  [SerializeField] private Transform _scoreSpawnPoint;
  [SerializeField] private Transform _fameSpawnPoint;
  [SerializeField] private HealthBar _healthBar;
  [SerializeField] private TextMeshProUGUI _healthText;
  [SerializeField] private Slider _tmpHealthSlider;
  [SerializeField] private TextMeshProUGUI _comboText;
  [SerializeField] private TargetGenerate _targetGenerate;
  [SerializeField] private Animator _speedUpTextAnimator;
  [SerializeField] private int _speedUpScore = 100;
  private int _curScoreBeforeChangeSpeed = 0;
  private int _combo = 0;
  private int _scorePoint = 0;

  [SerializeField] private int _maxHealthPoint = 5;
  private int _healthPoint;

  [SerializeField] private int _maxTmpHealthPoint = 10;
  private int _curTmpHealthPoint = 0;

  private bool _isGameOver = false;
  [SerializeField] private int _scoreAddPerHit = 1;
  [SerializeField] private int _healthAddPerHit = 1;
  [SerializeField] private int _healthLosePerHit = 1;
  [SerializeField] private int _minimumComboAddScore = 5;
  private void Start()
  {
    _healthPoint = _maxHealthPoint;
    _healthBar.SetMaxHealth(_maxHealthPoint);
  }
  private void Update()
  {
    _scoreText.text = "Score : " + _scorePoint.ToString();
    _healthBar.SetHealth(_healthPoint);
    SetHelthText();
    SetTmpHealthPoint();
    SetCombo();
    CheckScoreToChangeSpeed();
    // _hpText.text = "Fame : " + _healthPoint.ToString();
    IsGameover();
  }

  public void AddScore(int score)
  {
    var curScoreMaking = score * ((_combo / _minimumComboAddScore) + 1);
    _curScoreBeforeChangeSpeed += curScoreMaking;
    _scorePoint += curScoreMaking;
    var scorePopUp = Instantiate(_popupText, _scoreSpawnPoint.position, Quaternion.identity, gameObject.transform);
    var textSpawn = scorePopUp.GetComponentInChildren<TextSpawn>();

    var fameDownPopUp = Instantiate(_popupText, _fameSpawnPoint.position, Quaternion.identity, gameObject.transform);
    var textSpawnFame = fameDownPopUp.GetComponentInChildren<TextSpawn>();

    textSpawn.SetupText(curScoreMaking);
    textSpawnFame.SetupText(_healthAddPerHit);
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
  public void AddHealth(int hp)
  {
    _curTmpHealthPoint += hp;
    Debug.Log(_curTmpHealthPoint);
    if (_curTmpHealthPoint >= _maxTmpHealthPoint && _healthPoint != _maxHealthPoint)
    {
      _healthPoint++;
      _curTmpHealthPoint = 0;
    }
    else if (_healthPoint == _maxHealthPoint)
    {
      _curTmpHealthPoint = 0;
    }
  }
  public void IsHandHit()
  {
    _combo += 1;
    AddHealth(_healthAddPerHit);
    AddScore(_scoreAddPerHit);
  }

  public void IsMiss()
  {
    LoseHealth(_healthLosePerHit);
    _combo = 0;
  }

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

  private void CheckScoreToChangeSpeed()
  {
    if (_targetGenerate)
    {
      if (_curScoreBeforeChangeSpeed >= _speedUpScore)
      {
        _targetGenerate.SpeedUp();
        _curScoreBeforeChangeSpeed -= _speedUpScore;
        if (_speedUpTextAnimator) _speedUpTextAnimator.SetTrigger("MoveDown");
      }
    }
  }

  private void SetHelthText()
  {
    if (_healthText)
    {
      _healthText.text = _healthPoint.ToString() + " / " + _maxHealthPoint.ToString();
    }
  }

  private void SetTmpHealthPoint()
  {
    if (_tmpHealthSlider)
    {
      var scale = (float)_curTmpHealthPoint / _maxTmpHealthPoint;
      _tmpHealthSlider.value = scale;
    }
  }

  private void SetCombo()
  {
    if (_comboText)
    {
      _comboText.text = "Combo : " + _combo.ToString();
    }
  }
}
