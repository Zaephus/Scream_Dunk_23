
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField, Range(1.0f, 2.0f)]
    private float followSpeedModifier = 1.0f;
    [SerializeField]
    private float zoomModifier;

    [SerializeField]
    private float beforeFollowWaitTime = 0.5f;

    private DiscController disc;

    private Camera cam;

    private void Start() {
        cam = GetComponent<Camera>();
    }

    public void StartFollowingDisc(DiscController _disc) {
        disc = _disc;
        StartCoroutine(FollowDisc());
    }

    private IEnumerator FollowDisc() {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, disc.transform.position.z);
        Vector3 startPos = pos;

        float discSpeed = disc.GetComponent<Rigidbody2D>().velocity.magnitude;

        float startZoom = cam.orthographicSize;

        yield return new WaitForSeconds(beforeFollowWaitTime);

        while(disc.GetComponent<Rigidbody2D>().velocity.magnitude > 0.0f) {

            if(Vector3.Distance(pos, disc.transform.position) > 0.1f) {
                pos = Vector3.MoveTowards(pos, disc.transform.position, discSpeed * followSpeedModifier * Time.deltaTime);
                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            }
            else {
                transform.parent = disc.transform;
            }

            cam.orthographicSize = startZoom + (Mathf.Clamp(pos.x - startPos.x, 0.0f, Mathf.Infinity) * zoomModifier);
            
            yield return new WaitForFixedUpdate();

        }

    }
    
}