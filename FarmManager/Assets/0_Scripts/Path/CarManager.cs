using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarManager : MonoBehaviour
{
    public List<GameObject> carPrefab;
    public Transform spawnPoint1,spawnPoint2;
    public PathCreator pathCreator1,pathCreator2;
    public List<GameObject> carList;
    public float carSpawnRate;
    public BGUI bGUI;

    public bool checkCarCount(){

        if (carList.Count<10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Start()
    {
        StartCoroutine(spawnRoutine());
    }
    IEnumerator spawnRoutine(){
        while (carList.Count <10)
        {
            spawnCar();
            yield return new WaitForSeconds(carSpawnRate);
            yield return new WaitUntil(checkCarCount);
        }
        
    }
    
    void spawnCar()
    {
        int y = Random.Range(0,carPrefab.Count);
        GameObject tempCar = Instantiate(carPrefab[y],spawnPoint1.position,spawnPoint1.rotation);
        tempCar.GetComponent<CarPath>().pathCreator = pathCreator1;
        tempCar.GetComponent<CarPath>().carManager = this;
        tempCar.GetComponent<CarPath>().bGUI = bGUI;
        carList.Add(tempCar);
    }
}
