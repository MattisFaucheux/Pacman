using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movements))]
[RequireComponent(typeof(GhostAI))]
public class Ghost : MonoBehaviour
{
    public int points = 100;

    public Movements movements { get; private set; }

    public GhostAI ghostAI { get; private set; }

    public Transform target;

    bool scared = false;

    private void Start()
    {
        movements = GetComponent<Movements>();
        ghostAI = GetComponent<GhostAI>();
    }


    private void Eaten()
    {
        FindObjectOfType<GameManager>().GhostEaten(this);
    }

    private void EatPacman()
    {
        FindObjectOfType<GameManager>().PacmanEaten();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(scared)
            {
                Eaten();
            }
            else
            {
                EatPacman();
            }
        }
    }
}
