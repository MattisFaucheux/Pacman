using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPath : MonoBehaviour
{
    public List<Vector2> availableDirections;
    public LayerMask wallLayer;
    
    private void Start()
    {
        this.availableDirections = new List<Vector2>();
        CheckAvailableDirection(Vector2.right);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position , Vector2.one * 0.75f, 0.0f, direction, 1.5f, wallLayer);

        if(hit.collider == null)
        {
            availableDirections.Add(direction);
        }
    }

   
}
