using UnityEngine;
using Zenject;

public class NextLevelButton : MonoBehaviour
{
    [Inject] private LevelsManager _levelsManager;

    public void OnClick()
    {
        _levelsManager.LoadNextLevel();
    }
}
