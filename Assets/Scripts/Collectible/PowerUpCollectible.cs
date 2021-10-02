using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollectible : Collectible
{
    protected override void Eaten()
    {
        FindObjectOfType<GameManager>().PowerUpCollectibleEaten(this);
    }
}
