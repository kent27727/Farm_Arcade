using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterInfo : MonoBehaviour
{
    public int Cash;
    public List<bool> unlockedAreas,unlockedAreasEP2;
    public Text cashText,cashText2;
    public bool isCollecting;
    public BGUI bGUI,bGUIEP2;
    public List<Sprite> imageList,imageListEP2;
    public List<GameObject> forUnlockAreas,forUnlockAreasEP2;
    public GameObject ep2Opener;
    // Start is called before the first frame update
    public void addUnlockedItem(){
        for (int i = 0; i < 4; i++)
        {
            if (unlockedAreas[i])
            {
                if (!bGUI.unlockedItems.Contains(imageList[i]))
                {
                    bGUI.unlockedItems.Add(imageList[i]);
                } 
            }
            if (unlockedAreasEP2[i])
            {
                if (!bGUIEP2.unlockedItems.Contains(imageListEP2[i]))
                {
                    bGUIEP2.unlockedItems.Add(imageListEP2[i]);
                }
            }
        }
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("CashArea"))
        {
            MoneyCollect moneyArea = other.gameObject.GetComponent<MoneyCollect>();
            moneyArea.lastIndex = (moneyArea.stackList.Count % 12) -1;
            isCollecting = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("CashArea"))
        {
            MoneyCollect moneyArea = other.gameObject.GetComponent<MoneyCollect>();
            moneyArea.lastIndex = (moneyArea.stackList.Count % 12) -1;
            isCollecting = false;
        }
    }
    public void SaveUnlockBool(){
        int x= 0,y = 0;
        foreach (var item in unlockedAreas)
        {
            if (item)
            {
                x++;
            }
            else
            {
                break;
            }
        }
        foreach (var item in unlockedAreasEP2)
        {
            if (item)
            {
                y++;
            }
            else
            {
                break;
            }
        }
        PlayerPrefs.SetInt("UnlockedIndex", x + y);
        Debug.Log(PlayerPrefs.GetInt("UnlockedIndex"));
    }
    private void Update() {
        cashText.text = Cash.ToString();
        cashText2.text = Cash.ToString();
        PlayerPrefs.SetInt("Cash", Cash);
        
    }
    public void unlockLocks(){
        int x = 0;
        foreach (var item in unlockedAreas)
        {
            if (unlockedAreas.IndexOf(item)<4)
            {
                if (item && x<forUnlockAreas.Count)
                {
                    forUnlockAreas[x].SetActive(true);
                    x++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }       
        }
        if (x>=4)
        {
            forUnlockAreas[3].SetActive(true);
        }
        int y = 0;
        foreach (var item in unlockedAreasEP2)
        {
            if (unlockedAreas.IndexOf(item)<4)
            {if (item && y<forUnlockAreasEP2.Count)
            {
                forUnlockAreasEP2[y].SetActive(true);
                y++;
            }
            else
            {
                break;
            }
            }
            else
            {
                break;
            }
            
        }

        if (y>=4)
        {
            forUnlockAreasEP2[3].SetActive(true);
        }
        int z = 0;
        foreach (var item in unlockedAreas)
        {
            if (item)
            {
                z+=1;
            }
            else
            {
                break;
            }
        }
        Debug.Log(z + " " + unlockedAreas.Count);   
        if (z == unlockedAreas.Count)
        {
            ep2Opener.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("CashArea"))
        {
            MoneyCollect moneyArea = other.GetComponent<MoneyCollect>();
            moneyArea.MoveMoney(this.gameObject.GetComponent<CharacterInfo>());
        }
        
    }
    private void Start()
    {
            Cash = PlayerPrefs.GetInt("Cash");     
    }
}
