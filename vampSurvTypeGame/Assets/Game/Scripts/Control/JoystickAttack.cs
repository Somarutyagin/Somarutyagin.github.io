using UnityEngine;

public class JoystickAttack : JoystickHandler
{
    public Quaternion AttackDirection(Vector3 weaponPos)
    {
        Vector3 mousePosition;

        if (_platform == Platform.IOS || _platform == Platform.Android)
            mousePosition = (Vector3)_inputVector;
        else
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = 0;

        Vector3 direction = mousePosition - weaponPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
