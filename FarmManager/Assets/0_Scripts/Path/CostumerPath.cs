using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;
using System;

public class CostumerPath : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 0.5f;
    float distanceTravelled;
    public GameObject deorInactive;
    public Animator anim;
    public MoneyCollect moneyCollect;
    public String moneyAreaPlace;
    public void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        moneyCollect = GameObject.Find(moneyAreaPlace).GetComponent<MoneyCollect>();
    }

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "sitDown")
        {
            StartCoroutine(sitDown());
        }
    }

    IEnumerator sitDown()
    {
        speed = 0;
        deorInactive.SetActive(true);
        anim.SetTrigger("sit");
        yield return new WaitForSeconds(4);
        anim.SetTrigger("down");
        moneyCollect.GenerateMoney(1);
        deorInactive.SetActive(false);
        speed = 0.5f;
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);

    }

}

