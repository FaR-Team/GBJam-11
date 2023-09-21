using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;

public class PlayerController : MonoBehaviour
{
    private GBConsoleController gbConsoleController;

    [SerializeField] private Animator anim;
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    void Start()
    {
        gbConsoleController = GBConsoleController.GetInstance();
        movePoint.parent = null;
    }

    void Update()
    {
        if (transform.position != movePoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            anim.SetBool("IsWalking", true);
        }
        else anim.SetBool("IsWalking", false);

        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(xInput) == 1f)
            {
                transform.up = Vector2.right * xInput;
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(xInput, 0f, 0f), .1f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(xInput, 0f, 0f);
                }
            } else if (Mathf.Abs(yInput) == 1f)
            {
                transform.up = Vector2.up * yInput;
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, yInput, 0f), .1f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, yInput, 0f);
                }
            }
        }
    }
}
