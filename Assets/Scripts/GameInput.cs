using UnityEngine;

public class GameInput : MonoBehaviour
{
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
