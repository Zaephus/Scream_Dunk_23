
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
        private float releaseForce = 45.0f;

        [SerializeField]
        private Slider forceSlider;

        private Rigidbody2D body;
        private FixedJoint2D joint;
        
        private void Start() {
            body = GetComponent<Rigidbody2D>();
            joint = GetComponent<FixedJoint2D>();
            joint.breakForce = releaseForce;
        }

        private void Update() {

            if(joint != null && joint.isActiveAndEnabled) {
                forceSlider.value = joint.reactionForce.magnitude / ((releaseForce / 7) * 8);
                RotatePlayer();
            }

            if(Input.GetMouseButtonDown(0)) {
                joint.enabled = false;
                forceSlider.value = 0.0f;
            }
        }

        private void RotatePlayer()  {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            Vector3 targetDir = (mousePos - transform.position).normalized;

            rateOfChange = Mathf.Deg2Rad * Vector3.SignedAngle(targetDir, transform.right, -Vector3.forward);

            body.AddTorque(rateOfChange * rotateForceModifier * Time.deltaTime, ForceMode2D.Force);

        }
        
    }

}