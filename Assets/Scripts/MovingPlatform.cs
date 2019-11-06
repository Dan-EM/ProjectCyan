using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    // this is for left and right, adjust for up and down later
    private float direction, moveSpeed = 3f;
    private bool moveRight = true;

    // Update is called once per frame
    void Update () {

        if(transform.position.x > 3f)
        {
            moveRight = false;
        }
        if (transform.position.x < -3f)
        {
            moveRight = true;
        }

        if(moveRight == true)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else if(moveRight == false)
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }

    }
}
