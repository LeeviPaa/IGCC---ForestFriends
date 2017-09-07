// !Author is Ryohei Masujima.

public class State<T>
{
    // The instance that use this state.
    protected T _owner;

    public State(T owner)
    {
        this._owner = owner;
    }

    // It called only once at the time of transition to this state. 
    public virtual void Enter() { }

    // While in this state, it's called every frame.
    public virtual void Execute() { }

    // It called only once if it transition from this state to other one, 
    public virtual void Exit() { }
}
