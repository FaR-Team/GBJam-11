using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static GameState currentGameState = GameState.Moving;

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
    private void Awake()
    {
        //InputPlayerManager._playerInput.
    }
}
