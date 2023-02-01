
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MurderRobotAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private WinSysManager wsm;
    public List<Transform> allWayPoint, pickedWaypoint;
    Vector3 currentTarget;

    private int index = 0;

    public float distanceThreshold = 1.0f;
    public float delay = 0f;
    private bool foundOtherMe;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        allWayPoint = wsm.GetTargets();
        ResetBag();
        PickBag();
    }

    void SetTargetPosition(Vector3 _targetPos)
    {
        currentTarget = _targetPos;
        _agent.SetDestination(_targetPos);
    }

    void ResetBag()
    {
        pickedWaypoint.AddRange(allWayPoint);
    }

    void PickBag()
    {
        
        int choice = Random.Range(0, pickedWaypoint.Count);
        //Debug.Log(choice);
        SetTargetPosition(pickedWaypoint[choice].position);
        pickedWaypoint.RemoveAt(choice);
        if (pickedWaypoint.Count <= 1)
        {

            ResetBag();
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, currentTarget) <= distanceThreshold) {
            //index = index < _targets.Length-1 ? index+1 : 0; no need for ternary ope
          
            Invoke("PickBag", delay);
        }

        var collider = Physics.OverlapSphere(transform.position, 1f);
        if (collider.Length > 0) { 
            foreach(Collider _c in collider){
                if (_c.gameObject.transform.position != transform.position) {
                    if (_c.gameObject.name == "attackBot")
                    {
                        DetectOther(_c.gameObject);
                        Invoke("InvokeReset", 3.0f);
                    }
                }
                
            }
        }

        
    }

    void DetectOther(GameObject _other)
    {
        if (!foundOtherMe)
        {
            foundOtherMe = true;
            _other.transform.parent.GetComponent<MurderRobotAI>().SwitchMaterial(this.gameObject);

            wsm.UpdateTeam();

        }
    }

    void InvokeReset()
    {
        foundOtherMe = false;
    }

    void SwitchMaterial(GameObject _other)
    {
        
        MeshRenderer robotMat = transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>();
        
        robotMat.material = _other.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material;
        gameObject.tag = _other.tag;

    }
}
