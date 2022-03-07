using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoMove : MonoBehaviour
{
    public GameObject[] Targets;
    public AutoShot Weapon;

    private NavMeshAgent _navMeshAgent;
    private int _currenttarget = 0;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!GetComponent<Health>().Die
            && !Weapon.Visible)
        {
            _navMeshAgent.isStopped = false;
            if (Vector3.Magnitude(transform.position - Targets[_currenttarget].transform.position) < 2.0f)
            {
                _currenttarget = (_currenttarget + 1) % Targets.Length;
            }
    
            _navMeshAgent.SetDestination(Targets[_currenttarget].transform.position);
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }
    }
}
