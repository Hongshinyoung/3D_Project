using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    private Vector3 startPos;
    private Transform currentPos;
    private bool isForward;

    public float maxMovingDistance;
    public float moveSpeed;

    private void Start()
    {
        startPos = transform.position;
        currentPos = transform;
        isForward = true;
    }

    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        if (isForward)
        {
            currentPos.position += Vector3.forward * moveSpeed * Time.deltaTime;

            if (currentPos.position.z - startPos.z >= maxMovingDistance)
                isForward = false;
        }
        else
        {
            currentPos.position -= Vector3.forward * moveSpeed * Time.deltaTime;

            if (startPos.z - currentPos.position.z >= maxMovingDistance)
                isForward = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = currentPos;

    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent = null;
    }
}
