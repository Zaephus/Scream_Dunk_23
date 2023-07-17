
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelOne {

    public class PlayerController : MonoBehaviour {

        [SerializeField, ReadOnly]
        private float rateOfChange = 0.0f;
        [SerializeField]
        private float rotateForceModifier = 10.0f;

        public Vector2 force;

        [SerializeField]
        private GameObject disc;

        private Rigidbody2D body;
        private FixedJoint2D joint;
        
        private void Start() {
            body = GetComponent<Rigidbody2D>();
            joint = GetComponent<FixedJoint2D>();
        }

        private void Update() {
            RotatePlayer();
            if(joint != null) {
                force = joint.reactionForce;
                Debug.Log(force.magnitude);
            }

            if(Input.GetMouseButtonDown(0)) {
                joint.enabled = false;
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