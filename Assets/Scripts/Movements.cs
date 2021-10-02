using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movements : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private float speedMultiplier = 1.0f;
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector2 initialDirection = Vector2.zero;

    public LayerMask wallLayer;
    private Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        direction = initialDirection;
    }

    private void Update() 
    {
        if(nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    public void Reset() 
    {
        speedMultiplier = 1.0f;
        direction = initialDirection;
    }

    private void FixedUpdate() 
    {
        Move();
    }

    private void Move()
    {
        if (!rb || !CanGoInDirection(direction, 0.2f)) { return; }

        Vector2 movement = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
    }

    public void SetDirection(Vector2 newDirection)
    {
        if (CanGoInDirection(newDirection, 1.5f))
        {
            direction = newDirection;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = newDirection;
        }
    }

    private bool CanGoInDirection(Vector2 newDirection, float distanceCheck)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position , Vector2.one * 0.75f, 0.0f, newDirection, distanceCheck, wallLayer);
        return hit.collider == null;
    }

    public void SetSpeedMultiplier(float newSpeedMultiplier)
    {
        speedMultiplier = newSpeedMultiplier;
    }
}
