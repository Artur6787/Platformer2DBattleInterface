using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float _rightAngle = 0f;
    private float _leftAngle = 180f;
    private Quaternion _rightRotation;
    private Quaternion _leftRotation;

    private void Awake()
    {
        _rightRotation = Quaternion.Euler(0f, _rightAngle, 0f);
        _leftRotation = Quaternion.Euler(0f, _leftAngle, 0f);
    }

    public void Reflect(Vector3 direction)
    {
        if (direction.x > 0)
        {
            transform.rotation = _rightRotation;
        }
        else if (direction.x < 0)
        {
            transform.rotation = _leftRotation;
        }
    }
}