
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

        private Rigidbody2D body;

        [SerializeField]
        private DiscController disc;

        [SerializeField]
        private CameraController cameraController;

        private bool isReleased = false;
        
        private void Start() {
            body = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            if(!isReleased) {
                if(Input.GetMouseButton(0)) {
                    RotatePlayer();
                }
                if((Mathf.Abs(body.angularVelocity) >= minAngularVelocity && Input.GetMouseButtonUp(0)) || Mathf.Abs(body.angularVelocity) >= maxAngularVelocity) {
                    isReleased = true;
                    ReleaseDisc();
                }
            }
        }

        private void ReleaseDisc() {
            Debug.Log(body.angularVelocity);
            disc.transform.parent = transform.parent;
            disc.Throw(body.angularVelocity * Mathf.Deg2Rad);

            cameraController.StartFollowingDisc(disc);

            body.angularDrag = afterThrowAngularDrag;
        }

        private void RotatePlayer()  {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            Vector3 targetDir = (mousePos - transform.position).normalized;

            rateOfChange = Mathf.Deg2Rad * Vector3.SignedAngle(targetDir, transform.right, -Vector3.forward);

            body.AddTorque(rateOfChange * rotateForceModifier * Time.deltaTime, ForceMode2D.Force);

        }
        
    }

}