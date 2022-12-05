using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCylinder : MonoBehaviour
{
    public GameObject cylinder;
    public GameObject[] agents;
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(cylinder, hit.point, cylinder.transform.rotation);
                foreach (GameObject a in agents)
                {
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point);
                }
            }

        }
    }
}
