using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollectible : Collectible
{
    [Min(0)]public float powerUpDuration = 8.0f;
    [Min(0)]public float speedMultiplier = 1.5f;

    protected override void Eaten()
    {
        FindObjectOfType<GameManager>().PowerUpCollectibleEaten(this);
    }
}
