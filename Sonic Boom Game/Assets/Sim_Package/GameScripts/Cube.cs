using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float moveSpeed = 100f;

    Transform target = null;

    void Start()
    {
        target = GameObject.Find("CubeTarget").transform;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

        transform.LookAt(transform.position, target.transform.position * moveSpeed);
    }
}
