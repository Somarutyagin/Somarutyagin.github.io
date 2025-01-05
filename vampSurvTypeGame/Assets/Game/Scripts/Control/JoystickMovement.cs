using UnityEngine;

public class JoystickMovement : JoystickHandler
{
    public Vector3 MoveDirection()
    {
        if (_platform == Platform.IOS || _platform == Platform.Android)
            return (Vector3)_inputVector;
        else
            return new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    }
}
