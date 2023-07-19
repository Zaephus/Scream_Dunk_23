
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState<GameManager> {

    [SerializeField]
    private GameObject mainMenu;

    public override void OnStart() {
        mainMenu.SetActive(true);
    }

    public override void OnUpdate() {}

    public override void OnEnd() {
        mainMenu.SetActive(false);
    }

    public void StartGame(int _level) {
        if (_level == 1) {
            runner.SwitchState(typeof(LevelOneState));
        }

        if (_level == 2) {
            runner.SwitchState(typeof(LevelTwoState));
        }
    }

}