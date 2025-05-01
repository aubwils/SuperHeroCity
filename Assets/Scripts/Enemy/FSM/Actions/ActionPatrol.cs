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
    private Rigidbody2D rb;

    private void Start()
    {
        waypoint = GetComponent<Waypoint>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Act()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        Vector3 direction = GetNextPosition() - transform.position;
        rb.velocity = direction.normalized * speed;

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
