using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float speed = 0.5f;
    Vector3 lastPosition = Vector3.zero;

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 0f, -300f), speed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cars"))
        {
            speed = 0;
        }
        else
        {
            speed = 0.5f;
        }

    }
}
