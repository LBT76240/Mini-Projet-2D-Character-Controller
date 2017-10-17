﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GravityManager gravityManager;
    public PublicVariables publicVariables;

    Vector2 speedVector;
    

    private void Start() {
        speedVector = new Vector2(0, 0);
    }

    
    public Vector2 getSpeedVector() {
        return speedVector;
    }

    // Update is called once per frame
    void Update () {

        speedVector.x = Input.GetAxis("Horizontal")* publicVariables.userSpeed;
        
        

        if (Input.GetButtonDown("Jump")) {
            if(Input.GetAxis("Vertical")<-0.8) {
                gravityManager.downJump();
            } else {
                gravityManager.jump();
            }
            
        }


        

        if(Input.GetAxis("Sprint")<-0.55) {
            publicVariables.setSprint(true);
        } else {
            publicVariables.setSprint(false);
        }

        if (Input.GetButtonDown("Sprint")) {
            publicVariables.setSprint(true);
        }
        if (Input.GetButtonUp("Sprint")) {
            publicVariables.setSprint(false);
        }


    }
}
