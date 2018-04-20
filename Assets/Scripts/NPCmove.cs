using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCmove : MonoBehaviour
{
    [SerializeField] 
    Transform _destination;

    NavMeshAgent _NavMeshAgent;

    // Use this for initialization
    void Start()
    {
        _NavMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_NavMeshAgent == null)
        {
            Debug.LogError("The nav Mesh Component Agent component is not attached to " + gameObject.name);
        }
        else
        {
            setDestination();
        }
    }

    private void setDestination ()
    {
		if(_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _NavMeshAgent.SetDestination(targetVector);
        }
	}
}
