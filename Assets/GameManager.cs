using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  private int playerScore;
  public Text scoreText;
  private bool paused;
  public GameObject deathScreen;
  public GameObject pauseScreen;


  private void Start()
  {
    playerScore = 0;
    scoreText.GetComponent<Text>().text = playerScore.ToString();
    paused = false;
  }
  
  public void PlayerDeath()
  { 
    Time.timeScale = 0;
    deathScreen.SetActive(true);
  }
  
  public void PauseGame()
  {
    if (!paused)
    {
      Time.timeScale = 0;
      paused = true;
      pauseScreen.SetActive(true);
    }
    if (paused)
    {
      Time.timeScale = 1;
      paused = false;
      pauseScreen.SetActive(false);
    }
  }
  
  public void Score(int score)
  {
    playerScore += score;
    scoreText.GetComponent<Text>().text = playerScore.ToString();
  }
}
