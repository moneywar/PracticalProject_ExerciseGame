using System;
using System.Collections;
using System.Collections.Generic;
using Mediapipe.Unity.Sample.PoseTracking;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
  [SerializeField] private GameObject _pauseScreen;
  [SerializeField] private GameObject _gameOverScreen;
  [SerializeField] private PoseTrackingSolution _poseTrackingSolution;
  [SerializeField] private TargetGenerate _targetGenerate;
  [SerializeField] private TextMeshProUGUI _scoreText;
  private bool _isPause = false;
  private bool _isGameOver = false;
  private SoundPlayer _soundPlayer;
  private void Awake() => _soundPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundPlayer>();
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape) && !_isGameOver)
    {
      if (_isPause)
      {
        Resume();
      }
      else
      {
        Pause();
      }
    }
  }

  public void Resume()
  {
    _pauseScreen.SetActive(false);
    Time.timeScale = 1f;
    _isPause = false;
  }

  public void Pause()
  {
    _pauseScreen.SetActive(true);
    Time.timeScale = 0f;
    _isPause = true;
  }

  public void ReturnMainMenu()
  {
    Time.timeScale = 1f;
    _targetGenerate.StopGen();
    _poseTrackingSolution.Stop();
    SceneManager.LoadScene("MainMenu");
  }

  public void GameOver(int score)
  {
    Time.timeScale = 0f; // Stop the game time
    _scoreText.text = "Score : " + score.ToString();
    _isGameOver = true;
    _gameOverScreen.SetActive(true);
    if (_soundPlayer)
    {
      _soundPlayer.PlaySFX(_soundPlayer._gameoverSFX);
    }
  }

  public void RestartGame()
  {
    Time.timeScale = 1f; // Resume game time
    _targetGenerate.StopGen();
    _poseTrackingSolution.Stop();
    SceneManager.LoadScene("Jumping");
  }
}
