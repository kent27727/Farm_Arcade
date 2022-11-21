using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class DropItem : MonoBehaviour
{
    public List<GameObject> localList, milkList, meatList, eggList, sausageList, cheeseList;
    public BGUI bgUI;
    public List<GameObject> stackList;
    public GameObject tempOBJ;
    public OrderUI tempList;

    public bool isDropping;
    public bool isSomeoneIn;
    private void Update()
    {
        stackList = stackList.Where(item => item != null).ToList();
        if (bgUI.orderList.Count != 0)
        {
            OrderGive();
        }
    }
    public void OrderGive()
    {
        if (!isDropping)
        {

            tempList = bgUI.orderList[0].GetComponent<OrderUI>();
            if (tempList.sausageCount >= 1 && tempOBJ == null)
            {

                List<GameObject> reversedList = sausageList;
                reversedList.Reverse();
                foreach (var item in reversedList)
                {
                    if (item.transform.childCount != 0)
                    {
                        tempOBJ = item.transform.GetChild(0).gameObject;
                        isDropping = true;
                        reversedList.Reverse();
                        break;
                    }
                }
                if (tempOBJ != null)
                {
                    tempList.sausageCount--;
                    tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position, 0.2f).OnComplete(() =>
                    {
                        if (tempOBJ != null)
                        {
                            Destroy(tempOBJ);
                        }
                        isDropping = false;
                        tempOBJ = null;
                    });
                }


            }
            if (tempList.milkCount >= 1 && tempOBJ == null)
            {

                List<GameObject> reversedList = milkList;
                reversedList.Reverse();
                foreach (var item in reversedList)
                {
                    if (item.transform.childCount != 0)
                    {
                        tempOBJ = item.transform.GetChild(0).gameObject;
                        isDropping = true;
                        reversedList.Reverse();
                        break;
                    }
                }
                if (tempOBJ != null)
                {
                    tempList.milkCount--;
                    tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position, 0.2f).OnComplete(() =>
                    {
                        Destroy(tempOBJ);
                        isDropping = false;
                        tempOBJ = null;

                    });
                }


            }
            if (tempList.eggCount >= 1 && tempOBJ == null)
            {

                List<GameObject> reversedList = eggList;
                reversedList.Reverse();
                foreach (var item in reversedList)
                {
                    if (item.transform.childCount != 0)
                    {
                        tempOBJ = item.transform.GetChild(0).gameObject;
                        isDropping = true;
                        reversedList.Reverse();
                        break;
                    }
                }
                if (tempOBJ != null)
                {
                    tempList.eggCount--;
                    tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position, 0.2f).OnComplete(() =>
                    {
                        Destroy(tempOBJ);
                        isDropping = false;
                        tempOBJ = null;

                    });
                }


            }
            if (tempList.cheeseCount >= 1 && tempOBJ == null)
            {

                List<GameObject> reversedList = cheeseList;
                reversedList.Reverse();
                foreach (var item in reversedList)
                {
                    if (item.transform.childCount != 0)
                    {
                        tempOBJ = item.transform.GetChild(0).gameObject;
                        isDropping = true;
                        reversedList.Reverse();
                        break;
                    }
                }
                if (tempOBJ != null)
                {
                    tempList.cheeseCount--;
                    tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position, 0.2f).OnComplete(() =>
                    {
                        Destroy(tempOBJ);
                        isDropping = false;
                        tempOBJ = null;

                    });
                }


            }
            if (tempList.meatCount >= 1 && tempOBJ == null)
            {

                List<GameObject> reversedList = meatList;
                reversedList.Reverse();
                foreach (var item in reversedList)
                {
                    if (item.transform.childCount != 0)
                    {
                        tempOBJ = item.transform.GetChild(0).gameObject;
                        isDropping = true;
                        reversedList.Reverse();
                        break;
                    }
                }
                if (tempOBJ != null)
                {
                    tempList.meatCount--;
                    tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position, 0.2f).OnComplete(() =>
                    {
                        Destroy(tempOBJ);
                        isDropping = false;
                        tempOBJ = null;

                    });
                }


            }

        }


    }
}