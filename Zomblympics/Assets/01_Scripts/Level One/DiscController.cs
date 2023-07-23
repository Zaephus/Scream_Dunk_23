
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiscController : MonoBehaviour {

    private Rigidbody2D body;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private GameObject endMenu;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        body.simulated = false;
    }
    
    public void Throw(float _velocity) {
        body.simulated = true;
        body.velocity = transform.up * _velocity;
        transform.right = body.velocity.normalized;
        StartCoroutine(SetScore());
    }

    private IEnumerator SetScore() {

        Vector3 startPos = transform.position;

        scoreText.gameObject.SetActive(true);

        while(body.velocity.magnitude >= 0.075f) {
            scoreText.text = (transform.position.x - startPos.x).ToString("0.0");
            yield return new WaitForEndOfFrame();
        }

        endMenu.SetActive(true);
        scoreText.GetComponent<Animator>().SetTrigger("End");

    }
    
}