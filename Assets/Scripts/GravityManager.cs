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
    bool isOnCrossableFloor;

    // Use this for initialization
    void Start () {
        speedVector = new Vector2(0,0);
        isTouchingSurface = false;
        canDoubleJump = false;
        isSliding = false;
        isOnFloor = false;
        isOnCrossableFloor = false;
    }

    public void setSliding() {
        isSliding = true;
    }

    public void setOnFloor() {
        isOnFloor = true;
    }

    public void setOnCrossableFloor() {
        isOnCrossableFloor = true;
    }

    //Saut vers le bas pour passer à travers une platforme
    public void downJump() {
        if(isTouchingSurface && isOnCrossableFloor) {
            speedVector.y = -publicVariables.speed;
            isOnCrossableFloor = false;
        }
    }

    public void jump() {
        if (isTouchingSurface) {
            leaveSurface();
            gameObject.GetComponent<MoverScript>().walljump();
            speedVector.y = publicVariables.sprintSpeed;
        } else if(canDoubleJump) {

            speedVector.y = publicVariables.sprintSpeed;
            canDoubleJump = false;
        }
    }

    public void touchCeiling() {
        speedVector.y = 0;
        canDoubleJump = false;
    }

    public void touchSurface(bool preventWallJumpFromTheSameWall) {
        
        isTouchingSurface = true;
        if(!preventWallJumpFromTheSameWall) {
            canDoubleJump = true;
        }
        
    }

    public  void touchFloor() {
        speedVector.y = 0;
    }

    public void leaveSurface() {
        isTouchingSurface = false;
        isSliding = false;
        isOnFloor = false;
        isOnCrossableFloor = false;
    }
    public Vector2 getSpeedVector() {
        return speedVector;
    }

    // Update is called once per frame
    void Update () {
        if (!isTouchingSurface) {
            speedVector.y -= G * Time.deltaTime;
            if (speedVector.y <= -publicVariables.sprintSpeed) {
                speedVector.y = -publicVariables.sprintSpeed;
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
