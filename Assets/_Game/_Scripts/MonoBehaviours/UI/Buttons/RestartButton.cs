using UnityEngine;
using Zenject;

public class RestartButton : UIPanel
{
    [Inject] private LevelsManager _levelsManager;

    public void OnClick()
    {
        _levelsManager.RestartLevel();
    }
}
