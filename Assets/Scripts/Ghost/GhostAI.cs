using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost))]
public class GhostAI : MonoBehaviour
{
    public enum enGhostState
    {
        SCATTER,
        CHASE,
        FRIGHTNED,
        HOME
    };

    private enGhostState currentState;
    public enGhostState initialState;

    public Ghost ghost { get; private set; }

    private void Start() 
    {
        ghost = GetComponent<Ghost>();
        currentState = initialState;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        GhostPath path = collider.GetComponent<GhostPath>();
        
        switch (currentState)
        {
            case enGhostState.SCATTER : ScatterLogic(path); break;
            case enGhostState.CHASE : ChaseLogic(path); break;
            case enGhostState.FRIGHTNED : FrightenedLogic(path); break;
            case enGhostState.HOME : HomeLogic(path); break;
            default: ScatterLogic(path); break;
        }
    }

    private void ScatterLogic(GhostPath path)
    {
        if(path)
        {
            int index = Random.Range(0, path.availableDirections.Count);

            if(path.availableDirections[index] == -ghost.movements.direction && path.availableDirections.Count > 1)
            {
                index++;

                if(index >= path.availableDirections.Count)
                {
                    index = 0;
                }
            }
            ghost.movements.SetDirection(path.availableDirections[index]);

        } 
    }

    private void ChaseLogic(GhostPath path)
    {
        return;
    }

    private void HomeLogic(GhostPath path)
    {
        return;
    }

    private void FrightenedLogic(GhostPath path)
    {
        return;
    }
}
