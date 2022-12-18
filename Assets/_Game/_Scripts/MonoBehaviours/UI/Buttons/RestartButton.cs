using UnityEngine;
using Zenject;

public class RestartButton : MonoBehaviour
{
    [Inject] private LevelsManager _levelsManager;

    public void OnClick()
    {
        _levelsManager.RestartLevel();
    }
}
