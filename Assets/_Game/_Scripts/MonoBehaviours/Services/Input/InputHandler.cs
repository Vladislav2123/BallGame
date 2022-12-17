using UnityEngine;
using System;
using Zenject;

public class InputHandler : MonoBehaviour
{
    public event Action OnPressedEvent;
    public event Action OnReleasedEvent;

    [SerializeField] private float _sensitivity = 1;

    [Inject] private Canvas _canvas;
    private Vector3 _lastFrameTouchPosition;
    private bool _isPressed;
    private bool _isBlocked;

    public bool IsPressed
    {
        get => _isPressed;
        set
        {
            if (IsBlocked) return;

            _isPressed = value;
            if (value)
            {
                _lastFrameTouchPosition = PointerPosition;
                OriginPointerPosition = PointerPosition;
                OnPressedEvent?.Invoke();
            }
            else
            {
                OriginPointerPosition = Vector3.zero;
                OnReleasedEvent?.Invoke();
            }
        }
    }

    public bool IsBlocked
    {
        get => _isBlocked;
        set
        {
            if (value == false && IsPressed) IsPressed = false;

            _isBlocked = value;
        }
    }

    public Vector3 OriginPointerPosition { get; private set; }
    public Vector3 PointerPosition => IsPressed ? Input.mousePosition : Vector3.zero;

    private Vector3 _dragDelta;
    public Vector3 DragDelta
    {
        get
        {
            if (IsPressed)
            {
                _dragDelta = (PointerPosition - OriginPointerPosition) / _canvas.scaleFactor;
                _dragDelta.x = _dragDelta.x / Screen.height * _canvas.scaleFactor * _sensitivity;
                _dragDelta.y = _dragDelta.y  / Screen.height * _canvas.scaleFactor * _sensitivity;

                return _dragDelta;
            }
            else return Vector3.zero;
        }
    }

    public Vector3 UpdateDragDelta { get; private set; }

    private void Update()
    {
        HandleUpdateDragDelta();
    }

    private void HandleUpdateDragDelta()
    {
        if (IsPressed == false)
        {
            UpdateDragDelta = Vector3.zero;
            return;
        }

        UpdateDragDelta = ((PointerPosition - _lastFrameTouchPosition) / _canvas.scaleFactor)
            * _sensitivity;
        _lastFrameTouchPosition = Input.mousePosition;
    }
}