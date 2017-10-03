using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    public float G;
    Vector2 speedVector;
    

    bool isTouchingSurface;

    

	// Use this for initialization
	void Start () {
        speedVector = new Vector2(0,0);
        isTouchingSurface = false;
    }

    public void jump() {
        if (isTouchingSurface) {
            speedVector.y = 5;
        }
    }

    public void touchSurface() {
        speedVector.y = 0;
        isTouchingSurface = true;
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
