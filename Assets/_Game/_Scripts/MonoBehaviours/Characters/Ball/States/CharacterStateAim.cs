using UnityEngine;

public class CharacterStateAim : IState
{
    private Character _character;

    public CharacterStateAim(Character character)
    {
        _character = character;
    }

    public void Enter()
    {
        _character.TrajectoryArrow.IsShowing = true;
    }

    public void Exit()
    {
        _character.TrajectoryArrow.IsShowing = false;
    }

    public void Update()
    {
        _character.TrajectoryArrow.Direction = _character.AimDirection;
        _character.TrajectoryArrow.Length = _character.AimLength;
    }
}
