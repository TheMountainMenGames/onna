using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the camera rotation, zoom and scroll

public class CamControl : MonoBehaviour {

    //declare variables
    public float scrollSpeed = 100;
    public float zoomSpeed = 1000;
    public float rotateSpeed = 200;
    public float forwardPan = 2;
    public float cameraAngle = 60;
    public float maxzoomout = 1;
    public float maxzoomin = 1;

    private float rotationX = 0;
    
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void LateUpdate() {
        /* todo list
         * 1. add scroll boundaris to level size
         * 2. add zoom levels with pan
         * 3. add max and min zoom
         */
        
        //Debug.Log(Vector3.up);

        //declare variables
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //define cameradirection
        Vector3 cameraDirection = new Vector3(horizontal, 0f, vertical);
        cameraDirection = Camera.main.transform.TransformDirection(cameraDirection);
        cameraDirection.y = 0.0f;
                
        //scroll using arrows or wasd
        transform.position += cameraDirection * Time.deltaTime * scrollSpeed;

        //zoom using mousewheel scroll
        if (!Input.GetMouseButton(2)) //check if mousewheel is not clicked
        {
            if (mouseWheel != 0)
            {
                //zoom up or down
                transform.position += ((Vector3.down) + (Vector3.forward * forwardPan)) * mouseWheel * Time.deltaTime * zoomSpeed;
            }

            //scroll on edge of screen
            if (Input.mousePosition.x >= Screen.width - 20)
            {
                //scroll right
                transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed);
            }
            else if (Input.mousePosition.x <= 20 && transform.position.x > 20)
            {
                //scroll left
                transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed);
            }

            if (Input.mousePosition.y >= Screen.height - 20 && transform.position.y > 20)
            {
                //scroll forward
                transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed);
            }
            else if (Input.mousePosition.y <= 20)
            {
                //scroll back
                transform.Translate(Vector3.back * Time.deltaTime * scrollSpeed);
            }
        }
        
        //rotate with middle mouse
        if (Input.GetMouseButton(2))
        {
            rotationX += mouseX * Time.deltaTime * rotateSpeed;
            transform.localEulerAngles = new Vector3(transform.eulerAngles.x, rotationX, 0);
        }      
    }
}