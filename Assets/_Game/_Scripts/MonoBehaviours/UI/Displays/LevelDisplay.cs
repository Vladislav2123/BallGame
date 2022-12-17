using UnityEngine;
using Zenject;
using TMPro;

public class LevelDisplay : UIPanel
{
    [SerializeField] private TextMeshProUGUI _levelText;

    [Inject] private LevelsManager _levelsManager;

    private const string LEVEL_NUMBER_PREFIX = "Level ";

    private void Start()
    {
        _levelText.text = $"{LEVEL_NUMBER_PREFIX}{_levelsManager.CompletedLevels}";
    }
}