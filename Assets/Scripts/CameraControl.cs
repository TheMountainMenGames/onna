using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the camera rotation, zoom and scroll

public class CameraControl : MonoBehaviour {
    /* class todo list
         * 1. add scroll boundaris to level size
         * 2. add zoom levels with pan
         * 3. add max and min zoom
         * 4. change rotate with middle mouse to on centre screen
         * 5. dont scroll when mouse outside of game screen
         */

    //declare variables
    public int scrollSpeed;
    public int zoomSpeed;
    public int rotateSpeed;
    public int forwardPan;
    public int pixelBoundary;
    public float maxZoomOut;
    public float maxZoomIn;

    private LevelControl levelControl;

    private Vector3 cameraDirection;
    private Vector3 cameraPosition;

    private float horizontal; 
    private float vertical; 
    private float mouseWheel;
    private float rotationX;
    private float rotationY;
    private float mouseX;
    private float mouseY;
    private float mousePosX;
    private float mousePosY;
    
    void Awake()
    {
        levelControl = FindObjectOfType<LevelControl>();
        this.InitCamControl();
    }

    // Use this for initialization
    void Start () {
               
    }

    void Update() {
        print(cameraPosition);
    }

    // Update is called once per frame
    void LateUpdate() {
        this.UpdateInput();
        this.Scroll();
        this.Zoom();
        this.Rotate();
    }

    //initialize public variable values
    private void InitCamControl() 
    {
        this.scrollSpeed = 200;
        this.zoomSpeed = 100;
        this.rotateSpeed = 200;
        this.forwardPan = 5;
        this.maxZoomIn = 20;
        this.maxZoomOut = 100;
        this.pixelBoundary = 20;
    }

    //update Input variables
    private void UpdateInput()
    {
        this.horizontal = Input.GetAxisRaw("Horizontal");
        this.vertical = Input.GetAxisRaw("Vertical");
        this.mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        this.mouseX = Input.GetAxis("Mouse X");
        this.mouseY = Input.GetAxis("Mouse Y");
        this.mousePosX = Input.mousePosition.x;
        this.mousePosY = Input.mousePosition.y;
    }

    //scroll on keypress or edge of screen
    private void Scroll()
    {
        //scroll right
        if ((this.mousePosX >= Screen.width - this.pixelBoundary || this.horizontal > 0) && transform.position.x < this.levelControl.mapSizeX)
        {
            //transform.Translate(Vector3.right * this.horizontal * Time.deltaTime * this.scrollSpeed);
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * Time.deltaTime * this.scrollSpeed, 1);
        }
        //scroll left
        else if ((this.mousePosX <= this.pixelBoundary || this.horizontal < 0) && transform.position.x > 0)
        {
            transform.Translate(Vector3.left * -(this.horizontal) * Time.deltaTime * this.scrollSpeed);
        }

        //scroll forward
        if ((this.mousePosY >= Screen.height - this.pixelBoundary || this.vertical > 0) && transform.position.z < this.levelControl.mapSizeZ)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * this.scrollSpeed);
        }
        //scroll back
        else if ((this.mousePosY <= this.pixelBoundary || this.vertical < 0) && transform.position.z > 0)
        {
            transform.Translate(Vector3.back * Time.deltaTime * this.scrollSpeed);
        }

        Debug.Log("Mouse X: " + this.mouseX);
    }

    //zoom using mousewheel scroll
    private void Zoom()
    {
        // zoom in
        if (this.mouseWheel > 0 && transform.position.y > this.maxZoomIn)
        {
            transform.Translate(Vector3.down * Time.deltaTime * this.zoomSpeed);
        }
        //zoom out
        else if (this.mouseWheel < 0 && transform.position.y < this.maxZoomOut)
        {
            transform.Translate(Vector3.up * Time.deltaTime * this.zoomSpeed);
        }
    }

    //rotate with middle mouse
    private void Rotate()
    {
        if (Input.GetMouseButton(2))
        {
            //transform.localEulerAngles = new Vector3(transform.eulerAngles.x, this.rotationX, 0);
            RaycastHit raycastHit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            Physics.Raycast(ray, out raycastHit);

            
            Transform myTransform = Camera.main.transform;
            myTransform.position = Vector3.Lerp(raycastHit.point, new Vector3(0, -(this.mouseX), 0), 0.3f);
            myTransform.RotateAround(raycastHit.point, new Vector3(0, -(this.mouseX), 0), rotateSpeed * Time.deltaTime);
        }
    }   

}