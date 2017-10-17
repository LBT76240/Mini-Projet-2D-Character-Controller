using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

    public Collider2D playerCollider;

    public MoverScript moverScript;

    Collider2D lastcollider;

    int numberSurfaceContact;

    private void Start() {
        numberSurfaceContact = 0;
        lastcollider = null;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Surface") || other.CompareTag("Crossable Surface")) {

            

            //Recupére la dernière vitesse et dernière position
            Vector2 lastSpeed = moverScript.getLastSpeed();
            Vector2 lastPos = moverScript.getLastPos();
            
            numberSurfaceContact++;
            
            //Recuperation de la position et taille de l'objet et du mur
            Vector2 posOther = other.transform.position;
            Vector2 scaleOther = other.transform.lossyScale;
            Vector2 posMe = playerCollider.transform.position;
            Vector2 scaleMe = playerCollider.transform.lossyScale;

            
            
            //Test de quel coté du cube on a touché
            if((lastPos.x - scaleMe.x / 2 > posOther.x + scaleOther.x / 2) && (lastSpeed.x<0)) {
                //print("gauche");
                
                posMe.x = posOther.x + scaleOther.x / 2 + scaleMe.x / 2;
                gameObject.GetComponent<MoverScript>().setCanGoLeft(false);
                gameObject.GetComponent<GravityManager>().setSliding();
                
                if (lastcollider == other) {
                    gameObject.GetComponent<GravityManager>().touchSurface(true);
                } else {
                    gameObject.GetComponent<GravityManager>().touchSurface(false);
                }
                lastcollider = other;
                
                
                gameObject.transform.position = posMe;
            }

            if ((lastPos.x + scaleMe.x / 2 < posOther.x - scaleOther.x / 2) && (lastSpeed.x > 0)) {
                //print("droit");
                
                posMe.x = posOther.x - scaleOther.x / 2 - scaleMe.x / 2;
                gameObject.GetComponent<MoverScript>().setCanGoRight(false);
                gameObject.GetComponent<GravityManager>().setSliding();

                if (lastcollider == other) {
                    gameObject.GetComponent<GravityManager>().touchSurface(true);
                } else {
                    gameObject.GetComponent<GravityManager>().touchSurface(false);
                }
                lastcollider = other;
               
                gameObject.transform.position = posMe;
            }

            if ((lastPos.y - scaleMe.y / 2 > posOther.y + scaleOther.y / 2) && (lastSpeed.y < 0)) {
                //print("bas");
                posMe.y = posOther.y + scaleOther.y / 2 + scaleMe.y / 2;
                gameObject.GetComponent<GravityManager>().setOnFloor();
                gameObject.GetComponent<MoverScript>().setOnFloor();
                lastcollider = null;
                //Repositionner le cube et annuler la gravité
                gameObject.GetComponent<GravityManager>().touchSurface(false);
                gameObject.transform.position = posMe;
                if (other.CompareTag("Crossable Surface")) {
                    gameObject.GetComponent<GravityManager>().setOnCrossableFloor();
                }
            }

            if (!other.CompareTag("Crossable Surface")) {
                if ((lastPos.y + scaleMe.y / 2 < posOther.y - scaleOther.y / 2) && (lastSpeed.y > 0)) {
                    //print("haut");
                    posMe.y = posOther.y - scaleOther.y / 2 - scaleMe.y / 2;
                    gameObject.GetComponent<GravityManager>().touchCeiling();
                    gameObject.transform.position = posMe;
                    lastcollider = null;
                }


            }
            
            

        }

        if(other.CompareTag("Finish")) {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Surface") || other.CompareTag("Crossable Surface")) {
            numberSurfaceContact--;
            
            
            if(numberSurfaceContact==0) {
                gameObject.GetComponent<GravityManager>().leaveSurface();
                gameObject.GetComponent<MoverScript>().leaveSurface();
                gameObject.GetComponent<MoverScript>().setCanGoRight(true);
                gameObject.GetComponent<MoverScript>().setCanGoLeft(true);
            }
        }
    }
}
