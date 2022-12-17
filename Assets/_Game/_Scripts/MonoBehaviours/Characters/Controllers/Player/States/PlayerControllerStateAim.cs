public class PlayerControllerStateAim: IState
{
    private PlayerController _controller;

    public PlayerControllerStateAim(PlayerController controller)
    {
        _controller = controller;
    }

    public void Enter()
    {
        _controller.Character.IsAiming = true;
    }

    public void Exit()
    {
        _controller.Character.IsAiming = false;
    }

    public void Update()
    {
        _controller.Character.AimDirection = _controller.InputHandler.DragDelta.normalized;
        _controller.Character.AimLength = _controller.InputHandler.DragDelta.magnitude;
    }
}
