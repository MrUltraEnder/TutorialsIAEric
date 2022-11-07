using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject ShellSpawnPos;
    public GameObject parent;
    public GameObject target;
    float speed = 15;
    float turnSpeed = 2;

    bool canShoot = true;
    void Start()
    {

    }
    void ResetShoot()
    {
        canShoot = true;
    }
    void Fire()
    {
        if (canShoot)
        {
            GameObject shell = Instantiate(shellPrefab, ShellSpawnPos.transform.position, ShellSpawnPos.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
            canShoot = false;
            Invoke("ResetShoot", 0.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {




        Vector3 direction = (target.transform.position - ShellSpawnPos.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        float? angle = RotateTurret();
        if (angle != null && Vector3.Angle(direction, parent.transform.forward) < 10)
        {
            Fire();
        }


    }

    float? CalculateAngle(bool low)
    {
        Vector3 tagetDir = target.transform.position - this.transform.position;
        float y = tagetDir.y;
        tagetDir.y = 0;
        float x = tagetDir.magnitude;
        float gravity = 9.81f;
        float sSqr = speed * speed;
        float underTheSqrRoot = sSqr * sSqr - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;
            if (low)
            {
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
        {
            return null;
        }

    }

    float? RotateTurret()
    {
        //if you want to shoot at the highest angle possible change to false
        float? angle = CalculateAngle(true);
        if (angle != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, 0, 0);
        }
        return angle;
    }
}

