using UnityEngine;

[RequireComponent(typeof(Vision))]
public class Chaser : MonoBehaviour
{
    private Vision _vision;

    private void Awake()
    {
        _vision = GetComponent<Vision>();
    }

    public bool HasTarget()
    {
        return _vision != null && _vision.IsPlayerVisible();
    }

    public Vector2 GetTargetPosition()
    {
        return _vision != null ? _vision.GetTargetPosition() : (Vector2)transform.position;
    }
}