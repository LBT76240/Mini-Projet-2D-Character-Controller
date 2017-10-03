using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour {

    public InputManager inputManager;
    public GravityManager gravityManager;

    bool canGoLeft;
    bool canGoRight;

    public void setCanGoLeft(bool value) {
        canGoLeft = value;
    }

    public void setCanGoRight(bool value) {
        canGoRight = value;
    }

    public void Start() {
        canGoLeft = true;
        canGoRight = true;
    }

    // Update is called once per frame
    void FixedUpdate () {

        Vector2 pos = gameObject.transform.position;
        Vector2 speed = new Vector2(0, 0);
        speed += inputManager.getSpeed();
        speed += gravityManager.getSpeed();

        if(!canGoLeft && speed.x<0) {
            speed.x = 0;
        }

        if (!canGoLeft && speed.x > 0) {
            setCanGoLeft(true);
        }
        if (!canGoRight && speed.x > 0) {
            speed.x = 0;
        }
        if (!canGoRight && speed.x < 0) {
            setCanGoRight(true);
        }

        pos += speed * Time.deltaTime;

        gameObject.transform.position = pos;

    }
}
