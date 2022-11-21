using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;
using System;

public class CarPath : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 0.5f;
    float distanceTravelled;
    public CarManager carManager;
    public BGUI bGUI;
    public GameObject nextCar;
    public float tryDistance, distanceBetween;
    public Boolean isStopped;


    private void Start()
    {
        if (carManager.carList.IndexOf(this.gameObject) != 0)
        {
            nextCar = carManager.carList[carManager.carList.IndexOf(this.gameObject) - 1];
        }

    }
    void Update()
    {
        if (nextCar != null)
        {
            distanceBetween = Vector3.Distance(gameObject.transform.position, nextCar.transform.position);
            if (distanceBetween <= tryDistance)
            {
                speed = 0f;
            }
        }
        if (nextCar == null && !isStopped)
        {
            speed = 0.5f;
        }
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
    void OnTriggerEnter(Collider col)
    {
        if (carManager.carList.Contains(this.gameObject))
        {
            if (col.gameObject.CompareTag("OrderArea"))
            {
                bGUI.createOrder();
            }
            if (col.gameObject.CompareTag("stopWall"))
            {
                isStopped = true;
                speed = 0f;
            }
            if (col.CompareTag("Cars") && nextCar != null && distanceBetween <= tryDistance)
            {
                speed = 0;
            }
        }
        if (col.CompareTag("EndOfPath"))
        {
            carManager.carList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Cars") && nextCar != null && distanceBetween > tryDistance)
        {
            speed = 0.5f;
        }
        if (col.CompareTag("Cars") && nextCar == null)
        {
            speed = 0.5f;
        }

    }
}
