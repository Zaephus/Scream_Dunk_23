
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelOne {

    public class PlayerController : MonoBehaviour {

        [SerializeField, ReadOnly]
        private float rateOfChange = 0.0f;
        [SerializeField]
        private float rotateForceModifier = 10.0f;

        [SerializeField]
        private float afterThrowAngularDrag;
        [SerializeField]
        private float minAngularVelocity;
        [SerializeField]
        private float maxAngularVelocity;

        [SerializeField]
        private Slider forceSlider;
        [SerializeField]
        private float sliderTurnOffWaitTime;

        private Rigidbody2D body;

        [SerializeField]
        private Transform lowerArms;

        [SerializeField]
        private DiscController disc;

        [SerializeField]
        private CameraController cameraController;

        private bool isReleased = false;
        
        private void Start() {
            body = GetComponent<Rigidbody2D>();
            forceSlider.gameObject.SetActive(true);
        }

        private void Update() {
            if(!isReleased) {
                if(Input.GetMouseButton(0)) {
                    RotatePlayer();
                }

                if((Mathf.Abs(body.angularVelocity) >= minAngularVelocity && Input.GetMouseButtonUp(0))) {
                    isReleased = true;
                    ReleaseDisc();
                }
                else if(Mathf.Abs(body.angularVelocity) >= maxAngularVelocity) {
                    isReleased = true;
                    LoseDisc();
                }
            }
        }

        private void ReleaseDisc() {
            disc.transform.parent = transform.parent;
            disc.Throw(body.angularVelocity * Mathf.Deg2Rad);

            cameraController.StartFollowingDisc(disc);

            body.angularDrag = afterThrowAngularDrag;

            StartCoroutine(TurnOffSlider());
        }

        // When you turn too hard, your arms tear off.
        private void LoseDisc() {
            lowerArms.parent = disc.transform;

            disc.transform.parent = transform.parent;
            disc.Throw(body.angularVelocity * Mathf.Deg2Rad);

            cameraController.StartFollowingDisc(disc);

            body.angularDrag = afterThrowAngularDrag;

            StartCoroutine(TurnOffSlider());
        }

        private void RotatePlayer()  {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            Vector3 targetDir = (mousePos - transform.position).normalized;

            rateOfChange = Mathf.Deg2Rad * Vector3.SignedAngle(targetDir, transform.right, -Vector3.forward);

            body.AddTorque(rateOfChange * rotateForceModifier * Time.deltaTime, ForceMode2D.Force);

            forceSlider.value = (Mathf.Abs(body.angularVelocity) - minAngularVelocity) / (((maxAngularVelocity - minAngularVelocity) / 7.0f) * 7.2f);

        }

        private IEnumerator TurnOffSlider() {
            yield return new WaitForSeconds(sliderTurnOffWaitTime);
            forceSlider.gameObject.SetActive(false);
        }
        
    }

}