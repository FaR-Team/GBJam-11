public class FurniturePreviewMovement : MovementController
{
    void Update()
    {
        if (StateManager.IsPaused()) return;
        if (StateManager.IsMoving()) return;

        MoveObject();
    }
}
