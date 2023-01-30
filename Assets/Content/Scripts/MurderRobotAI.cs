using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MurderRobotAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform[] _targets;
    private int index = 0;

    public float distanceThreshold = 1.0f;
    public float delay = 3.0f;
    private bool foundOtherMe, lockUpdt;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_targets[index].position);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _targets[index].position) <= distanceThreshold) {
            //index = index < _targets.Length-1 ? index+1 : 0; no need for ternary ope
          
            index = (index + 1) % (_targets.Length);
            Invoke("MoveToNextTarget", delay);
        }

        var collider = Physics.OverlapSphere(transform.position, 2f);
        if (collider.Length > 0) { 
            foreach(Collider _c in collider){
                if (_c.gameObject.transform.position != transform.position) {
                    if (_c.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log("Hey!");
                        foundOtherMe = true;
                        SayHello();
                    }
                }
                
            }
        }

        
    }

    void MoveToNextTarget() {
        _agent.SetDestination(_targets[index].position);
    }

    void SayHello()
    {
        if (foundOtherMe && !lockUpdt)
        {
            lockUpdt = true;
            Debug.Log("Hey!");
            
        }
    }
}
