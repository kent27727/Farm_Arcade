using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class MoneyCollect : MonoBehaviour
{
    public GameObject moneyPrefab;
    public List<GameObject> moneyList, stackList;
    public int stackHeight;
    public float horizontalPadding;
    public Image moneyUIprefab, moneyStartPos, moneyEndPos;
    public GameObject canvas;
    GameObject currentOBJ;
    GameObject player;
    public int lastIndex;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GenerateMoney(20);
    }
    public void GenerateMoney(int amountOfMoney)
    {
        //hey
        for (int i = 0; i < 12; i++)
        {

            if (amountOfMoney >= 1)
            {

                if (lastIndex != 0)
                {
                    i = lastIndex;
                    lastIndex = 0;
                }
                Vector3 tempVector = new Vector3(moneyList[i].transform.position.x, moneyList[i].transform.position.y + horizontalPadding * stackHeight, moneyList[i].transform.position.z);
                GameObject temp = Instantiate(moneyPrefab, tempVector, moneyList[0].transform.rotation);
                stackList.Add(temp);
                temp.transform.localScale = Vector3.zero;
                temp.transform.DOScale(Vector3.one / 2, 0.3f);
                amountOfMoney--;
                if (i == 11)
                {
                    i = -1;
                    stackHeight++;
                }
            }
            else
            {
                lastIndex = i;
                break;
            }

        }
    }
    private void Update()
    {
        if (currentOBJ != null)
        {
            currentOBJ.transform.position = Vector3.MoveTowards(currentOBJ.transform.position, player.transform.position, 15 * Time.deltaTime);

        }
        stackList = stackList.Where(item => item != null).ToList();
        lastIndex = stackList.Count % 12;

    }
    public bool isEnumStarted;
    public void MoveMoney(CharacterInfo character)
    {
        if (stackList.Count > 0)
        {
            if (!isEnumStarted)
            {
                isEnumStarted = true;
                currentOBJ = stackList[stackList.Count - 1];
                Image tempMoney = Instantiate(moneyUIprefab);
                tempMoney.transform.SetParent(canvas.transform);
                tempMoney.rectTransform.sizeDelta = moneyStartPos.rectTransform.sizeDelta;
                tempMoney.rectTransform.position = moneyStartPos.rectTransform.position;

                tempMoney.transform.DOMove(moneyEndPos.rectTransform.position, 0.2f).SetEase(Ease.OutBounce).SetEase(Ease.OutExpo).OnComplete(() =>
                {
                    character.Cash += 10;
                    if (stackList.Count < 12)
                    {
                        stackHeight = 0;
                    }
                    else
                    {
                        stackHeight = stackList.Count / 12;
                    }

                    moneyEndPos.rectTransform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.15f).OnComplete(() => { moneyEndPos.rectTransform.DOScale(Vector3.one, 0.15f); });
                    Destroy(tempMoney.gameObject);
                    Destroy(currentOBJ);
                    stackList = stackList.Where(item => item != null).ToList();
                    isEnumStarted = false;

                });

            }
        }


    }
}