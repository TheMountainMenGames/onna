using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionControl : MonoBehaviour {
    public GameObject selectionBox;
    public bool selectStarted;
    public RaycastHit raycastHit;
    private GridControl gridControl;

    // Use this for initialization
    void Start ()
    {
        this.gridControl = FindObjectOfType<GridControl>();
        this.selectionBox = Instantiate(Resources.Load("Selector") as GameObject);
        this.selectionBox.SetActive(false);
        this.selectionBox.transform.localScale = new Vector3(this.gridControl.GetGridSize(), this.selectionBox.transform.localScale.y, this.gridControl.GetGridSize());
        this.selectStarted = false;
        this.UpdateScaleBox();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateCurrentRay();
        if (Input.GetMouseButtonDown(0) && !this.selectStarted)
        {
            this.StartSelect();
            this.selectionBox.SetActive(true);
            var newPosition = gridControl.GetNearestPointOnGrid(raycastHit.point);
            this.selectionBox.transform.position = newPosition;
        }
        else if(Input.GetMouseButtonDown(0) && this.selectStarted)
        {
            var newPosition = gridControl.GetNearestPointOnGrid(raycastHit.point);
            this.selectionBox.transform.position = newPosition;
        }
        
    }

    private void StartDragSelection()
    {

    }

    private void UpdateCurrentRay()
    {
        if (this.selectStarted)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out raycastHit);
        }
    }
    public void StartNewSelection()
    {
        this.StartSelect();
    }
    private void StartSelect()
    {
        this.selectStarted = true;
    }
    private void EndSelect()
    {
        this.selectStarted = false;
    }
    public void UpdateScaleBox()
    {
        this.selectionBox.transform.localScale = new Vector3(this.gridControl.GetGridSize(), this.selectionBox.transform.localScale.y, this.gridControl.GetGridSize());
    }
}