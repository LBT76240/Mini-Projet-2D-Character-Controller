using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVariables : MonoBehaviour {

    public float speed;
    public float sprintSpeed;

    public float userSpeed;
   

    public void Start() {
        userSpeed = speed;
    }

    public void setSprint(bool value) {
        if(value) {
            userSpeed = sprintSpeed;
        } else {
            userSpeed = speed;
        }
    }
	
}
