using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ConnectedWaypoint : Waypoint
{
    public int siteID = 0;

    float debugDrawRadius = 1.0F;

    public float _connectivityRadius = 50f;

    List<ConnectedWaypoint> _connections;

    public int durability = 0;

    public enum Worker
    {
        Hout, Houtblok, Storage, General
    }

    public Worker WaypointType;

    public void Awake()
    {
        this.name += "." + WaypointType;
    }

    public void Start()
    {
        durability = 3;

        //Grab all waypoint objects in scene.
        GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        //Debug.LogError("Aantal Waypoints found =" + allWaypoints.Length);

        //Create a list of waypoints I can refer to later.
        _connections = new List<ConnectedWaypoint>();

        //Check if they're a connected waypoint.
        for (int i = 0; i < allWaypoints.Length; i++)
        {
            ConnectedWaypoint nextWaypoint = allWaypoints[i].GetComponent<ConnectedWaypoint>();

            //i.e. we found a waypoint.
            if (nextWaypoint != null)
            {
                if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= _connectivityRadius && nextWaypoint != this)
                {
                    _connections.Add(nextWaypoint);
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        switch (WaypointType)
        {
            case Worker.Hout:
                Gizmos.color = Color.green;
                break;
            case Worker.Houtblok:
                Gizmos.color = Color.black;
                break;
            case Worker.Storage:
                Gizmos.color = Color.red;
                break;
            case Worker.General:
                Gizmos.color = Color.yellow;
                break;
            default:
                Debug.LogError("Waypoint heeft geen Type");
                break;
        }

        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _connectivityRadius);
    }

    public ConnectedWaypoint NextWaypoint(ConnectedWaypoint previousWaypoint)
    {
        if (_connections.Count == 0)
        {
            //No waypoints?  Return null and complain.
            Debug.LogError("Insufficient waypoint count.");
            return null;
        }
        else if (_connections.Count == 1 && _connections.Contains(previousWaypoint))
        {
            //Only one waypoint and it's the previous one? Just use that.
            return previousWaypoint;
        }
        else //Otherwise, find a random one that isn't the previous one.
        {
            ConnectedWaypoint nextWaypoint;
            int nextIndex = 0;

            do
            {
                nextIndex = UnityEngine.Random.Range(0, _connections.Count);
                nextWaypoint = _connections[nextIndex];

            } while (nextWaypoint == previousWaypoint);

            return nextWaypoint;
        }
    }

    public void Breaktest()
    {
        if(WaypointType == Worker.Hout)
        {
            durability--;
            if (durability == 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }

    }

    void Update()
    {

    }
}
