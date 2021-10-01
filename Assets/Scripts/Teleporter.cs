using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform teleportationPoint;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        collider.transform.position = new Vector3(teleportationPoint.position.x, teleportationPoint.position.y, collider.transform.position.z);
    }
}
