using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject ShellSpawnPos;
    void Start()
    {

    }

    void Fire()
    {
        GameObject shell = Instantiate(shellPrefab, ShellSpawnPos.transform.position, ShellSpawnPos.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
}

