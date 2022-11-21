using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTcamera2D : MonoBehaviour {

    public Transform target;
    Vector3 way;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        way = new Vector3(target.position.x - transform.position.x, 0f, 0f);
        GetComponent<Rigidbody>().velocity = way * way.magnitude;

    }
}
