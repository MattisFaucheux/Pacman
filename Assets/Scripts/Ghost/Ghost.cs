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

    private Vector3 initialPos;

    public float initialTimeToSpawn = 5.0f;
    public float timeToRespawn = 8.0f;



    private void Start()
    {
        movements = GetComponent<Movements>();
        ghostAI = GetComponent<GhostAI>();
        initialPos = transform.position;
    }

    public void Reset() 
    {
        ghostAI.Reset();
        movements.Reset();
        transform.position = initialPos;
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
            if(ghostAI.isScared)
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
