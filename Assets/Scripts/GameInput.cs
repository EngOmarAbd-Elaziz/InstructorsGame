using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnPauseAction;
    public enum PlayerID 
    {
        Player1,
        Player2
    }

    [SerializeField] private PlayerID playerID;
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // only Player 1 handles the global Pause to prevent double-toggling
        if (playerID == PlayerID.Player1)
        {
            Instance = this; // Set Singleton
            playerInputActions.Player.Pause.performed += Pause_performed;
        }
    }

    public void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) 
    {
        OnPauseAction?.Invoke(this ,EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        if (playerID == PlayerID.Player1)
        {
            return playerInputActions.Player.Move_P1.ReadValue<Vector2>();
        }
        else 
        {
            return playerInputActions.Player.Move_P2.ReadValue<Vector2>();
        }
    }

    public bool IsJumpPressed()
    {
        if (playerID == PlayerID.Player1)
        {
            return playerInputActions.Player.Jump_P1.WasPressedThisFrame();
        }
        else
        {
            return playerInputActions.Player.Jump_P2.WasPressedThisFrame();
        }
    }
}
