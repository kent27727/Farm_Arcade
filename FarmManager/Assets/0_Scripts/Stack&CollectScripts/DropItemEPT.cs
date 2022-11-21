using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class DropItemEPT : MonoBehaviour {
    public List<GameObject> localList,carrotList,pumpkinList,grapeList,cornList,tomatoList;
    public BGUI bgUI;
    public List<GameObject> stackList;
    public GameObject tempOBJ;
    public OrderUI tempList;
    
    public bool isDropping;
    public bool isSomeoneIn;
    private void Update() {
        stackList = stackList.Where(item => item != null).ToList();
        if (bgUI.orderList.Count !=0)
        {
            OrderGive();
        }
    }
    public void OrderGive(){
        if (!isDropping)
        {
        
        tempList = bgUI.orderList[0].GetComponent<OrderUI>();
        if (tempList.grapeCount >=1 && tempOBJ == null)
        {
            
            List<GameObject> reversedList = grapeList;
            reversedList.Reverse();
            foreach (var item in reversedList)
            {
                if (item.transform.childCount !=0)
                {
                    tempOBJ = item.transform.GetChild(0).gameObject;
                    isDropping = true;
                    break;
                }
            }
            if (tempOBJ!=null)
            {
                tempList.grapeCount--;
                tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position,0.2f).OnComplete(()=>{Destroy(tempOBJ);
                isDropping = false;
                tempOBJ = null;
                });
            }
           
                
        }
         if (tempList.cornCount >=1 && tempOBJ == null)
        {
            
            List<GameObject> reversedList = cornList;
            reversedList.Reverse();
            foreach (var item in reversedList)
            {
                if (item.transform.childCount !=0)
                {
                    tempOBJ = item.transform.GetChild(0).gameObject;
                    isDropping = true;
                    break;
                }
            }
            if (tempOBJ!=null)
            {
                tempList.cornCount--;
                tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position,0.2f).OnComplete(()=>{Destroy(tempOBJ);
                isDropping = false;
                tempOBJ = null;
                });
            }
           
                
        }
        if (tempList.tomatoCount >=1 && tempOBJ == null)
        {
            
            List<GameObject> reversedList = tomatoList;
            reversedList.Reverse();
            foreach (var item in reversedList)
            {
                if (item.transform.childCount !=0)
                {
                    tempOBJ = item.transform.GetChild(0).gameObject;
                    isDropping = true;
                    break;
                }
            }
            if (tempOBJ!=null)
            {
                tempList.tomatoCount--;
                tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position,0.2f).OnComplete(()=>{Destroy(tempOBJ);
                isDropping = false;
                tempOBJ = null;
                });
            }
           
                
        }
        if (tempList.pumpkinCount >=1 && tempOBJ == null)
        {
            
            List<GameObject> reversedList = pumpkinList;
            reversedList.Reverse();
            foreach (var item in reversedList)
            {
                if (item.transform.childCount !=0)
                {
                    tempOBJ = item.transform.GetChild(0).gameObject;
                    isDropping = true;
                    break;
                }
            }
            if (tempOBJ!=null)
            {
                tempList.pumpkinCount--;
                tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position,0.2f).OnComplete(()=>{Destroy(tempOBJ);
                isDropping = false;
                tempOBJ = null;
                });
            }
           
                
        }
        if (tempList.carrotCount >=1 && tempOBJ == null)
        {
            
            List<GameObject> reversedList = carrotList;
            reversedList.Reverse();
            foreach (var item in reversedList)
            {
                if (item.transform.childCount !=0)
                {
                    tempOBJ = item.transform.GetChild(0).gameObject;
                    isDropping = true;
                    break;
                }
            }
            if (tempOBJ!=null)
            {
                tempList.carrotCount--;
                tempOBJ.transform.DOMove(bgUI.carManager.carList[0].transform.position,0.2f).OnComplete(()=>{Destroy(tempOBJ);
                isDropping = false;
                tempOBJ = null;
                });
            }
           
                
        }
         
        }
        

    }
}