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
