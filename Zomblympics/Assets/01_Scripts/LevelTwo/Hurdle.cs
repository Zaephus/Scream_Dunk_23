using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelTwo;

public class Hurdle : MonoBehaviour
{
    public delegate void HurdleHit();
    public static event HurdleHit OnHurdleHit;
    private PlayerController player;

    private void Start() {
        player = FindAnyObjectByType<PlayerController>();
    }

    private void Update() {
        if (player.transform.position.x > transform.position.x) {
            player.Accelerate();
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        OnHurdleHit();
        Destroy(this);
    }
}
