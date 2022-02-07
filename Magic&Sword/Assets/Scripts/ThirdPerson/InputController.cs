
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public bool strafe;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnStrafe(InputValue value)
    {
        StrafeInput(value.isPressed);
    }


    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    } 
    
    public void StrafeInput(bool newStrafeState)
    {
        strafe = newStrafeState;
    }
}
