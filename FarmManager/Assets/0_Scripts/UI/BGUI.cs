using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BGUI : MonoBehaviour
{
    public List<GameObject> orderList;
    public GameObject orderPrefab, startPos;
    public List<Sprite> unlockedItems;
    public MoneyCollect moneyCollect;
    public CarManager carManager;
    public CharacterInfo character;
    public List<RectTransform> imageHolders;

    private void Start() {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfo>();
    }
    public int getInt(GameObject obj)
    {

        return orderList.IndexOf(obj);
    }
    public void createOrder()
    {
        if (orderList.Count != 3)
        {
            GameObject temp = Instantiate(orderPrefab);
            temp.transform.SetParent(this.gameObject.transform);
            temp.GetComponent<RectTransform>().sizeDelta = startPos.GetComponent<RectTransform>().sizeDelta;
            temp.GetComponent<RectTransform>().localScale = startPos.GetComponent<RectTransform>().localScale;
            temp.GetComponent<RectTransform>().position = startPos.GetComponent<RectTransform>().position;
            temp.GetComponent<OrderUI>().moneyCollect = moneyCollect;
            temp.GetComponent<OrderUI>().character = character;
            orderList.Add(temp);
            temp.GetComponent<OrderUI>().bgUI = this;
            temp.GetComponent<OrderUI>().imageHolders = imageHolders;
        }


    }

    private void FixedUpdate()
    {
        orderList = orderList.Where(item => item != null).ToList();

    }
}
