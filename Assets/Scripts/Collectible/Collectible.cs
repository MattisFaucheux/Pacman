using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Min(0)]public int points = 10;

    protected virtual void Eaten()
    {
        FindObjectOfType<GameManager>().CollectibleEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Eaten();
        }
    }
}
