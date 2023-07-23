using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelTwo;

public class LevelTwoState : BaseState<GameManager> {

    [SerializeField]
    private GameObject level;
    private PlayerController player;
    private GameObject spawnedLevel;
    private HurdleManager hurdleManager;
    [ReadOnly, SerializeField]
    public bool startLevel = false;

    [Header("Settings")]
    [SerializeField]
    private float smoothTime;

    private Vector3 camVelocity = Vector3.zero;

    public Coroutine lerp;

    public override void OnStart() {
        InstantiateLevel();
        lerp = StartCoroutine(LerpCamera());
    }

    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            runner.SwitchState(typeof(MainMenuState));
        }

        if (startLevel) {
            hurdleManager.OnUpdate(player);
        }
    }

    public override void OnEnd() {
        if (spawnedLevel != null) {
            spawnedLevel?.SetActive(false);
        }
    }

    private void InstantiateLevel() {
        if (spawnedLevel == null) {
            spawnedLevel = Instantiate(level);
        }
        else {
            Destroy(spawnedLevel);
            Debug.Log("Destroyed");
            spawnedLevel = Instantiate(level);
        }

        player = spawnedLevel.GetComponentInChildren<PlayerController>();
        hurdleManager = spawnedLevel.GetComponent<HurdleManager>();

    }

    private IEnumerator LerpCamera() {
        yield return new WaitForSeconds(1);

        float elapsedTime = 0;
        Vector3 startPos = new Vector3(662, 2.52f, -10) + player.transform.position;

        while (elapsedTime < smoothTime) {
            player.cam.transform.position = Vector3.Lerp(startPos, player.camPos.position, (elapsedTime/smoothTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
       Debug.Log(elapsedTime);

        player.cam.transform.position = player.camPos.position;

        yield return new WaitForSeconds(1.5f);


        player.canMove = true;
        startLevel = true;

        hurdleManager.InitializeSlider(player.transform.position);
    }

}
