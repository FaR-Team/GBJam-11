﻿using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] protected Transform movePoint;
    [SerializeField] protected float moveSpeed = 5f;

    public LayerMask whatStopsMovement;
    protected PlayerInput playerInput;

    public bool IsInEditingMode { get; private set; }

    protected void MoveObject()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");

        if (Vector3.Distance(transform.position, movePoint.position) > .05f) return;


        if (Mathf.Abs(xInput) == 1f)
        {
            transform.up = Vector2.right * xInput;
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(xInput, 0f, 0f), .1f, whatStopsMovement)) return;

            movePoint.position += new Vector3(xInput, 0f, 0f);
        }
        else if (Mathf.Abs(yInput) == 1f)
        {
            transform.up = Vector2.up * yInput;
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, yInput, 0f), .1f, whatStopsMovement)) return;

            movePoint.position += new Vector3(0f, yInput, 0f);
        }
    }

    void Start()
    {
        movePoint.parent = null;
    }
}