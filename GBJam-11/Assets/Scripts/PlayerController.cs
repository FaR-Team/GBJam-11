using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;

public class PlayerController : MonoBehaviour
{
    private GBConsoleController gbConsoleController;

    public float moveSpeed = 5f;
    public Transform movePoint;

    void Start()
    {
        gbConsoleController = GBConsoleController.GetInstance();
        movePoint.parent = null;
    }

    void Update()
    {
        if (gbConsoleController.Input.UpJustPressed)
        {
            transform.Translate(Vector2.up);
        }
        else if (gbConsoleController.Input.DownJustPressed)
        {
            transform.Translate(Vector2.down);
        }
        else if (gbConsoleController.Input.LeftJustPressed)
        {
            transform.Translate(Vector2.left);
        }
        else if (gbConsoleController.Input.RightJustPressed)
        {
            transform.Translate(Vector2.right);
        }
    }
}
