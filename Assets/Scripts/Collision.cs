using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

    public Collider2D playerCollider;

    public MoverScript moverScript;

    int numberSurfaceContact;

    private void Start() {
        numberSurfaceContact = 0;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Surface")) {

            Vector2 lastSpeed = moverScript.getLastSpeed();

            print("SH : " + lastSpeed.x);
            print("SV : " + lastSpeed.y);

            Vector2 lastPos = moverScript.getLastPos();

            print("PH : " + lastPos.x);
            print("PV : " + lastPos.y);

            numberSurfaceContact++;
            
            Vector2 posOther = other.transform.position;
            Vector2 scaleOther = other.transform.lossyScale;
            Vector2 posMe = playerCollider.transform.position;
            Vector2 scaleMe = playerCollider.transform.lossyScale;
            if(((posMe.y - scaleMe.y/2)<(posOther.y+ scaleOther.y/2+0.1f)) && ((posMe.y - scaleMe.y / 2) > (posOther.y + scaleOther.y / 2)- scaleMe.y/10) ){
                posMe.y = posOther.y + scaleOther.y/2 + scaleMe.y / 2;
            }

            
            if(((posMe.x- scaleMe.x / 2) <(posOther.x + scaleOther.x / 2 + 0.1f)) && ((posMe.x - scaleMe.x / 2) > (posOther.x + scaleOther.x / 2)- scaleMe.x/10)) {
                
                posMe.x = posOther.x + scaleOther.x / 2 + scaleMe.x / 2;
                gameObject.GetComponent<MoverScript>().setCanGoLeft(false);
            }


            if (((posMe.x + scaleMe.x / 2) > (posOther.x - scaleOther.x / 2 - 0.1f)) && ((posMe.x + scaleMe.x / 2) < (posOther.x - scaleOther.x / 2)+ scaleMe.x/10)) {
                
                posMe.x = posOther.x - scaleOther.x / 2 - scaleMe.x / 2;
                gameObject.GetComponent<MoverScript>().setCanGoRight(false);
            }

           

            gameObject.GetComponent<GravityManager>().touchSurface();
            gameObject.transform.position = posMe;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Surface")) {
            numberSurfaceContact--;
            
            
            if(numberSurfaceContact==0) {
                gameObject.GetComponent<GravityManager>().leaveSurface();
                gameObject.GetComponent<MoverScript>().setCanGoRight(true);
                gameObject.GetComponent<MoverScript>().setCanGoLeft(true);
            }
        }
    }
}
