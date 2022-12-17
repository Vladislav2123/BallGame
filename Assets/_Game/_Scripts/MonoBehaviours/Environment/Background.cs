using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private List<Transform> _corners;

    public List<Transform> Corners => _corners;
}
