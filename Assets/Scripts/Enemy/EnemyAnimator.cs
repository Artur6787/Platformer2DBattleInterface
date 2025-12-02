using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    //private Animator _animator;

    //private void Awake()
    //{
    //    _animator = GetComponent<Animator>();
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.TryGetComponent<Player>(out _))
    //    {
    //        Debug.Log("Враг столкнулся с игроком, запускаем анимацию удара.");
    //        _animator.SetTrigger("HitTrigger");
    //    }
    //}




    private Animator _animator;

    [SerializeField] private Transform _playerTransform; // назначьте игрока в инспекторе

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            RotateTowardsPlayer(_playerTransform);
            Debug.Log("Враг столкнулся с игроком, запускаем анимацию удара.");
            _animator.SetTrigger("HitTrigger");
        }
    }

    public void RotateTowardsPlayer(Transform playerTransform)
    {
        if (playerTransform == null)
            return;

        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        float rotationSpeed = 20f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}