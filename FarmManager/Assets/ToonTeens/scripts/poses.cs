using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poses : MonoBehaviour {

    public int pose;

	void Start ()
    {
        if (pose < 1) pose = Random.Range(1, 12);
        if (pose > 12) pose = Random.Range(1, 12);
        GetComponent<Animator>().SetInteger("pose", pose);
    }	
	
}
