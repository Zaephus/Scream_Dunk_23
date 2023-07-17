
using UnityEngine;

public abstract class BaseState<T> : MonoBehaviour where T : Object {

    [HideInInspector]
    public T runner;

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();

}