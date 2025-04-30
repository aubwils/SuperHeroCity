using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWander : EnemyFSMAction
{
    [Header("Wandering Settings")]
    [SerializeField] private float speed = 2f; // Speed of the wandering movement
    [SerializeField] private float wanderTimer = 5f; // Time interval for changing direction
    [SerializeField] private Vector2 moveRange; // Range for random movement

    private float timer; // Timer to track the time for changing direction
    private Vector3 movePosition;  

    private void Start()
    {
        GetNewDestination();
    }

    public override void Act()
    {
        timer -= Time.deltaTime;
        Vector3 moveDirection = (movePosition - transform.position).normalized;
        Vector3 movement = moveDirection * (speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePosition) > 0.5f)
        {
            transform.Translate(movement); // Move the enemy towards the new position
                    Debug.Log($"Moving towards: {movePosition}");

        }

        if (timer <= 0f)
        {
            GetNewDestination();
            timer = wanderTimer;
                    Debug.Log($"New destination: {movePosition}");

        }

    }

    private void GetNewDestination()
    {
        float randomX = Random.Range(-moveRange.x, moveRange.x);
        float randomY = Random.Range(-moveRange.y, moveRange.y);
        movePosition = transform.position + new Vector3(randomX, randomY);
            Debug.Log($"Generated new destination: {movePosition}");

    }

    private void OnDrawGizmosSelected()
    {
        if(moveRange != Vector2.zero)
        {
            Gizmos.color = Color.cyan; 
            Gizmos.DrawWireCube(transform.position, moveRange * 2); // Draw a wireframe cube to visualize the movement range
            Gizmos.DrawLine(transform.position, movePosition); // Draw a line to the current position
        }
    }

}
