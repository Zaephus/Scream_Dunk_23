
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : StateRunner<GameManager> {

    private void Awake() {
        base.OnAwake();
    }

    private void Start() {
        SwitchState(typeof(MainMenuState));
    }

    private void Update() {
        base.OnUpdate();
    }

}