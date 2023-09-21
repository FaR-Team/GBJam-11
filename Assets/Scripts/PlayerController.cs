using UnityEngine;
using GBTemplate;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] private Animator anim;
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    public bool IsInEditingMode { get; private set; }

    void Start()
    {
        //playerInput = GBConsoleController.GetInstance();
        movePoint.parent = null;
    }

    void Update()
    {
        if (StateManager.IsPaused()) return;

        if (StateManager.IsEditing()) return;

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (transform.position != movePoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            anim.SetBool("IsWalking", true);
        }
        else anim.SetBool("IsWalking", false);

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
}

public class StateManager : MonoBehaviour
{
    public static GameState currentGameState;

    public static bool IsPaused()
    {
        return currentGameState == GameState.Pause;
    }
    public static bool IsMoving()
    {
        return currentGameState == GameState.Moving;
    }
    public static bool IsEditing()
    {
        return currentGameState == GameState.Editing;
    }

}
public enum GameState { Pause, Moving, Editing };


public class FurnitureController : MonoBehaviour
{
    private void Update()
    {
        if (StateManager.IsPaused()) return;

        if (StateManager.IsMoving()) return;
    }
}