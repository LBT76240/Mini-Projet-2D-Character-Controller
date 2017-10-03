using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GravityManager gravityManager;

    public float speed;
    Vector2 speedVector;

    private void Start() {
        speedVector = new Vector2(0, 0);
    }

    
    public Vector2 getSpeed() {
        return speedVector;
    }

    // Update is called once per frame
    void Update () {

        speedVector.x = Input.GetAxis("Horizontal")*speed;

        if(Input.GetButtonDown("Jump")) {
            gravityManager.jump();
        }

        

        

       
    }
}
