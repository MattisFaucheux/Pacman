using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movements))]
public class Pacman : MonoBehaviour
{
    public Movements movements { get; private set; }
    private Vector3 initialPos;

    private void Start()
    {
        movements = GetComponent<Movements>();
        initialPos = transform.position;
    }

    private void Update()
    {
        UpdateMovements();
    }

    public void Reset() 
    {
        CancelInvoke();

        movements.Reset();
        transform.position = initialPos;
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
