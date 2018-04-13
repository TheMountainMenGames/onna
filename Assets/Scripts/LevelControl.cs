using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour {

    public int mapSizeX;
    public int mapSizeZ;

    void Awake()
    {
        this.InitLevelControl();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitLevelControl()
    {
        this.mapSizeX = 200;
        this.mapSizeZ = 200;
    }
    //get-set functions for Map Size Y
    public int GetMapSizeX()
    {
        return this.mapSizeX;
    }
    public void GetMapSizeX(int mapSizeX)
    {
        this.mapSizeX = mapSizeX;
    }

    //get-set functions for Map Size X
    public int GetMapSizeZ()
    {
        return this.mapSizeZ;
    }
    public void SetMapSizeZ(int mapSizeZ)
    {
        this.mapSizeZ = mapSizeZ;
    }
}
