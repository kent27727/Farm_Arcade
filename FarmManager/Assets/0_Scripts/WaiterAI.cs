using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

public class WaiterAI : MonoBehaviour
{
    public List<Transform> collectList, transformList;
    public bool isCollecting;
    public GameObject currentOBJ;
    public List<GameObject> stackList;
    public Transform collectPoint;
    public float collectSpeed;
    public int collectLimit;
    public AIDestinationSetter aiSet;
    public Transform sellArea;
    private bool isDropping;
    public float dropSpeed;
    public int round;

    // Update is called once per frame
    private void Update()
    {
        if (currentOBJ != null)
        {
            MoveObj(currentOBJ);
        }

    }

    void MoveObj(GameObject obj)
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, collectPoint.position + new Vector3(0, (float)stackList.Count / 10, 0), 10 * Time.deltaTime);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CollectArea"))
        {
            isCollecting = false;
        }
        if (other.CompareTag("DropArea"))
        {
            if (stackList.Count == 0)
            {
                round += 1;
                if (round == collectList.Count)
                {
                    round = 0;
                }
                aiSet.target = collectList[round];
            }
        }
    }
    IEnumerator collectItem(GameObject obj, StackItem stackItem)
    {
        if (!isCollecting)
        {
            if (!stackList.Contains(obj))
            {
                stackList.Add(obj);
                stackItem.stackList.Remove(obj);
                float stacklistCount = stackList.Count;

                isCollecting = true;

                currentOBJ = obj;

                yield return new WaitForSeconds(collectSpeed);
                obj.transform.SetParent(collectPoint);
                obj.transform.position = new Vector3(collectPoint.position.x, collectPoint.position.y + stacklistCount / 10, collectPoint.position.z);

                isCollecting = false;

                obj.transform.DORotate(collectPoint.rotation.eulerAngles, 0.5f).OnComplete(() => { obj.transform.rotation = collectPoint.rotation; });
                currentOBJ = null;

            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CollectArea"))
        {
            if (stackList.Count < collectLimit)
            {
                StackItem stackTemp = other.gameObject.GetComponent<StackItem>();
                if (stackTemp.stackList.Count > 0)
                {
                    GameObject objTemp = stackTemp.stackList[stackTemp.stackList.Count - 1];
                    if (stackTemp.stackList.Contains(objTemp))
                    {
                        StartCoroutine(collectItem(objTemp, stackTemp));
                    }
                }
                else
                {
                    aiSet.target = sellArea;
                }
            }
            else
            {
                aiSet.target = sellArea;
            }
        }
        else if (other.CompareTag("DropArea"))
        {
            if (other.gameObject.GetComponent<DropItem>() != null)
            {
                DropItem stackTemp = other.gameObject.GetComponent<DropItem>();
                if (stackList.Count != 0)
                {
                    GameObject objTemp = stackList[stackList.Count - 1];

                    if (stackList.Contains(objTemp))
                    {
                        dropItem(objTemp, stackTemp);

                    }
                }
                else
                {
                    aiSet.target = collectList[0];
                }
            }
            else if (other.gameObject.GetComponent<DropItemEPT>() != null)
            {
                DropItemEPT stackTemp = other.gameObject.GetComponent<DropItemEPT>();
                if (stackList.Count != 0)
                {
                    GameObject objTemp = stackList[stackList.Count - 1];

                    if (stackList.Contains(objTemp))
                    {
                        dropItemEPT(objTemp, stackTemp);

                    }
                }
                else
                {
                    aiSet.target = collectList[0];
                }
            }


        }
    }
    void dropItemEPT(GameObject obj, DropItemEPT stackItem)
    {
        if (!isDropping)
        {
            isDropping = true;
            bool ishaveSpace = false;

            switch (obj.name)
            {
                case "Carrot(Clone)":
                    stackItem.localList = stackItem.carrotList;
                    break;
                case "Tomato(Clone)":
                    stackItem.localList = stackItem.tomatoList;
                    break;
                case "Grapes(Clone)":
                    stackItem.localList = stackItem.grapeList;
                    break;
                case "Pumpkin(Clone)":
                    stackItem.localList = stackItem.pumpkinList;
                    break;
                case "Corn(Clone)":
                    stackItem.localList = stackItem.cornList;
                    break;
                default:
                    break;
            }
            int indexofList = 0;
            foreach (var item in stackItem.localList)
            {

                if (item.transform.childCount == 1)
                {
                    indexofList += 1;
                    ishaveSpace = false;

                }
                else if (item.transform.childCount == 0)
                {
                    ishaveSpace = true;
                    break;
                }
            }
            if (ishaveSpace)
            {
                stackItem.stackList.Add(obj);
                stackList.Remove(obj);
                obj.transform.SetParent(stackItem.localList[indexofList].transform);
                obj.transform.DOMove(stackItem.localList[indexofList].transform.position, dropSpeed).OnComplete(() =>
            {
                obj.transform.position = stackItem.localList[indexofList].transform.position;
                isDropping = false;
                });
                obj.transform.DORotate(stackItem.localList[indexofList].transform.rotation.eulerAngles, dropSpeed);
            }
            else
            {
                stackList.Remove(obj);
                obj.transform.DOMove(stackItem.localList[0].transform.position, dropSpeed).OnComplete(() =>
            {
                Destroy(obj);
                isDropping = false;

            });

            }


        }

    }
    void dropItem(GameObject obj, DropItem stackItem)
    {
        if (!isDropping)
        {
            isDropping = true;
            bool ishaveSpace = false;

            switch (obj.name)
            {
                case "Cheese(Clone)":
                    stackItem.localList = stackItem.cheeseList;
                    break;
                case "Sausage(Clone)":
                    stackItem.localList = stackItem.sausageList;
                    break;
                case "Milk(Clone)":
                    stackItem.localList = stackItem.milkList;
                    break;
                case "Egg(Clone)":
                    stackItem.localList = stackItem.eggList;
                    break;
                case "Meat(Clone)":
                    stackItem.localList = stackItem.meatList;
                    break;
                default:
                    break;
            }
            int indexofList = 0;
            foreach (var item in stackItem.localList)
            {

                if (item.transform.childCount == 1)
                {
                    indexofList += 1;
                    ishaveSpace = false;

                }
                else if (item.transform.childCount == 0)
                {
                    ishaveSpace = true;
                    break;
                }
            }
            if (ishaveSpace)
            {
                stackItem.stackList.Add(obj);
                stackList.Remove(obj);
                obj.transform.SetParent(stackItem.localList[indexofList].transform);
                obj.transform.DOMove(stackItem.localList[indexofList].transform.position, dropSpeed).OnComplete(() =>
            {
                obj.transform.position = stackItem.localList[indexofList].transform.position;
                
                isDropping = false;

            });
                obj.transform.DORotate(stackItem.localList[indexofList].transform.rotation.eulerAngles, dropSpeed);
            }
            else
            {
                stackList.Remove(obj);
                obj.transform.DOMove(stackItem.localList[0].transform.position, dropSpeed).OnComplete(() =>
            {
                Destroy(obj);
                isDropping = false;

            });

            }


        }

    }
}
