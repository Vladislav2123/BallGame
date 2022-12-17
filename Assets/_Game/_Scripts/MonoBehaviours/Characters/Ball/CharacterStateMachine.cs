public class CharacterStateMachine : StateMachine
{
    private Character _character;

    protected override void Awake()
    {
        _character = GetComponent<Character>();

        base.Awake();
    }

    protected override void InitializeStates()
    {
        _states[typeof(CharacterStateAim)] = new CharacterStateAim(_character);
    }

    public void SetStateAim()
    {
        SetState<CharacterStateAim>();
    }
}
