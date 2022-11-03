using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellmove : MonoBehaviour
{
    float speed = 1;
    void Update()
    {
        this.transform.Translate(0, (speed * Time.deltaTime) / 2, speed * Time.deltaTime);
    }
}
