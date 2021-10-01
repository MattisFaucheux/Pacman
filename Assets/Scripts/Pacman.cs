using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    private PlayerMovements movements;

    private void Start()
    {
        movements = GetComponent<PlayerMovements>();
    }

    private void Update()
    {
        UpdateMovements();
    }

    private void UpdateMovements()
    {
        if(!movements) { return; }

        if(Input.GetButtonDown("Right"))
        {
            movements.SetDirection(Vector2.right);
        }
        else if(Input.GetButtonDown("Left"))
        {
            movements.SetDirection(Vector2.left);
        }
        else if(Input.GetButtonDown("Up"))
        {
            movements.SetDirection(Vector2.up);
        }
        else if(Input.GetButtonDown("Down"))
        {
            movements.SetDirection(Vector2.down);
        }
    }
}
