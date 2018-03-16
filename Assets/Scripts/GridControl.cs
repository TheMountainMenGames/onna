using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour {
    private GameControl gameControl;
    private SelectionControl selectionControl;
    private float gridSize = 5f;
    // Use this for initialization
    void Start () {
        this.gameControl = FindObjectOfType<GameControl>();
        this.selectionControl = FindObjectOfType<SelectionControl>();
    }
	
	// Update is called once per frame
	void Update () {
        this.UpdateGridSize();
    }
    
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;
        Vector3 result = new Vector3((float)Mathf.RoundToInt(position.x / gridSize) * gridSize,(float)Mathf.RoundToInt(position.y / gridSize) * gridSize,(float)Mathf.RoundToInt(position.z / gridSize) * gridSize);
        result += transform.position;
        return result;
    }

    private void UpdateGridSize()
    { 
        if(gameControl.GetGameState() == 0)
        {
            this.gridSize = 5f;
            this.selectionControl.UpdateScaleBox();
        }
        else if(gameControl.GetGameState() == 1)
        {
            this.gridSize = 2.5f;
            this.selectionControl.UpdateScaleBox();
        }
    }
    public float GetGridSize()
    {
        return this.gridSize;
    }
}
