/******************************************************
File   : StatefulObjecBase.cs
Author : Masujima Ryohei
Date   : 2017/--/-- ~ 2017/--/--
Summary: For .
*******************************************************/

namespace MasujimaRyohei
{
    using UnityEngine;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class StatefulObjectBase<T, TEnum> : MonoBehaviour
        where T : class where TEnum : System.IConvertible
    {
        protected List<State<T>> _stateList = new List<State<T>>();

        protected StateMachine<T> _stateMachine;

        public virtual void ChangeState(TEnum state)
        {
            if (_stateMachine == null)
            {
                return;
            }

            _stateMachine.ChangeState(_stateList[state.ToInt32(null)]);
        }

        public virtual bool IsCurrentlyState(TEnum state)
        {
            if (_stateMachine == null)
            {
                return false;
            }

            return _stateMachine.CurrentlyState == _stateList[state.ToInt32(null)];
        }

        protected virtual void Update()
        {
            if (_stateMachine != null)
            {
                _stateMachine.Update();
            }
        }
    }
}


/******************************************************
                      END OF FILE
*******************************************************/
