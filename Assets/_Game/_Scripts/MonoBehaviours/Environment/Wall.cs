using UnityEngine;
using Zenject;

public class Wall : MonoBehaviour
{
    [SerializeField] private Material _loseMaterial;

    [Inject] private GameStateController _gameStateController;

    private MeshRenderer _renderer;

    private void Awake()
    {
        _gameStateController.OnLoseEvent += Paint;

        _renderer = GetComponent<MeshRenderer>();
    }

    private void Paint()
    {
        _renderer.material = _loseMaterial;
    }
}
