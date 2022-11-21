using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPController : MonoBehaviour
{
    GameObject Player;
    string firstText,lastText;
    TextMeshProUGUI textComp;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        textComp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        firstText = Player.GetComponent<CollectObject>().stackList.Count.ToString();
        lastText = Player.GetComponent<CollectObject>().collectLimit.ToString();
        textComp.text = firstText+"/"+lastText;

    }
}
