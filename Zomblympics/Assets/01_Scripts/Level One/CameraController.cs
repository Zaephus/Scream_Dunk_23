
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField, Range(1.0f, 2.0f)]
    private float followSpeedModifier = 1.0f;

    [SerializeField]
    private float beforeFollowWaitTime = 0.5f;

    private DiscController disc;

    public void StartFollowingDisc(DiscController _disc) {
        disc = _disc;
        StartCoroutine(FollowDisc());
    }

    private IEnumerator FollowDisc() {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, disc.transform.position.z);

        Rigidbody2D discBody = disc.GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(beforeFollowWaitTime);

        while(Vector3.Distance(pos, disc.transform.position) > 0.0f) {
            pos = Vector3.MoveTowards(pos, disc.transform.position, discBody.velocity.magnitude * followSpeedModifier * Time.deltaTime);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            yield return new WaitForFixedUpdate();
        }

        transform.parent = disc.transform;
    }
    
}