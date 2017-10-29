using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVariables : MonoBehaviour {


    //variable pour régler la vitesse du joueur
    public float speed;
    public float sprintSpeed;

    //La vitesse actuelle du joueur
    float userSpeed;

    bool isSprinting;
   
    public float getUserSpeed() {
        return userSpeed;
    }

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
