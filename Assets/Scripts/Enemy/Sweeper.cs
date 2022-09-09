using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sweeper : MonoBehaviour
{
    public GameObject target;
    private UnityEngine.AI.NavMeshAgent _agent;
    Animator _anim;
    bool walking = false;

    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _anim = transform.Find("Body").GetComponent<Animator>();
    }

    public void Walking(){
        _anim.SetBool("stay", false);
        walking = true;
        _agent.SetDestination(target.transform.position);
    }

    void Update(){
        if(walking){
            if (!_agent.pathPending){
                if (_agent.remainingDistance <= _agent.stoppingDistance){
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f){
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
