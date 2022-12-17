using UnityEngine;

public class TrajectoryArrow : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private Transform _scalingPart;
    [SerializeField] private Transform _movingPart;
    [Header("Animations")]
    [SerializeField] private CustomAnimation _showAnimation;
    [SerializeField] private CustomAnimation _hideAnimation;

    private bool _isShowing;
    public bool IsShowing
    {
        get => _isShowing;
        set
        {
            _isShowing = value;

            if(_isShowing)
            {
                Length = 0;
                _content.SetActive(true);
                if (_showAnimation != null) _showAnimation.Play();
            }
            else
            {
                if (_hideAnimation != null) _hideAnimation.Play(() => _content.SetActive(false));
                else _content.SetActive(false);
            }
        }
    }

    private Vector3 _direction;
    public Vector3 Direction
    {
        get => _direction;
        set
        {
            _direction = value;

            transform.right = _direction;
        }
    }

    private float _length;
    Vector3 _scale = Vector3.one;
    Vector3 _position = Vector3.zero;
    public float Length
    {
        get => _length;
        set
        {
            _length = value;

            _scale.x = _length;
            _scalingPart.localScale = _scale;

            _position.x = _length;
            _movingPart.localPosition = _position; 
        }
    }
}
