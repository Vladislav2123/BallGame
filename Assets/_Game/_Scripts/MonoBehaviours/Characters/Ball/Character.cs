using UnityEngine;
using DG.Tweening;
using System;

public class Character : MonoBehaviour
{
    public event Action OnCollisionEvent;

    [Header("Moving")]
    [SerializeField] private AnimationProperties _moveAnimation;
    [SerializeField] private bool _changingDirection;
    [Space]
    [SerializeField] private TrajectoryArrow _trajectoryArrow;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private float _radius;

    private bool _collision;

    public CharacterStateMachine StateMachine { get; private set; }
    public TrajectoryArrow TrajectoryArrow => _trajectoryArrow;
    public bool CanMove => _changingDirection ? true : IsMoving == false;
    public bool IsMoving { get; private set; }
    public Vector3 AimDirection { get; set; }
    public float AimLength { get; set; }

    private bool _isAiming;
    public bool IsAiming
    {
        get => _isAiming;
        set
        {
            if (value && CanMove == false) return;

            _isAiming = value;

            if (_isAiming) StateMachine.SetStateAim();
            else StateMachine.ResetState();
        }
    }


    private void Awake()
    {
        StateMachine = GetComponent<CharacterStateMachine>();
    }

    private Sequence _movingSequence;
    public void TryMove()
    {
        if (CanMove == false) return;

        Vector3 targetPoint = transform.position + AimDirection * AimLength;

        if (Physics.SphereCast(transform.position, _radius, AimDirection,  out RaycastHit hitInfo, AimLength + _radius, _wallLayer))
        {
            targetPoint = transform.position + AimDirection * hitInfo.distance;
            _collision = true;
        }
        else _collision = false;

        float moveDistance = Vector3.Distance(transform.position, targetPoint);
        float duration = _moveAnimation.Duration * (moveDistance / AimLength);

        _movingSequence.Kill();
        _movingSequence = DOTween.Sequence();

        _movingSequence.Append(transform.DOMove(targetPoint, duration).SetEase(_moveAnimation.Ease, _moveAnimation.EaseOvershoot))
            .OnComplete(OnStopMoving);

        IsMoving = true;
    }

    private void OnStopMoving()
    {
        IsMoving = false;
        if (_collision) OnCollisionEvent?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
