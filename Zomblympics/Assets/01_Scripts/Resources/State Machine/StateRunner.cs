
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateRunner<T> : MonoBehaviour where T : StateRunner<T> {

    protected BaseState<T> state;

    private Dictionary<Type, BaseState<T>> states = new Dictionary<Type, BaseState<T>>();

    protected virtual void OnAwake() {
        BaseState<T>[] baseStates = FindObjectsOfType<BaseState<T>>();
        foreach(BaseState<T> baseState in baseStates) {
            baseState.runner = GetComponent<T>();
            states.Add(baseState.GetType(), baseState);
        }
    }

    protected virtual void OnUpdate() {
        state.OnUpdate();
    }

    public virtual void SwitchState(Type _type) {
        if(state != null) {
            state.OnEnd();
        }

        state = states[_type];

        state.OnStart();
    }

}