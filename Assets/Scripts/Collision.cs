using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Surface")) {

            Vector2 posOther = other.gameObject.transform.position;
            Vector2 scaleOther = other.gameObject.transform.lossyScale;
            Vector2 posMe = gameObject.transform.position;
            Vector2 scaleMe = gameObject.transform.lossyScale;
            if(((posMe.y - scaleMe.y/2)<(posOther.y+ scaleOther.y/2+0.1f)) && ((posMe.y - scaleMe.y / 2) > (posOther.y + scaleOther.y / 2)- scaleMe.y) ){
                posMe.y = posOther.y + scaleOther.y/2 + scaleMe.y / 2;
            }

            
            if(((posMe.x- scaleMe.x / 2) <(posOther.x + scaleOther.x / 2 + 0.1f)) && ((posMe.x - scaleMe.x / 2) > (posOther.x + scaleOther.x / 2)- scaleMe.x)) {
                print("left");
                posMe.x = posOther.x + scaleOther.x / 2 + scaleMe.x / 2;
                gameObject.GetComponent<MoverScript>().setCanGoLeft(false);
            }


            if (((posMe.x + scaleMe.x / 2) > (posOther.x - scaleOther.x / 2 - 0.1f)) && ((posMe.x + scaleMe.x / 2) < (posOther.x - scaleOther.x / 2)+ scaleMe.x)) {
                print("right");
                posMe.x = posOther.x - scaleOther.x / 2 - scaleMe.x / 2;
                gameObject.GetComponent<MoverScript>().setCanGoRight(false);
            }

            /*
            if (((posMe.x - scaleMe.x / 2) < (posOther.x + scaleOther.x / 2)) && ((posMe.x - scaleMe.x / 2) > (posOther.x + scaleOther.x / 2)-0.2f)) {
                print("right");
                //posMe.x  = posOther.x + scaleOther.x / 2 + scaleMe.x / 2;
            }
            
            if (((posMe.x + scaleMe.x / 2) > (posOther.x - scaleOther.x / 2)) && ((posMe.x + scaleMe.x / 2) < (posOther.x - scaleOther.x / 2)+0.2f)) {
                print("left");
            }*/

            gameObject.GetComponent<GravityManager>().touchSurface();
            gameObject.transform.position = posMe;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Surface")) {
            gameObject.GetComponent<GravityManager>().touchSurface();

           

        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Surface")) {
            
            gameObject.GetComponent<GravityManager>().leaveSurface();
            
        }
    }
}
