public interface IState
{
    public void OnEnter();
    public void OnExit();
    public bool CanEnter();
    public bool CanExit();
    public void OnUpdate();
    public void OnFixedUpdate();

}
