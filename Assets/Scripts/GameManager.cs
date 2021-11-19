using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public Text scoreText;
  private Text _scoreText;
  
  [SerializeField] private GameObject _inGameUi;
  [SerializeField] private GameObject _startScreen;
  [SerializeField] private GameObject _deathScreen;
  [SerializeField] private GameObject _pauseScreen;
  [SerializeField] private GameObject playerPrefab;
  
  public List<GameObject> asteroidsInSceneList;
  private GameObject _player;
  private CameraFollow _cameraFollow;
  
  private int _playerScore;
  private bool isPaused;
  private void Awake()
  {
    Camera cam = Camera.main;
    _cameraFollow = cam.GetComponent<CameraFollow>();
    _scoreText = scoreText.GetComponent<Text>();
  }
  
  private void Start()
  {
    asteroidsInSceneList = new List<GameObject>();
    
    Time.timeScale = 0;
    _player = Instantiate(playerPrefab);
    _cameraFollow.SetCameraTarget(_player);
    _startScreen.SetActive(true);
  }

  public void GameStart()
  {
    _playerScore = 0;
    Score(_playerScore);
    
    _startScreen.SetActive(false);
    _inGameUi.SetActive(true);
    
    Time.timeScale = 1;
  }
  
  private void GameReset()
  {
    _player = Instantiate(playerPrefab);
    _cameraFollow.SetCameraTarget(_player);
    _inGameUi.SetActive(true);
    
    ClearAsteroids();
    asteroidsInSceneList = new List<GameObject>();
    
    _playerScore = 0;
    Score(_playerScore);
    Time.timeScale = 1;
  }
  
  public void Score(int score)
  {
    _playerScore += score;
    _scoreText.text = _playerScore.ToString();
  }

  private void ClearAsteroids()
  {
    foreach (var obj in asteroidsInSceneList)
      Destroy(obj);
  }
  
  public void PlayerDeath()
  { 
    Time.timeScale = 0;
    _deathScreen.SetActive(true);
    _inGameUi.SetActive(false);
  }
  
  public void PauseGame()
  {
    if (!isPaused)  
    { 
      Time.timeScale = 0;
      _pauseScreen.SetActive(true);
      isPaused = true;
      return;
    }
    Unpause();
  }

  private void Unpause()
  {
    Time.timeScale = 1;
    _pauseScreen.SetActive(false);
    isPaused = false;
  }
  
  public void RestartGame()
  {
    _deathScreen.SetActive(false);
    GameReset();
  }
  
  public void ExitGame() 
  {
    Debug.Log("Exit game");
    Application.Quit();
  }
}
