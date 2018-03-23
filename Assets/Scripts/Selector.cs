using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeSelectionBox(Transform t)
    {
        this.transform.parent = t;
        this.name = "SelectionObject";
        this.gameObject.SetActive(false);

    }
    public void MakeSelection(Transform t)
    {
        this.transform.parent = t;
        this.name = "selection";
        this.gameObject.SetActive(true);
        this.GetComponent<MeshRenderer>().material = Resources.Load("Material/Placed", typeof(Material)) as Material; ;
    }

    void OnCollisionEnter(Collision col)
    {
        print("collide:enter");
        if (col.gameObject.name == "selection")
        {
            Destroy(col.gameObject);
        }
    }
    void OnCollisionStay(Collision col)
    {
        print("collide:stay");
    }
    void OnCollisionExit(Collision col)
    {
        print("collide:exit");
    }
    void OnTriggerEnter(Collider other)
    {
        print("trigger:enter");
    }
}