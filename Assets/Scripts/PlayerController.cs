using UnityEngine;

public class PlayerController : MovementController
{
    public static PlayerController instance;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject[] furniturePreviews;
    [SerializeField] private Inventory inventory;
    public Inventory Inventory => inventory;
    [SerializeField] private Interactor interactor;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        SwitchEditingMode();

        if (StateManager.IsPaused()) return;
        if (StateManager.IsEditing()) return;

        Animate();
        MoveObject();
        CheckInteract();
    }

    private void SwitchEditingMode()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || (IsMoving && StateManager.currentGameState == GameState.Moving)) return;

        FurnitureOriginalData furnitureData = inventory.furnitureInventory;

        if (furnitureData == null) return;
        
        StateManager.SwitchEditMode();

        Debug.Log($"Game State: {StateManager.currentGameState}");


        foreach (var furniturePreview in furniturePreviews)
        {
            if (furniturePreview == furniturePreviews[(int)furnitureData.typeOfSize])
            {
                furniturePreview.GetComponent<FurniturePreview>().data = furnitureData;
                furniturePreview.SetActive(!furniturePreview.activeInHierarchy);
            }
            else
            {
                furniturePreview.SetActive(false);
            }
        }
    }

    private void CheckInteract()
    {
        if(Input.GetMouseButtonDown(0) && !IsMoving)
        {
            interactor.Interact(inventory);
        }
    }


    private void Animate()
    {
        if (transform.position != movePoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            anim.SetBool("IsWalking", true);
        }
        else
            anim.SetBool("IsWalking", false);
    }
}