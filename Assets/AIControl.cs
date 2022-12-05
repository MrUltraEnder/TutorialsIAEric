using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{

    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator anim;

    float detectionRadius = 20f;
    float fleeRadius = 10f;
    float speedMult;

    void ResetAgent()
    {
        speedMult = Random.Range(0.5f, 1.5f);
        agent.speed = 2f * speedMult;
        agent.angularSpeed = 120f;
        anim.SetFloat("speedMult", speedMult);

        anim.SetTrigger("isWalking");
        agent.ResetPath();
    }

    // Use this for initialization
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = this.GetComponent<Animator>();
        anim.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
        ResetAgent();
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    public void DetectNewObstacle(Vector3 pos)
    {
        if (Vector3.Distance(pos, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - pos).normalized;
            Vector3 newgoal = this.transform.position + fleeDirection * fleeRadius;
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newgoal, path);
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger("isRunning");
                agent.speed = 10f;
                agent.angularSpeed = 500f;
            }
        }
    }
}

