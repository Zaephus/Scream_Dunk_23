
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneState : BaseState<GameManager> {

    [SerializeField]
    private GameObject level;

    public override void OnStart() {
        level.SetActive(true);
    }

    public override void OnUpdate() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            runner.SwitchState(typeof(MainMenuState));
        }
    }

    public override void OnEnd() {
        level.SetActive(false);
    }
    
}