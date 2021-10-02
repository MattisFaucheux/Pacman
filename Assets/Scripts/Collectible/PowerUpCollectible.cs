using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollectible : Collectible
{
    public float powerUpDuration = 8.0f;
    public float speedMultiplier = 1.5f;

    protected override void Eaten()
    {
        FindObjectOfType<GameManager>().PowerUpCollectibleEaten(this);
    }
}
