using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour {

    public InputManager inputManager;
    public GravityManager gravityManager;
    public PublicVariables publicVariables;
    public Collision collision;

    public float speedFloorFactor;

    public float timeStickOnWall = 0.4f;
    bool canGoLeft;
    float waitForRight = 0f;
    bool canGoRight;
    float waitForLeft = 0f;

    bool isWallJumping;
    Vector2 vectorWallJumping;
    float timerWallJumping;
    float timeWallJumpingLimit = 1f;
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

    //Fonction pour lancer un walljump
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



    //Fonction pour indiquer qu'on est bloqué à gauche ou à droite
    public void setCanGoLeft(bool value) {
        canGoLeft = value;
        
        if (!value) {
            if (!Input.GetButton("Horizontal")) {
                Input.ResetInputAxes();
            }
           
            timerWallJumping = timeWallJumpingLimit;
            waitForRight = timeStickOnWall;
        }

    }

    public void setCanGoRight(bool value) {
        canGoRight = value;
        
        if (!value) {
            if (!Input.GetButton("Horizontal")) {
                Input.ResetInputAxes();
            }
            timerWallJumping = timeWallJumpingLimit;
            waitForLeft = timeStickOnWall;
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

        
        //On applique le facteur de vitesse quand on est sur le sol
        if (!isOnAir) {
            speed.x = speed.x * speedFloorFactor;
        }

        //Annulation des vitesses verticales si l'on est bloqué
        if (!canGoLeft && speed.x < 0) {
            speed.x = 0;
        }
        if (!canGoLeft && speed.x > 0) {
            if(collision.getNumberSurfaceContact()== 1) {
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
            waitForRight = timeStickOnWall;
        }
        if (!canGoRight && speed.x > 0) {
            speed.x = 0;
        }
        if (!canGoRight && speed.x < 0) {
            if (collision.getNumberSurfaceContact() == 1) {
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

        

        

        //Nouvelle position du cube
        pos += speed * Time.deltaTime;
        lastSpeed = speed; // Save last speed
        gameObject.transform.position = pos;
        

    }
}
