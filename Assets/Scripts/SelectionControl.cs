using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionControl : MonoBehaviour {
    public GameObject Selector;
    public GameObject selectionBox;
    public bool selectStarted;
    public RaycastHit raycastHit;
    private GridControl gridControl;
    public List<GameObject> spawnClone;

    // Use this for initialization
    void Start ()
    {
        // other controllers
        this.gridControl = FindObjectOfType<GridControl>();

        // set inititals
        this.spawnClone = new List<GameObject>();
        this.Selector = Resources.Load("Selector") as GameObject;
        this.selectionBox = Instantiate(this.Selector);
        this.selectionBox.GetComponent<Selector>().MakeSelectionBox(gameObject.transform);        
        this.selectStarted = false;        
        print("SelectionControl Key Escape to cancel selection");
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.MoveBoxByMouse();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.EndSelect();
        }
        if (Input.GetMouseButtonDown(0) && !this.selectStarted)
        {
            this.StartNewSelection();            
        }
        else if(Input.GetMouseButtonDown(0) && this.selectStarted)
        {
            this.UpdateSelect();            
        }        
    }

    // place selection box(s)
    private void PlaceBoxSelection()
    {
        var temp = Instantiate(this.Selector);
        temp.GetComponent<Selector>().MakeSelection(gameObject.transform);
        temp.transform.position = gridControl.GetNearestPointOnGrid(this.selectionBox.transform.position);
        temp.transform.localScale = new Vector3(this.gridControl.GetGridSize(), this.selectionBox.transform.localScale.y, this.gridControl.GetGridSize());
        this.spawnClone.Add(temp);
    }
    // get curent ray
    private void UpdateCurrentRay()
    {
        if (this.selectStarted)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out raycastHit);
        }
    }
    // start of a selection
    public void StartNewSelection()
    {
        this.selectStarted = true;
        this.UpdateSelect();
        this.selectionBox.SetActive(true);        
    }

    private void UpdateSelect()
    {
        this.UpdateCurrentRay();
        this.selectionBox.transform.position = gridControl.GetNearestPointOnGrid(this.raycastHit.point);
        this.PlaceBoxSelection();
    }
    // clears all selected items
    private void EndSelect()
    {
        this.selectStarted = false;
        this.selectionBox.SetActive(false);
        foreach (GameObject o in spawnClone)
        {
            Destroy(o);
        }
    }

    public void UpdateScaleBox()
    {
        this.selectionBox.transform.localScale = new Vector3(this.gridControl.GetGridSize(), this.selectionBox.transform.localScale.y, this.gridControl.GetGridSize());
        //set object to new position
        this.selectionBox.transform.position = gridControl.GetNearestPointOnGrid(this.selectionBox.transform.position);
    }

    private void MoveBoxByMouse()
    {
        if (this.selectStarted) { 
            this.UpdateCurrentRay();
            this.MoveBox(this.raycastHit.point);
        }
    }
    private void MoveBox(Vector3 clickPoint)
    {
        this.selectionBox.transform.position = gridControl.GetNearestPointOnGrid(clickPoint);
    }
}