using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVariables : MonoBehaviour {

    public float speed;
    public float sprintSpeed;

    public float userSpeed;

    bool isSprinting;
   

    public void Start() {
        userSpeed = speed;
        isSprinting = false;
    }

    public void setSprint(bool value) {
        if(value!= isSprinting) {
            isSprinting = value;
            if (value) {
                userSpeed = sprintSpeed;
            } else {
                userSpeed = speed;
            }
        }
        
    }
	
}
