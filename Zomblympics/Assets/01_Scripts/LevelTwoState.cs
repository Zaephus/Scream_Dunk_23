using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelTwo;

public class LevelTwoState : BaseState<GameManager> {

    [SerializeField]
    private GameObject level;
    [SerializeField]
    private PlayerController player;

    [Header("Settings")]
    [SerializeField]
    private float smoothTime;

    private Vector3 camVelocity = Vector3.zero;

    public override void OnStart() {
        level.SetActive(true);
        StartCoroutine(LerpCamera());
    }

    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            runner.SwitchState(typeof(MainMenuState));
        }
    }

    public override void OnEnd() {
        level.SetActive(false);
    }

    private IEnumerator LerpCamera() {

        float elapsedTime = 0;
        Vector3 startPos = player.cam.transform.position;

        while (elapsedTime < smoothTime) {
            player.cam.transform.position = Vector3.Lerp(startPos, player.camPos.position, (elapsedTime/smoothTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
       Debug.Log(elapsedTime);

        player.cam.transform.position = player.camPos.position;

        yield return new WaitForSeconds(3);

        //TODO: Add visual timer

        player.canMove = true;

        yield break;
    }

}
