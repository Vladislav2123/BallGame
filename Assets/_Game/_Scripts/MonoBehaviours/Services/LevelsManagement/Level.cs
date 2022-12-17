using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level : MonoBehaviour
{
    [SerializeField] private Character _playerCharacter;

    public Character PlayerCharacter => _playerCharacter;

    private List<Background> _backgrounds;
    public List<Background> Backgrounds
    {
        get
        {
            if (_backgrounds == null) _backgrounds = GetComponentsInChildren<Background>().ToList();

            return _backgrounds;
        }
    }

    private List<Transform> _corners;
    public List<Transform> Corners
    {
        get
        {
            if (_corners == null)
            {
                _corners = new List<Transform>(Backgrounds.Count * 4);
                Backgrounds.ForEach(back => _corners.AddRange(back.Corners));
            }

            return _corners;
        }
    }

    private Bounds _bounds;
    public Bounds Bounds
    {
        get
        {
            _bounds = new Bounds();
            Corners.ForEach(corner => _bounds.Encapsulate(corner.position));

            return _bounds;
        }
    }
}