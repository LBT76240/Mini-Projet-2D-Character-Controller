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
            Vector2 lastPos = moverScript.getLastPos();
            
            numberSurfaceContact++;
            
            Vector2 posOther = other.transform.position;
            Vector2 scaleOther = other.transform.lossyScale;
            Vector2 posMe = playerCollider.transform.position;
            Vector2 scaleMe = playerCollider.transform.lossyScale;


            

            if((lastPos.x - scaleMe.x / 2 > posOther.x + scaleOther.x / 2) && (lastSpeed.x<0)) {
                print("gauche");
                posMe.x = posOther.x + scaleOther.x / 2 + scaleMe.x / 2;
                gameObject.GetComponent<MoverScript>().setCanGoLeft(false);
            }

            if ((lastPos.x + scaleMe.x / 2 < posOther.x - scaleOther.x / 2) && (lastSpeed.x > 0)) {
                print("droit");
                posMe.x = posOther.x - scaleOther.x / 2 - scaleMe.x / 2;
                gameObject.GetComponent<MoverScript>().setCanGoRight(false);
            }

            if ((lastPos.y - scaleMe.y / 2 > posOther.y + scaleOther.y / 2) && (lastSpeed.y < 0)) {
                print("bas");
                posMe.y = posOther.y + scaleOther.y / 2 + scaleMe.y / 2;
            }

            if ((lastPos.y + scaleMe.y / 2 < posOther.y - scaleOther.y / 2) && (lastSpeed.y > 0)) {
                print("haut");
                posMe.y = posOther.y - scaleOther.y / 2 - scaleMe.y / 2;
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
