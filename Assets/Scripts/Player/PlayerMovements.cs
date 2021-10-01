using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Vector2 direction = Vector2.zero;

    public LayerMask wallLayer;

    private Rigidbody2D rigidbody;

    private void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        if(direction != Vector2.zero)
        {
            SetDirection(direction);
        }
    }

    private void FixedUpdate() 
    {
        Move();
    }

    private void Move()
    {
        if (!rigidbody) { return; }

        Vector2 movement = direction * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + movement);
    }

    public void SetDirection(Vector2 newDirection)
    {
        if(CanGoInDirection(newDirection))
        {
            direction = newDirection;
        }
        else if(CanGoInDirection(direction))
        {
            return;
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private bool CanGoInDirection(Vector2 newDirection)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position , Vector2.one * 0.75f, 0.0f, newDirection, 0.15f, wallLayer);
        return hit.collider == null;
    }
}
