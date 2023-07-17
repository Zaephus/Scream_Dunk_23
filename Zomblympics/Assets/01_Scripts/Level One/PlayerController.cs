
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelOne {

    public class PlayerController : MonoBehaviour {
        
        private void Start() {
            
        }

        private void Update() {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            transform.right = (mousePos - transform.position).normalized;
            
        }
        
    }

}