using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour {

    public InputManager inputManager;
    public GravityManager gravityManager;
    public PublicVariables publicVariables;
    public Collision collision;

    public float speedFloorFactor;

    public float waitTime = 1f;
    bool canGoLeft;
    float waitForRight = 0f;
    bool canGoRight;
    float waitForLeft = 0f;

    bool isWallJumping;
    Vector2 vectorWallJumping;
    float timerWallJumping;
    public float timeWallJumpingLimit;
    Vector2 lastSpeed;
    Vector2 lastPos;
    Vector2 lastPos2;
    Vector2 lastPos3;
    bool isOnAir;
    public Vector2 getLastSpeed() {
        return lastSpeed;
    }

    public Vector2 getLastPos() {
        return lastPos3;
    }

    public void leaveSurface() {
        isOnAir = true;
    }

    public void setOnFloor() {
        isOnAir = false;
    }

    public void walljump() {
        
        
        if(!canGoLeft) {
            
            isWallJumping = true;
            vectorWallJumping = new Vector2(+publicVariables.sprintSpeed, 0);
            timerWallJumping = 0;
            setCanGoLeft(true);
        }
        if (!canGoRight) {
            
            isWallJumping = true;
            vectorWallJumping = new Vector2(-publicVariables.sprintSpeed, 0);
            timerWallJumping = 0;
            setCanGoRight(true);
        }
    }




    public void setCanGoLeft(bool value) {
        canGoLeft = value;
        
        if (!value) {
            if (!Input.GetButton("Horizontal")) {
                Input.ResetInputAxes();
            }
           
            timerWallJumping = timeWallJumpingLimit;
            waitForRight = waitTime;
        }

    }

    public void setCanGoRight(bool value) {
        canGoRight = value;
        
        if (!value) {
            if (!Input.GetButton("Horizontal")) {
                Input.ResetInputAxes();
            }
            timerWallJumping = timeWallJumpingLimit;
            waitForLeft = waitTime;
        }
    }

    public void Start() {
        canGoLeft = true;
        canGoRight = true;
        isWallJumping = false;
        lastSpeed = new Vector2();
        lastPos = new Vector2();
        lastPos2 = new Vector2();
        lastPos3 = new Vector2();
        isOnAir = true;
    }

    

    // Update is called once per frame
    void Update () {

        Vector2 pos = gameObject.transform.position;

        //Save the last 3 positions
        lastPos3 = lastPos2;
        lastPos2 = lastPos;
        lastPos = pos;

        //récupère la vitesse des différentes forces
        Vector2 speed = new Vector2(0, 0);
        speed += inputManager.getSpeedVector();
        speed += gravityManager.getSpeedVector();

        //Cas où l'on est en WallJump
        if (isWallJumping) {
            timerWallJumping += Time.deltaTime;
            if (timerWallJumping > timeWallJumpingLimit) {
                isWallJumping = false;
            } else {

                speed.x = vectorWallJumping.x * (timeWallJumpingLimit - timerWallJumping) / timeWallJumpingLimit
                    + speed.x * (timerWallJumping) / timeWallJumpingLimit;
            }
        }

        /*
        if (speed.y > 0 && speed.x == 0) {
            speed.y = 0;
        }*/

        //Annulation des vitesses verticales si l'on est bloqué
        if (!canGoLeft && speed.x < 0) {
            speed.x = 0;
        }
        if (!canGoLeft && speed.x > 0) {
            if(collision.numberSurfaceContact==1) {
                waitForRight -= Time.deltaTime;
                if (waitForRight < 0) {
                    setCanGoLeft(true);
                } else {
                    speed.x = 0;
                }
            } else {
                setCanGoLeft(true);
            }
            
            
        } else {
            waitForRight = waitTime;
        }
        if (!canGoRight && speed.x > 0) {
            speed.x = 0;
        }
        if (!canGoRight && speed.x < 0) {
            if (collision.numberSurfaceContact == 1) {
                waitForLeft -= Time.deltaTime;
                if (waitForLeft < 0) {
                    setCanGoRight(true);
                } else {
                    speed.x = 0;
                }
            } else {
                setCanGoRight(true);
            }
            
        }

        if(!isOnAir) {
            speed.x = speed.x * speedFloorFactor;
        }

        

        //Nouvelle position du cube
        pos += speed * Time.deltaTime;
        lastSpeed = speed; // Save last speed
        gameObject.transform.position = pos;
        

    }
}
