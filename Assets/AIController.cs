using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag("Goal");
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);
        anim = GetComponent<Animator>();
        anim.SetTrigger("isWalking");
        anim.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
        float sm = Random.Range(0.5f, 2.0f);
        anim.SetFloat("speedMult", sm);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            int i = Random.Range(0, goalLocations.Length);
            agent.SetDestination(goalLocations[i].transform.position);
        }
    }
}
