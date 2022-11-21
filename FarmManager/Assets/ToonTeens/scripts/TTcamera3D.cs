using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTcamera3D : MonoBehaviour {

    public Transform target;
    public float far = 5f;


	
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = target.position + new Vector3(0f, 2f, far);
       // target2.position= target.position + new Vector3(0f, 10f, 0f);
        transform.LookAt(target.position + new Vector3(0f, 1f, 0f));
    }
}
