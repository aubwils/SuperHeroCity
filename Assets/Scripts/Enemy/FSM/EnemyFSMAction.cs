using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFSMAction : MonoBehaviour
{
    public bool IsActive { get; set; } = true; // Default to active

    public abstract void Act();
}