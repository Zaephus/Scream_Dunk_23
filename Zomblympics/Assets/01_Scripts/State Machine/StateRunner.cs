using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateRunner<T> : MonoBehaviour {

    protected BaseState<T> state;

    protected virtual void OnUpdate() {
        state.OnUpdate();
    }

    public virtual void SwitchState(BaseState<T> _state) {
        if(state != null) {
            state.OnEnd();
        }

        state = _state;

        state.OnStart();
    }

}