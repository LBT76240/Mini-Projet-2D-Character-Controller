using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    public float G;
    Vector2 speedVector;
    

    bool isTouchingSurface;
    bool canDoubleJump;
    

	// Use this for initialization
	void Start () {
        speedVector = new Vector2(0,0);
        isTouchingSurface = false;
        canDoubleJump = false;
    }

    public void jump() {
        if (isTouchingSurface) {
            leaveSurface();
            gameObject.GetComponent<MoverScript>().walljump();
            speedVector.y = 5;
        } else if(canDoubleJump) {

            speedVector.y = 5;
            canDoubleJump = false;
        }
    }

    public void touchSurface() {
        speedVector.y = 0;
        isTouchingSurface = true;
        canDoubleJump = true;
    }
    public void leaveSurface() {
        isTouchingSurface = false;
    }
    public Vector2 getSpeed() {
        return speedVector;
    }

    // Update is called once per frame
    void Update () {
        if (!isTouchingSurface) {
            speedVector.y -= G * Time.deltaTime;
            if (speedVector.y <= -10) {
                speedVector.y = -10;
            }
        }
        
    }

    
}
