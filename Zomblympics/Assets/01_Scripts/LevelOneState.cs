
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneState : BaseState<GameManager> {

    [SerializeField]
    private GameObject levelPrefab;
    private LevelOneManager level;

    public override void OnStart() {
        if(level != null) {
            Destroy(level.gameObject);
        }
        level = Instantiate(levelPrefab).GetComponent<LevelOneManager>();
        level.levelState = this;
    }

    public override void OnUpdate() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            runner.SwitchState(typeof(MainMenuState));
        }
    }

    public override void OnEnd() {
        if(level != null) {
            Destroy(level.gameObject);
        }
    }
    
}