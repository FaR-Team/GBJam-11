using UnityEngine;

public class PlayerController : MovementController
{
    [SerializeField] private Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StateManager.currentGameState = StateManager.currentGameState == GameState.Moving ? GameState.Editing : GameState.Moving;
            Debug.Log($"Game State: {StateManager.currentGameState.ToString()}");
        }
        
        if (StateManager.IsPaused()) return;
        if (StateManager.IsEditing()) return;
        
        Animate();
        MoveObject();

        
    }
    private void Animate()
    {
        if (transform.position != movePoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            anim.SetBool("IsWalking", true);
        }
        else anim.SetBool("IsWalking", false);
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private FurniturePreview furniture;
}