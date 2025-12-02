using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Invincibility : MonoBehaviour
{
    private const string PlayerLayer = "Player";
    private const string EnemyLayer = "Enemy";

    [SerializeField] private float _protectionDuration = 2f;
    [SerializeField] private float _blinkSpeed = 0.2f;

    private bool _isProtected = false;
    private Renderer _objectRenderer;
    private WaitForSeconds _blinkWait;
    private int _playerLayerIndex;
    private int _enemyLayerIndex;
    private Coroutine _blinkCoroutine;
    private Coroutine _protectionCoroutine;

    private void Awake()
    {
        _objectRenderer = GetComponentInChildren<Renderer>();

        //if (_objectRenderer == null)
        //{
        //    Debug.LogError("Renderer не найден! Убедитесь, что у дочернего объекта есть SpriteRenderer/Renderer.");
        //    return;
        //}

        _blinkWait = new WaitForSeconds(_blinkSpeed);
        CacheLayerIndices();
    }

    public void MakeProtected()
    {
        // Уже защищён – ничего не делаем.
        if (_isProtected)
            return;

        _isProtected = true;
        SetLayerCollision(true);

        if (_blinkCoroutine != null)
            StopCoroutine(_blinkCoroutine);

        if (_protectionCoroutine != null)
            StopCoroutine(_protectionCoroutine);

        _blinkCoroutine = StartCoroutine(Blinking());
        _protectionCoroutine = StartCoroutine(ProtectionTimer());
    }

    public bool IsProtected()
    {
        return _isProtected;
    }

    private void CacheLayerIndices()
    {
        _playerLayerIndex = LayerMask.NameToLayer(PlayerLayer);
        _enemyLayerIndex = LayerMask.NameToLayer(EnemyLayer);
    }

    private void SetLayerCollision(bool ignore)
    {
        //if (_playerLayerIndex < 0 || _enemyLayerIndex < 0)
        //{
        //    Debug.LogWarning("Слои Player/Enemy не найдены.");
        //    return;
        //}

        Physics2D.IgnoreLayerCollision(_playerLayerIndex, _enemyLayerIndex, ignore);
    }

    private IEnumerator ProtectionTimer()
    {
        yield return new WaitForSeconds(_protectionDuration);
        DisableProtection();
    }

    private IEnumerator Blinking()
    {
        while (_isProtected)
        {
            _objectRenderer.enabled = !_objectRenderer.enabled;
            yield return _blinkWait;
        }

        _objectRenderer.enabled = true;
    }

    private void DisableProtection()
    {
        _isProtected = false;
        SetLayerCollision(false);
        // корутина Blinking сама выйдет из цикла
    }




    //private const string PlayerLayer = "Player";
    //private const string EnemyLayer = "Enemy";

    //[SerializeField] private float _protectionDuration = 2f;
    //[SerializeField] private float _blinkSpeed = 0.2f;

    //private bool _isProtected = false;
    //private Renderer _objectRenderer;
    //private WaitForSeconds _blinkWait;
    //private int _playerLayerIndex;
    //private int _enemyLayerIndex;

    //private void Awake()
    //{
    //    _objectRenderer = GetComponentInChildren<Renderer>();

    //    //if (_objectRenderer == null)
    //    //{
    //    //    Debug.LogError("Renderer не найден! Проверьте, что PlayerSprite является дочерним и содержит SpriteRenderer.");
    //    //    return;
    //    //}

    //    _blinkWait = new WaitForSeconds(_blinkSpeed);
    //    CacheLayerIndices();
    //}

    //public void MakeProtected()
    //{
    //    // Если уже мигаем/защищены — просто выходим, ничего не перезапускаем
    //    if (_isProtected)
    //        return;

    //    _isProtected = true;
    //    SetLayerCollision(true);
    //    StartCoroutine(Blinking());
    //    StartCoroutine(ProtectionTimer());
    //}

    //public bool IsProtected()
    //{
    //    return _isProtected;
    //}

    //private void CacheLayerIndices()
    //{
    //    _playerLayerIndex = LayerMask.NameToLayer(PlayerLayer);
    //    _enemyLayerIndex = LayerMask.NameToLayer(EnemyLayer);
    //}

    //private void SetLayerCollision(bool ignore)
    //{
    //    Physics2D.IgnoreLayerCollision(_playerLayerIndex, _enemyLayerIndex, ignore);
    //}

    //private IEnumerator ProtectionTimer()
    //{
    //    yield return new WaitForSeconds(_protectionDuration);
    //    DisableProtection();
    //}

    //private IEnumerator Blinking()
    //{
    //    while (_isProtected)
    //    {
    //        _objectRenderer.enabled = !_objectRenderer.enabled;
    //        yield return _blinkWait;
    //    }

    //    _objectRenderer.enabled = true;
    //}

    //private void DisableProtection()
    //{
    //    _isProtected = false;
    //    SetLayerCollision(false);
    //    // НЕ вызываем StopCoroutine(Blinking());
    //    // корутина сама завершится, когда _isProtected станет false
    //}



    //private const string PlayerLayer = "Player";
    //private const string EnemyLayer = "Enemy";

    //[SerializeField] private float _protectionDuration = 2f;
    //[SerializeField] private float _blinkSpeed = 0.2f;

    //private bool _isProtected = false;
    //private Renderer _objectRenderer;
    //private WaitForSeconds _blinkWait;
    //private int _playerLayerIndex;
    //private int _enemyLayerIndex;

    //private void Start()
    //{
    //    if (_objectRenderer == null)
    //    {
    //        Debug.LogError("Renderer не найден! Проверьте, что PlayerSprite является дочерним и содержит SpriteRenderer.");

    //        foreach (Transform child in transform)
    //        {
    //            Debug.Log("Дочерний объект: " + child.name + ", Renderer: " + child.GetComponent<Renderer>());
    //        }

    //        return;
    //    }

    //    _blinkWait = new WaitForSeconds(_blinkSpeed);
    //    CacheLayerIndices();
    //}

    //private void Awake()
    //{
    //    _objectRenderer = GetComponentInChildren<Renderer>();
    //}

    //public void MakeProtected()
    //{
    //    if (_isProtected == false)
    //    {
    //        _isProtected = true;
    //        StartCoroutine(Blinking());
    //        SetLayerCollision(true);
    //        StartCoroutine(ProtectionTimer());
    //    }
    //}

    //public bool IsProtected()
    //{
    //    return _isProtected;
    //}

    //private void CacheLayerIndices()
    //{
    //    _playerLayerIndex = LayerMask.NameToLayer(PlayerLayer);
    //    _enemyLayerIndex = LayerMask.NameToLayer(EnemyLayer);
    //}

    //private void SetLayerCollision(bool ignore)
    //{
    //    Physics2D.IgnoreLayerCollision(_playerLayerIndex, _enemyLayerIndex, ignore);
    //}

    //private IEnumerator ProtectionTimer()
    //{
    //    yield return new WaitForSeconds(_protectionDuration);
    //    DisableProtection();
    //}

    //private IEnumerator Blinking()
    //{
    //    while (_isProtected)
    //    {
    //        _objectRenderer.enabled = _objectRenderer.enabled == false;
    //        yield return _blinkWait;
    //    }

    //    _objectRenderer.enabled = true;
    //}

    //private void DisableProtection()
    //{
    //    _isProtected = false;
    //    SetLayerCollision(false);
    //    StopCoroutine(Blinking());
    //}
}