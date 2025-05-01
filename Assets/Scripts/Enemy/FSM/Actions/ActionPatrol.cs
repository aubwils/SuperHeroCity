using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPatrol : EnemyFSMAction
{
    [Header("Patrol Settings")]
    [SerializeField] private float speed = 1.3f;
        
    private Waypoint waypoint;
    private int waypointIndex;
    private Vector3 nextPosition;

    private void Start()
    {
        waypoint = GetComponent<Waypoint>();
    }


    public override void Act()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetNextPosition(), speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, GetNextPosition()) <= 0.1f)
        {
            UpdateNextPosition();
        }
    }

    private void UpdateNextPosition()
    {
            waypointIndex++;
            if (waypointIndex >= waypoint.Waypoints.Length)
            {
                waypointIndex = 0;
            }
    }


    private Vector3 GetNextPosition()
    {
    return waypoint.GetPosition(waypointIndex);
    }



}
