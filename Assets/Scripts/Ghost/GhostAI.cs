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
        TRAPPER
    };

    private enGhostState currentState;
    public enGhostState initialState;

    public Ghost ghost { get; private set; }
    public bool isScared { get; private set; } = false;

    private void Start() 
    {
        ghost = GetComponent<Ghost>();
        currentState = initialState;
    }

    public void Reset() 
    {
        CancelInvoke();

        currentState = initialState;
        isScared = false;
    }

    public void EnableGhostScared(float duration)
    {
        currentState = enGhostState.FRIGHTNED;
        isScared = true;
        Invoke("DisableGhostScared", duration);
    }

    private void DisableGhostScared()
    {
        currentState = initialState;
        isScared = false;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        GhostPath path = collider.GetComponent<GhostPath>();
        
        if(!path) { return; }

        switch (currentState)
        {
            case enGhostState.SCATTER : ScatterLogic(path); break;
            case enGhostState.CHASE : ChaseLogic(path); break;
            case enGhostState.FRIGHTNED : FrightenedLogic(path); break;
            case enGhostState.TRAPPER : TrapperLogic(path); break;
            default: ScatterLogic(path); break;
        }
    }

    private void ScatterLogic(GhostPath path)
    {
        int nbPathsAvailables = path.availableDirections.Count;
        if(nbPathsAvailables <= 0) { return; }

        int index = 0;

        if(nbPathsAvailables > 1)
        {
            index = Random.Range(0, nbPathsAvailables);

            if(path.availableDirections[index] == -ghost.movements.direction)
            {
                index++;

                if(index >= nbPathsAvailables)
                {
                    index = 0;
                }
            }
        }

        ghost.movements.SetDirection(path.availableDirections[index]);
    }

    private void ChaseLogic(GhostPath path)
    {
        int nbPathsAvailables = path.availableDirections.Count;
        if(nbPathsAvailables <= 0) { return; }

        Vector2 direction = Vector2.zero;

        if(nbPathsAvailables > 1)
        {
            float minDistance = float.MaxValue;

            foreach (Vector2 pathDirection in path.availableDirections)
            {
                float distance = (ghost.target.transform.position - (transform.position + new Vector3(pathDirection.x, pathDirection.y))).sqrMagnitude;

                if(distance < minDistance)
                {
                    minDistance = distance;
                    direction = pathDirection;
                }
            }
        }

        ghost.movements.SetDirection(direction);
    }

    private void FrightenedLogic(GhostPath path)
    {
        int nbPathsAvailables = path.availableDirections.Count;
        if(nbPathsAvailables <= 0) { return; }

        Vector2 direction = Vector2.zero;

        if(nbPathsAvailables > 1)
        {
            float maxDistance = float.MinValue;

            foreach (Vector2 pathDirection in path.availableDirections)
            {
                float distance = (ghost.target.transform.position - (transform.position + new Vector3(pathDirection.x, pathDirection.y))).sqrMagnitude;

                if(distance > maxDistance)
                {
                    maxDistance = distance;
                    direction = pathDirection;
                }
            }
        }

        ghost.movements.SetDirection(direction);
    }

    private void TrapperLogic(GhostPath path)
    {
        int nbPathsAvailables = path.availableDirections.Count;
        if(nbPathsAvailables <= 0) { return; }

        Vector2 direction = Vector2.zero;
        Vector3 targetPos = ghost.target.transform.position + (new Vector3(ghost.target.movements.direction.x, ghost.target.movements.direction.x) * 4);

        if(nbPathsAvailables > 1)
        {
            float minDistance = float.MaxValue;

            foreach (Vector2 pathDirection in path.availableDirections)
            {
                float distance = (targetPos - (transform.position + new Vector3(pathDirection.x, pathDirection.y))).sqrMagnitude;

                if(distance < minDistance)
                {
                    minDistance = distance;
                    direction = pathDirection;
                }
            }
        }

        ghost.movements.SetDirection(direction);
    }
}
