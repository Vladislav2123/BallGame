using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private Transform _levelsParent;
    [SerializeField] private Level[] _levels;
    [SerializeField] private float _restartDelay;
    [SerializeField] private bool _fading;

#if UNITY_EDITOR
    [Header("=== TESTING ===")]
    [SerializeField] private bool _isTesting;
    [SerializeField] private int _testingLevel;
#endif

    [Inject] private DiContainer _diContainer;
    [Inject] private UIFade _uiFade;
    private Level _currentLevel;

    private const string CURRENT_LEVEL_KEY = "CurrentLevel";
    private const string COMPLETED_LEVELS_KEY = "CompletedLevels";
    private const int LOOP_LEVEL = 2;                                   // Level, playing on replay

    public static LevelsManager Instance { get; private set; }

    public int CompletedLevels
    {
        get => PlayerPrefs.GetInt(COMPLETED_LEVELS_KEY, 1);
        set => PlayerPrefs.SetInt(COMPLETED_LEVELS_KEY, value);
    }
    public int LevelNumber
    {
        get => PlayerPrefs.GetInt(CURRENT_LEVEL_KEY, 1);
        set => PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, value);
    }
    public Level Level
    {
        get
        {
            if (_currentLevel == null) LoadLevel();

            return _currentLevel;
        }
        private set => _currentLevel = value;
    }

    public bool Restarting { get; private set; }

    public Level[] Levels => _levels;

    private void Awake()
    {
        Instance = this;
        if (Level == null) LoadLevel();
    }

    private void LoadLevel()
    {
#if UNITY_EDITOR
        if (_isTesting)
        {
            LoadLevel(_testingLevel);
            return;
        }
#endif

        LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        if (LevelNumber > _levels.Length) LevelNumber = _levels.Length - 1;
        LoadLevel(LevelNumber);
    }

    public void LoadNextLevel(float delay)
    {
        Invoke(nameof(LoadNextLevel), delay);
    }
    public void LoadNextLevel()
    {
        if (Restarting) return;

        CompleteLevel();
        RestartLevel();
    }

    public async void RestartLevel(float delay)
    {
        if (Restarting) return;

        await Task.Delay((int)(delay * 1000));

        RestartLevel();
    }
    public void RestartLevel()
    {
        if (Restarting) return;

        Restarting = true;
        StartCoroutine(RestartRoutine());
    }

    private void CompleteLevel()
    {
#if UNITY_EDITOR
        if (_isTesting) return;
#endif

        if (LevelNumber < _levels.Length) LevelNumber++;
        else LevelNumber = LOOP_LEVEL;

        CompletedLevels++;
    }

    private void LoadLevel(int levelNumber)
    {
        if (Restarting) return;
             
        Level level = _levels[levelNumber - 1];
        Level = _diContainer.InstantiatePrefab(level, level.transform.position, Quaternion.identity, _levelsParent).GetComponent<Level>();
        if (_fading) _uiFade.FadeOut();
    }
    private IEnumerator RestartRoutine()
    {
        if (_fading) _uiFade.FadeIn();

        yield return new WaitForSeconds(_restartDelay);

        RestartScene();
    }

    private void RestartScene(bool plug = true)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}