using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private int lives = 3;
    private int highScore;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text livesText;

    [SerializeField]private Pacman pacman;
    [SerializeField]private Ghost[] ghosts;
    [SerializeField]private Transform collectibles;

    public float waitingTimeRestartGame = 5.0f;


    private void Start()
    {
        scoreText.text = "Score : \n" + score;
        livesText.text = "Lives : \n" + lives;
        if(PlayerPrefs.HasKey("Player High Score"))
        {
            SetHighScore(PlayerPrefs.GetInt("Player High Score"));
        }
        else
        {
            SetHighScore(0);
        }

    }

    private void OnDisable() 
    {
        PlayerPrefs.SetInt("Player High Score", highScore);
    }

    public void CollectibleEaten(Collectible collectible)
    {
        SetScore(score + collectible.points);
        collectible.gameObject.SetActive(false);

        if(IsLevelFinished())
        {
            RestartLevel();
        }
    }

    public void PowerUpCollectibleEaten(PowerUpCollectible collectible)
    {
        CollectibleEaten(collectible);

        foreach (Ghost ghost in ghosts)
        {
            ghost.ghostAI.EnableGhostScared(collectible.powerUpDuration);
        }

        pacman.EnablePowerUp(collectible.powerUpDuration, collectible.speedMultiplier);
    }

    public void PacmanEaten()
    {
        pacman.gameObject.SetActive(false);
        SetLives(lives - 1);

        if( lives <= 0)
        {
            GameOver();
        }
        else
        {
            ResetLevel();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(score + ghost.points);
        ghost.Reset(ghost.timeToRespawn);
    }

    private bool IsLevelFinished()
    {
       foreach (Transform collectible in collectibles)
       {
           if(collectible.gameObject.activeSelf)
           {
               return false;
           }
       }

       return true;
    }

    private void GameOver()
    {
        pacman.gameObject.SetActive(false);

        foreach (Ghost ghost in ghosts)
        {
           ghost.gameObject.SetActive(false);
        }

        foreach (Transform collectible in collectibles)
        {
           collectible.gameObject.SetActive(false);
        }

        if(score > highScore)
        {
            SetHighScore(score);
        }

        Invoke("StartGame", waitingTimeRestartGame);
    }

    private void StartGame()
    {
        SetLives(3);
        SetScore(0);
        RestartLevel();
    }

    private void RestartLevel()
    {
        ResetLevel();
        foreach (Transform collectible in collectibles)
        {
           collectible.gameObject.SetActive(true);
        }
    }

    private void ResetLevel()
    {
        CancelInvoke();

        pacman.gameObject.SetActive(true);
        pacman.Reset();
        foreach (Ghost ghost in ghosts)
        {
           ghost.gameObject.SetActive(true);
           ghost.Reset(ghost.initialTimeToSpawn);
        }
    }

    private void SetLives(int nbLives)
    {
        lives = nbLives;
        livesText.text = "Lives : \n" + lives;
    }

    private void SetScore(int newScore)
    {
        score = newScore;
        scoreText.text = "Score : \n" + score;
    }

    private void SetHighScore(int newHighScore)
    {
        highScore = newHighScore;
        highScoreText.text = "High Score : \n" + highScore;
    }

}
