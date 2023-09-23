using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] InputAction action;

    static InputPlayerManager instance;
    public static PlayerInput _playerInput => instance.playerInput;

    private void Awake()
    {
        instance = this;
    }
}