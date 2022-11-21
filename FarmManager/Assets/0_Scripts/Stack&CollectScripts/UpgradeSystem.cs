using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Pathfinding;

public class UpgradeSystem : MonoBehaviour
{
    public GameObject upgradePanel;
    public List<CraftItem> craftItems, craftItemsEP2;
    public List<StackItem> stackItems, stackItemsEP2;
    CharacterInfo myCharInfo;
    GameObject myChar;
    public OrderManager UpgradePanelSystem, UpgradePanelSystemEP2;
    public List<Text> buttonList, buttonListEP2;
    public List<int> upgradeCountList, upgradeCountListEP2;
    public float upgradeScale;
    public CollectObject collectObject;
    public WaiterAI Waiter;
    public bool isEP2;


    private void Start()
    {
        myChar = GameObject.FindGameObjectWithTag("Player");
        if (isEP2)
        {
            upgradeCountListConverterEP2();
        }
        else
        {
            upgradeCountListConverter();
        }


        myCharInfo = myChar.GetComponent<CharacterInfo>();
        collectObject = myChar.GetComponent<CollectObject>();
        myChar.GetComponent<CollectObject>().upgradeSystem = this;
    }
    public void upgradeCountListConverter()
    {
        
        if (upgradeCountList.Count > 0)
        {
            upgradeCountList = new List<int>();
        }
        
        buttonList = UpgradePanelSystem.mainSlidertext.ToList();
        foreach (var item in buttonList)
        {
            upgradeCountList.Add(int.Parse(item.text));
        }
        foreach (var item in stackItems)
        {
            item.spawnDelay = 3f - upgradeScale * upgradeCountList[2];
        }
        foreach (var item in stackItems)
        {
            item.listLimit = 3 + upgradeCountList[1];
        }
        foreach (var item in craftItems)
        {
            item.craftTime = 2f - upgradeScale * upgradeCountList[4];
        }
        collectObject.collectLimit = 3 + upgradeCountList[3];
        for (int i = 0; i < upgradeCountList[0]; i++)
        {
            craftItems[i].helper.SetActive(true);
            if (!Waiter.collectList.Contains(Waiter.transformList[i]))
            {
                
                Waiter.collectList.Add(Waiter.transformList[i]);
            }
        }
        
        if (upgradeCountList[5] != 0)
        {
            myChar.GetComponent<PlayerMovement>().speedMove = 0.035f * upgradeCountList[5];
        }
        Waiter.gameObject.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = 5f+ 2.5f*upgradeCountList[6];
        Waiter.collectSpeed = 0.4f- upgradeScale * upgradeCountList[7];
        Waiter.collectLimit = 3+upgradeCountList[8];
        

    }
    public void upgradeCountListConverterEP2()
    {
       
        if (upgradeCountListEP2.Count > 0)
        {
            upgradeCountListEP2 = new List<int>();
        }
        
        buttonListEP2 = UpgradePanelSystemEP2.mainSlidertext.ToList();
        int x = 0;
        foreach (var item in buttonListEP2)
        {
            if (x !=3 && x!=4&&x!=5)
            {
                 upgradeCountListEP2.Add(int.Parse(item.text));
            }
           x++;
        }
        foreach (var item in stackItemsEP2)
        {
            item.spawnDelay = 3f - upgradeScale * upgradeCountListEP2[2];
        }
        foreach (var item in stackItemsEP2)
        {
            item.listLimit = 3 + upgradeCountListEP2[1];
        }
        Waiter.gameObject.transform.parent.gameObject.GetComponent<AIPath>().maxSpeed = 5f+ 2.5f*upgradeCountListEP2[3];
        Waiter.collectSpeed = 0.4f- upgradeScale * upgradeCountListEP2[4];
        Waiter.collectLimit = 3+upgradeCountListEP2[5];
        for (int i = 0; i < upgradeCountListEP2[0]; i++)
        {
            craftItemsEP2[i].helper.SetActive(true);
            if (!Waiter.collectList.Contains(Waiter.transformList[i]))
            {
                Waiter.collectList.Add(Waiter.transformList[i]);
            }
        }
    }

    public void openPanel()
    {
        upgradePanel.SetActive(true);
    }
}
