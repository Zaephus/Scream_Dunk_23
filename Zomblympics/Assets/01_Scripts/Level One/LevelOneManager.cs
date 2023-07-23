
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneManager : MonoBehaviour {
    
    public LevelOneState levelState;
    
    public void ResetLevel() {
        levelState.runner.SwitchState(typeof(LevelOneState));
    }

    public void MainMenu() {
        levelState.runner.SwitchState(typeof(MainMenuState));
    }
    
}