using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Waypoint : MonoBehaviour
{
    [Header("Waypoint Settings")]
    [SerializeField] private Vector3[] waypoints;

    public Vector3[] Waypoints => waypoints;
    public Vector3 EntityPosition { get; private set; }

    private bool gameStarted;

    private void Start()
    {
        EntityPosition = transform.position;
        gameStarted = true;
    }

    public Vector3 GetPosition(int waypointIndex)
    {
        return EntityPosition + waypoints[waypointIndex];
    }

    private void OnDrawGizmos()
    {
        if(gameStarted == false && transform.hasChanged)
        {
            EntityPosition = transform.position;
        }

    }

}
