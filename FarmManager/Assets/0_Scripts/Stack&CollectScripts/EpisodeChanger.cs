using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeChanger : MonoBehaviour
{
    public GameObject EPONE,EPTWO,player;
    public bool isEP2;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            if (player.transform.position.x>20)
            {
                EPONE.GetComponent<Canvas>().enabled = false;
                EPTWO.GetComponent<Canvas>().enabled = true;
            }
            else if (player.transform.position.x<=20)
            {
                EPONE.GetComponent<Canvas>().enabled = true;
                EPTWO.GetComponent<Canvas>().enabled = false;
            }
                
        }
        
        
    }
}
