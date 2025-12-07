using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed = 5f;

    private float _targetValue = 1f;
    private Coroutine _smoothCoroutine;

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

    private void OnHealthChanged(float value)
    {
        _targetValue = value;

        if (_smoothCoroutine != null)
        {
            StopCoroutine(_smoothCoroutine);
        }

        _smoothCoroutine = StartCoroutine(SmoothUpdateSlider());
    }

    private IEnumerator SmoothUpdateSlider()
    {
        while (_slider.value != _targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value,_targetValue,_smoothSpeed * Time.deltaTime);
            yield return null;
        }
    }
}