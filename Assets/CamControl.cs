using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

    public float scrollSpeed = 100;
    public float zoomSpeed = 1000;
    public float rotateSpeed = 200;
    public float downPan = 10;
    public float forwardPan = 2;
    public float cameraAngle = 60;
    
    public float minY = -90.0f;
    public float maxY = 90.0f;
    
    float rotationY = 0.0f;
    float rotationX = 0.0f;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void LateUpdate() {
        /* todo list
         * 1. add scroll boundaris to level size
         * 2. add zoom levels with pan
         */

        //declare variables
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //scroll using arrows or wasd
        transform.position += (Vector3.forward * vertical + Vector3.right * horizontal) * Time.deltaTime * scrollSpeed;

        //zoom using mousewheel scroll
        if (!Input.GetMouseButton(2)) //check if mousewheel is not clicked
        {
            if (mouseWheel != 0)
            {
                //zoom up or down
                transform.position += ((Vector3.down * downPan) + (Vector3.forward * forwardPan)) * mouseWheel * Time.deltaTime * zoomSpeed;
            }

            //scroll on edge of screen
            if (Input.mousePosition.x >= Screen.width - 20)
            {
                //scroll right
                transform.position += Vector3.right * Time.deltaTime * scrollSpeed;
            }
            else if (Input.mousePosition.x <= 20 && transform.position.x > 20)
            {
                //scroll left
                transform.position += Vector3.left * Time.deltaTime * scrollSpeed;
            }

            if (Input.mousePosition.y >= Screen.height - 20 && transform.position.y > 20)
            {
                //scroll forward
                transform.position += Vector3.forward * Time.deltaTime * scrollSpeed;
            }
            else if (Input.mousePosition.y <= 20)
            {
                //scroll back
                transform.position += Vector3.back * Time.deltaTime * scrollSpeed;
            }
        }
        
        //rotate with middle mouse
        if (Input.GetMouseButton(2))
        {
            rotationX += mouseX * Time.deltaTime * rotateSpeed;
            transform.localEulerAngles = new Vector3(cameraAngle, rotationX, 0);
        }
    }
}