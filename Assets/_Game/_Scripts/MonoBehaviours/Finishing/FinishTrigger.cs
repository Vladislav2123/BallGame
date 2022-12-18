using UnityEngine;
using Zenject;

public class FinishTrigger : MonoBehaviour
{
    [Inject] private LevelsManager _levelsManager;
    [Inject] private GameStateController _gameStateController;

    private Character PLayerCharacter => _levelsManager.Level.PlayerCharacter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character) == false) return;
        if (character != PLayerCharacter) return;

        _gameStateController.State = GameState.Win;
    }
}
