using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movements))]
public class Pacman : MonoBehaviour
{
    private Movements movements;

    private void Start()
    {
        movements = GetComponent<Movements>();
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

    public void EnablePowerUp(float duration, float newSpeedMultiplier)
    {
        movements.SetSpeedMultiplier(newSpeedMultiplier);
        Invoke("DisablePowerUp", duration);
    }

    private void DisablePowerUp()
    {
        movements.SetSpeedMultiplier(1.0f);
    }
}
