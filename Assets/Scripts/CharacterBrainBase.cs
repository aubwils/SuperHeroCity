using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBrainBase : MonoBehaviour
{
    public Animator animator { get; protected set; }
    public Rigidbody2D rb { get; protected set; }
    public CapsuleCollider2D characterCollider { get; protected set; }

    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<CapsuleCollider2D>();
    }
}
