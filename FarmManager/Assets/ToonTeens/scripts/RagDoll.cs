using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour {

    float time;
    float counter;

	void Start ()
    {
        time = Random.Range(1f, 4f);
		
	}
	
	void Update ()
    {
        counter += Time.deltaTime;
        if (counter > time) GetComponent<Animator>().enabled = false;
    }
}
