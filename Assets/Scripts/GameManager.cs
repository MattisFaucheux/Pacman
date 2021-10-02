using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    [SerializeField]private Pacman pacman;
    [SerializeField]private Ghost[] ghosts;

    public void CollectibleEaten(Collectible collectible)
    {
        score += collectible.points;
        collectible.gameObject.SetActive(false);
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
    }

    public void GhostEaten(Ghost ghost)
    {
        score += ghost.points;
        ghost.gameObject.SetActive(false);
    }




}
