using System;   
using UnityEngine;

public class GameInput : MonoBehaviour
{
<<<<<<< HEAD
    public enum PlayerID
=======
    public static GameInput Instance { get; private set; }
    public event EventHandler OnPauseAction;
    public enum PlayerID 
>>>>>>> f701581f65074aaba322fb59c267a050345dd216
    {
        Player1,
        Player2
    }

    [SerializeField] private PlayerID playerID;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        // only Player 1 handles the global Pause to prevent double-toggling
        if (playerID == PlayerID.Player1)
        {
            Instance = this; // Set Singleton
            playerInputActions.Player.Pause.performed += Pause_performed;
        }
    }

    private void OnEnable()
    {
        // enable actions when this component becomes active
        playerInputActions?.Player.Enable();
    }

    private void OnDisable()
    {
        // disable actions when this component becomes inactive
        playerInputActions?.Player.Disable();
    }

    private void OnDestroy()
    {
        // Unsubscribe callbacks and release the generated asset to avoid leaks
        if (playerInputActions != null)
        {
            if (playerID == PlayerID.Player1)
            {
                playerInputActions.Player.Pause.performed -= Pause_performed;
                if (Instance == this) Instance = null;
            }

            // Dispose destroys the underlying InputActionAsset and prevents the finalizer warning
            playerInputActions.Dispose();
            playerInputActions = null;
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
