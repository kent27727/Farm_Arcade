using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackItem : MonoBehaviour
{
    public GameObject stackPrefab;
    public List<GameObject> stackList = new List<GameObject>();
    public Transform exitPoint;
    public Vector3 localExitPoint;
    public float spawnDelay;
    public int listLimit;
    public bool isVertical, isHorizontal;

    public int horizontalLimit;
    public float horizontalPadding, horizontalPaddingUp, verticalPadding;
    int localIndex = 0;


    private void Start()
    {
        localExitPoint = exitPoint.transform.position;
        StartCoroutine(spawnItems(stackPrefab, stackList, exitPoint));
    }
    private bool checkIndexChange()
    {
        if (stackList.Count < listLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public IEnumerator spawnItems(GameObject prefab, List<GameObject> stackList, Transform exitPoint)
    {

        while (stackList.Count < listLimit)
        {
            float stackCount = stackList.Count;

            GameObject tempPrefab = Instantiate(prefab);
            if (stackCount == 0)
            {
                tempPrefab.transform.rotation = exitPoint.rotation;
                tempPrefab.transform.position = new Vector3(exitPoint.position.x, exitPoint.position.y, exitPoint.position.z);
            }

            else
            {
                if (isVertical)
                {
                    tempPrefab.transform.rotation = exitPoint.rotation;
                    tempPrefab.transform.position = new Vector3(exitPoint.position.x, exitPoint.position.y + stackCount * verticalPadding, exitPoint.position.z);
                }

                else if (isHorizontal)
                {
                    #region if-else Cases
                    if (stackList.Count < horizontalLimit)
                    {
                        exitPoint.transform.position = localExitPoint;
                    }
                    if (stackList.Count % horizontalLimit == 0)
                    {
                        localIndex = stackList.Count / horizontalLimit;
                        switch (localIndex)
                        {
                            case 0:
                                exitPoint.transform.position = localExitPoint;
                                break;
                            case 1:
                                exitPoint.transform.position = localExitPoint - new Vector3(horizontalPadding, 0, -stackCount / 4);
                                break;
                            case 2:
                                exitPoint.transform.position = localExitPoint + new Vector3(0, horizontalPaddingUp, stackCount / 4);
                                break;
                            case 3:
                                exitPoint.transform.position = localExitPoint - new Vector3(horizontalPadding, -horizontalPaddingUp, -stackCount / 4);
                                break;
                            default:
                                break;
                        }
                    }

                    tempPrefab.transform.position = new Vector3(exitPoint.position.x, exitPoint.position.y, exitPoint.position.z - stackCount / 4);
                    #endregion

                }

            }


            stackList.Add(tempPrefab);
            yield return new WaitForSeconds(spawnDelay);
            yield return new WaitUntil(checkIndexChange);

        }



    }
    // Start is called before the first frame update

}
