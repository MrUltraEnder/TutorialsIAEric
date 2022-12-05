using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    public int currentWP = 0;
    public float speed = 10.0f;
    public float rotSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < 3.0f)
        {
            currentWP++;
        }
        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }
        //transform.LookAt(waypoints[currentWP].transform.position);
        Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, Time.deltaTime * rotSpeed);

        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}

