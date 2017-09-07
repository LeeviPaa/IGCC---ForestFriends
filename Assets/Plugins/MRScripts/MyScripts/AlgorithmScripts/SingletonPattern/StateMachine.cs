// !Author is Ryohei Masujima.
using UnityEngine;
using System.Collections;

public class StateMachine<T>
{
    private State<T> _currentlyState;

    public StateMachine()
    {
        _currentlyState = null;
    }

    public State<T> CurrentlyState
    {
        get { return _currentlyState; }
    }
   
    public void ChangeState(State<T> state)
    {
        if (_currentlyState != null)
        {
            _currentlyState.Exit();
        }

        _currentlyState = state;
        _currentlyState.Enter();
    }

    public void Update()
    {
        if (_currentlyState != null)
        {
            //Debug.Log("currentState is not null");
            _currentlyState.Execute();
        }
        else
        {
            //Debug.Log("currentState is null");
        }
    }
}