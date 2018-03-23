using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    private int gameState;
    private SelectionControl selectionControl;
    // Use this for initialization
    void Awake()
    {
        this.InitGameState();
    }

    void Start () {
        this.selectionControl = FindObjectOfType<SelectionControl>();
        print("GameControl Key Minus and Plus to change GameState");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            this.SetGameState(1);
        }
        else if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            this.SetGameState(0);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            this.selectionControl.StartNewSelection();
        }
    }
    private void InitGameState()
    {
        this.SetGameState(0);
    }
    
    /// <summary>
    /// Get the curent gameState
    /// </summary>
    /// <returns>Returns an integer with the gamestate</returns>
    public int GetGameState()
    {
        return this.gameState;
    }
    /// <summary>
    /// Set a gamestate
    /// </summary>
    /// <param name="gameState">Integer of the new gamestate</param>
    public void SetGameState(int gameState)
    {
        /*
         * 0 = view
         * 1 = edit
         */
        this.gameState = gameState;
    }
}
