using System;
using UnityEngine;

public class FurniturePreviewMovement : MovementController
{
    private float moveCooldown = .2f;
    private float moveCooldownCounter;


    void Update()
    {
        if (StateManager.IsPaused()) return;
        if (StateManager.IsMoving()) return;

        //MoveObject();

        if (moveCooldownCounter >= 0)
        {
            moveCooldownCounter -= Time.deltaTime;
        }
        else
        {
            var xInput = Input.GetAxisRaw("Horizontal");
            var yInput = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(xInput) == 1f)
            {
                if (Physics2D.OverlapCircle(transform.position + new Vector3(xInput, 0f, 0f), .1f,
                        whatStopsMovement) || moveCooldownCounter > 0) return;

                transform.position += new Vector3(xInput, 0f, 0f);;
                moveCooldownCounter = moveCooldown;
            }
            else if (Mathf.Abs(yInput) == 1f)
            {
                if (Physics2D.OverlapCircle(transform.position + new Vector3(0f, yInput, 0f), .1f,
                        whatStopsMovement) || moveCooldownCounter > 0) return;

                transform.position += new Vector3(0f, yInput, 0f);
                moveCooldownCounter = moveCooldown;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && moveCooldownCounter >= 0)
        {
            GetComponent<FurniturePreview>().Rotate();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<FurniturePreview>().PutFurniture();
        }
    }
}
