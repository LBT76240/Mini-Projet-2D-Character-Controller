using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    public float G;
    Vector2 speedVector;
    public PublicVariables publicVariables;

    bool isTouchingSurface;
    bool canDoubleJump;
    bool isSliding;
    bool isOnFloor;


    // Use this for initialization
    void Start () {
        speedVector = new Vector2(0,0);
        isTouchingSurface = false;
        canDoubleJump = false;
        isSliding = false;
        isOnFloor = false;
    }

    public void setSliding() {
        isSliding = true;
    }

    public void setOnFloor() {
        isOnFloor = true;
    }

    public void jump() {
        if (isTouchingSurface) {
            leaveSurface();
            gameObject.GetComponent<MoverScript>().walljump();
            speedVector.y = publicVariables.sprintSpeed/2;
        } else if(canDoubleJump) {

            speedVector.y = publicVariables.sprintSpeed / 2;
            canDoubleJump = false;
        }
    }

    public void touchCeiling() {
        speedVector.y = 0;
        canDoubleJump = false;
    }

    public void touchSurface(bool preventWallJumpFromTheSameWall) {
        speedVector.y = 0;
        isTouchingSurface = true;
        if(!preventWallJumpFromTheSameWall) {
            canDoubleJump = true;
        }
        
    }
    public void leaveSurface() {
        isTouchingSurface = false;
        isSliding = false;
        isOnFloor = false;
    }
    public Vector2 getSpeedVector() {
        return speedVector;
    }

    // Update is called once per frame
    void Update () {
        if (!isTouchingSurface) {
            speedVector.y -= G * Time.deltaTime;
            if (speedVector.y <= -publicVariables.speed) {
                speedVector.y = -publicVariables.speed;
            }
        } else if (isSliding){
            if (!isOnFloor) {
                speedVector.y -= G * Time.deltaTime;
                if (speedVector.y <= -publicVariables.speed / 5) {
                    speedVector.y = -publicVariables.speed / 5;
                }
            }
        }
        
    }

    
}
