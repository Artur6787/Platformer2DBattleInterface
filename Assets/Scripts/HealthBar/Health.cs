using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealthPoints = 100f;

    private float _currentHealthPoints = 100f;

    public event Action<float> ValueChanged;
    public event Action Died;

    public float CurrentHealthPoints { get { return _currentHealthPoints; } }
    public float MaxHealthPoints { get { return _maxHealthPoints; } }

    private void Awake()
    {
        _currentHealthPoints = _maxHealthPoints;
    }

    public void ChangeAmount(float amount)
    {
        _currentHealthPoints = Mathf.Clamp(_currentHealthPoints + amount, 0f, _maxHealthPoints);
        ValueChanged?.Invoke(_currentHealthPoints / _maxHealthPoints);

        if (_currentHealthPoints <= 0f)
            HandleDeath();
    }

    private void HandleDeath()
    {
        Died?.Invoke();
    }
}