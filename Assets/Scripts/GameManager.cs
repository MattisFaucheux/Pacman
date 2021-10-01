using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;


    public void CollectibleEaten(Collectible collectible)
    {
        score += collectible.points;
        collectible.gameObject.SetActive(false);
    }


}
