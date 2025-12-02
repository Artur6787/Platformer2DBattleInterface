using System;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private float _visionDistance = 5f;
    [SerializeField] private float _searchInterval = 0.5f;
    [SerializeField] private Invincibility _playerInvincibility;

    private Transform _target; 
    private bool _lastVisibility;
    private float _searchTimer = 0f;

    public event Action<bool> PlayerVisibilityChanged;

    private void Update()
    {
        _searchTimer += Time.deltaTime;

        if (_searchTimer >= _searchInterval)
        {
            _searchTimer = 0f;
            TryFindPlayer();
        }

        bool visible = IsPlayerVisible();

        if (visible != _lastVisibility)
        {
            PlayerVisibilityChanged?.Invoke(visible);
            _lastVisibility = visible;
        }
    }

    public bool IsPlayerVisible()
    {
        if (_target == null)
            return false;
        if (_playerInvincibility != null && _playerInvincibility.IsProtected())
            return false;

        float directionSign = Mathf.Sign(_target.position.x - transform.position.x);
        Vector2 direction = new (directionSign, 0);
        float distanceSqr = ((Vector2)_target.position - (Vector2)transform.position).sqrMagnitude;
        float visionDistanceSqr = _visionDistance * _visionDistance;

        if (distanceSqr > visionDistanceSqr)
            return false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, _visionDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject == _target.gameObject)
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);
                return true;
            }
        }

        return false;
    }

    public Vector2 GetTargetPosition()
    {
        if (_target != null)
            return (Vector2)_target.position;
        else
            return (Vector2)transform.position;
    }

    private void TryFindPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _visionDistance);

        foreach (var col in colliders)
        {
            Player player = col.GetComponent<Player>();

            if (player != null)
            {
                _target = player.transform;
                _playerInvincibility = player.GetComponent<Invincibility>();
                return;
            }
        }

        _target = null;
        _playerInvincibility = null;
    }
}