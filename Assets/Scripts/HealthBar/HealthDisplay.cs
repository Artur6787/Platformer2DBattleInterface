using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed = 5f;
    //[SerializeField] private Transform _target;

    private float _targetValue = 1f;

    private void OnEnable()
    {
        _health.ValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= OnHealthChanged;
    }

    private void Start()
    {
        _slider.value = 1f;
    }

    private void Update()
    {
        if (_slider.value != _targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, _smoothSpeed * Time.deltaTime);
        }

        //transform.position = _target.position;
        transform.rotation = Quaternion.identity;
    }

    private void OnHealthChanged(float value)
    {
        _targetValue = value;
    }
}