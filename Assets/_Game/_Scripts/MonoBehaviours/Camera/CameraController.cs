using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _boundsExpand;

    [Inject] private LevelsManager _levelsManager;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        FitToMap();
    }

    public void FitToMap()
    {
        Bounds bounds = _levelsManager.Level.Bounds;

        bounds.Expand(_boundsExpand);

        float horizontal = bounds.size.x * _camera.pixelHeight / _camera.pixelWidth;
        float vertical = bounds.size.y;

        float size = Mathf.Max(horizontal, vertical) / 2;

        _camera.orthographicSize = size;
    }
}