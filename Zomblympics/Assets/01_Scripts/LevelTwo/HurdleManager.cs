using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LevelTwo;

public class HurdleManager : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField]
    private Slider progressBar;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private GameObject endMenu;


    private bool endIsReached = false;

    [ReadOnly, SerializeField]
    private float time = 0;  

    // Start is called before the first frame update
    private void Start() {
        Hurdle.OnHurdleHit += HurdleHit;
        progressBar.gameObject.SetActive(false);
    }

    private void HurdleHit() {
        time += 10;
    }

    // Update is called once per frame
    public void OnUpdate(PlayerController _player) {
        if(!endIsReached) {
            time += Time.deltaTime;
        }

        progressBar.value = Vector2.Distance(_player.transform.position, transform.position);
        timerText.text = FormatTime();
    }

    public void InitializeSlider(Vector2 _startPos) {
        progressBar.gameObject.SetActive(true);
        progressBar.maxValue = Vector2.Distance(_startPos, transform.position);
    }

    public string FormatTime() {
        int minutes = (int)time / 60;
        int seconds = (int)time;
        int milliseconds = (int)(1000 * (time - seconds));
        string timeText = string.Format("{0:00}:{1:000}", seconds, milliseconds);
        return timeText;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player)) {
            StartCoroutine(DisplayEnd(player));
        }
    }

    private IEnumerator DisplayEnd(PlayerController _player) {
        endIsReached = true;
        _player.animator.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        _player.canMove = false;
        progressBar.gameObject.SetActive(false);
        endMenu.SetActive(true);
        timerText.GetComponent<Animator>().SetTrigger("End");

        yield break;
    }

    public void ExitLevel() {
        FindObjectOfType<LevelTwoState>().runner.SwitchState(typeof(MainMenuState));
    }

    public void RestartLevel() {
        FindObjectOfType<LevelTwoState>().runner.SwitchState(typeof(LevelTwoState));
    }
}
