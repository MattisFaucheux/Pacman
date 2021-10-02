using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private int lives = 3;

    [SerializeField]private Pacman pacman;
    [SerializeField]private Ghost[] ghosts;
    [SerializeField]private Transform collectibles;

    public void CollectibleEaten(Collectible collectible)
    {
        score += collectible.points;
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
        lives -= 1;

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
        score += ghost.points;
        ghost.gameObject.SetActive(false);
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
    }

    private void StartGame()
    {
        lives = 3;
        score = 0;
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
        pacman.gameObject.SetActive(true);
        pacman.Reset();
        foreach (Ghost ghost in ghosts)
        {
           ghost.gameObject.SetActive(true);
           ghost.Reset();
        }
    }




}
