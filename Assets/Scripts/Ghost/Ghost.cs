using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movements))]
[RequireComponent(typeof(GhostAI))]
public class Ghost : MonoBehaviour
{
    [Min(0)] public int points = 100;
    public Pacman target;
    [Min(0f)] public float initialTimeToSpawn = 5.0f;
    [Min(0f)] public float timeToRespawn = 8.0f;

    public Movements movements { get; private set; }
    public GhostAI ghostAI { get; private set; }
    private Vector3 initialPos;

    private void Start()
    {
        movements = GetComponent<Movements>();
        ghostAI = GetComponent<GhostAI>();
        initialPos = transform.position;

        Unactivate();
        Invoke("Activate", initialTimeToSpawn);
    }

    public void Reset(float timeToActivate = 0.0f) 
    {
        CancelInvoke();
        
        Unactivate();
        ResetPosition();
        Invoke("Activate", timeToActivate);
    }

    private void ResetPosition()
    {
        ghostAI.Reset();
        movements.Reset();
        transform.position = initialPos;
    }

    private void Activate()
    {
        movements.enabled = true;
        ghostAI.enabled = true;
    }

    private void Unactivate()
    {
        movements.enabled = false;
        ghostAI.enabled = false;
    }

    private void Eaten()
    {
        FindObjectOfType<GameManager>().GhostEaten(this);
    }

    private void EatPacman()
    {
        FindObjectOfType<GameManager>().PacmanEaten();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
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
