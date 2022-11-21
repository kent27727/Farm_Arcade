using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charTeleport : MonoBehaviour
{
    public Transform teleportTarger;
    GameObject thePlayer;

    private void Start() {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thePlayer.transform.position = teleportTarger.transform.position;
        }
        
    }
}
