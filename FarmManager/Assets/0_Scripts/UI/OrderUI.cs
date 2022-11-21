using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{

    public List<GameObject> sprites;
    public CharacterInfo character;
    public List<Sprite> unlocketItems;
    public BGUI bgUI;
    public Vector3[] imageTransform;
    public List<string> orderNames;
    public Sprite spriteToAssign;
    public MoneyCollect moneyCollect;
    public List<RectTransform> imageHolders;
    public RectTransform myRect;
    public int myIndex, startIndex;
    public bool isProcessing;

    public int sausageCount, cheeseCount, eggCount, meatCount, milkCount, tomatoCount, grapeCount, carrotCount, pumpkinCount, cornCount;

    private void Start()
    {
        myRect = GetComponent<RectTransform>();
        imageTransform = new Vector3[3];
        imageTransform[2] = new Vector3(-29.1f, -63, 0);
        imageTransform[1] = new Vector3(-91.2f, -63, 0);
        imageTransform[0] = new Vector3(-149.3f, -63, 0);


        unlocketItems = bgUI.unlockedItems;
        int x = Random.Range(0, 3);
        for (int i = 0; i < x + 1; i++)
        {
            sprites[i].SetActive(true);
            int y = Random.Range(0, unlocketItems.Count);
            sprites[i].GetComponent<Image>().sprite = unlocketItems[y];
            sprites[i].GetComponent<RectTransform>().sizeDelta = unlocketItems[y].rect.size * 2;
            orderNames.Add(sprites[i].GetComponent<Image>().sprite.name);
        }
        foreach (var item in orderNames)
        {
            switch (item)
            {
                case "Sausage":
                    sausageCount++;
                    break;
                case "Cheese":
                    cheeseCount++;
                    break;
                case "Meat":
                    meatCount++;
                    break;
                case "Milk":
                    milkCount++;
                    break;
                case "Egg":
                    eggCount++;
                    break;
                case "Corn":
                    cornCount++;
                    break;
                case "Grapes":
                    grapeCount++;
                    break;
                case "Carrot":
                    carrotCount++;
                    break;
                case "Pumpkin":
                    pumpkinCount++;
                    break;
                case "Tomato":
                    tomatoCount++;
                    break;
                default:
                    break;
            }
        }
        AnimateOrder();
        startIndex = bgUI.getInt(this.gameObject);
    }
    private void Update()
    {
        if (myRect.position.x != imageHolders[bgUI.getInt(this.gameObject)].position.x && !isProcessing)
        {
            List<Vector3> imageTransforms = imageTransform.ToList();
            imageTransforms.Reverse();
            isProcessing = true;
            this.GetComponent<RectTransform>().DOAnchorPos(imageTransforms[bgUI.getInt(this.gameObject)], 0.5f).SetEase(Ease.InOutExpo).OnComplete(() => { isProcessing = false; });
        }
        if (orderNames.Count == 0)
        {
            completeOrder();
        }
        DestroyOrderName();

    }
    void AnimateOrder()
    {
        isProcessing = true;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(this.GetComponentInParent<CanvasGroup>().DOFade(1, 0.5f));
        mySequence.Append(this.GetComponent<RectTransform>().DOAnchorPos(imageTransform[0], 0.5f).SetEase(Ease.InOutExpo));
        mySequence.Append(this.GetComponent<RectTransform>().DOAnchorPos(imageTransform[0] - new Vector3(10, 0, 0), 0.5f).SetEase(Ease.InOutExpo));
        switch (bgUI.getInt(this.gameObject))
        {
            case 0:

                mySequence.Append(this.GetComponent<RectTransform>().DOAnchorPos(imageTransform[2], 0.5f).SetEase(Ease.InOutExpo)).OnComplete(() => { isProcessing = false; });
                break;
            case 1:

                mySequence.Append(this.GetComponent<RectTransform>().DOAnchorPos(imageTransform[1], 0.5f).SetEase(Ease.InOutExpo)).OnComplete(() => { isProcessing = false; });
                break;
            case 2:

                mySequence.Append(this.GetComponent<RectTransform>().DOAnchorPos(imageTransform[0], 0.5f).SetEase(Ease.InOutExpo)).OnComplete(() => { isProcessing = false; });
                break;
            default:
                break;
        }
    }
    public void completeOrder()
    {
        this.GetComponent<Image>().sprite = spriteToAssign;

        this.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-29.9f, -145), 0.5f).SetEase(Ease.InOutExpo).OnComplete(() =>
        {
            foreach (var item in bgUI.carManager.carList)
            {
                if (item.GetComponent<CarPath>().speed == 0)
                {

                    break;
                }
            }
            bgUI.orderList.Remove(this.gameObject);
            bgUI.carManager.carList[0].GetComponent<CarPath>().speed = 1f;
            bgUI.carManager.carList[0].GetComponent<CarPath>().isStopped = false;
            bgUI.carManager.carList.RemoveAt(0);
            
            moneyCollect.GenerateMoney(this.transform.childCount * 2);
            Destroy(this.gameObject);

        });

    }
    void DestroyOrderName()
    {
        if (sausageCount == 0)
        {
            orderNames.Remove("Sausage");
        }
        if (eggCount == 0)
        {
            orderNames.Remove("Egg");
        }
        if (cheeseCount == 0)
        {
            orderNames.Remove("Cheese");
        }
        if (milkCount == 0)
        {
            orderNames.Remove("Milk");
        }
        if (meatCount == 0)
        {
            orderNames.Remove("Meat");
        }
        if (tomatoCount == 0)
        {
            orderNames.Remove("Tomato");
        }
        if (carrotCount == 0)
        {
            orderNames.Remove("Carrot");
        }
        if (pumpkinCount == 0)
        {
            orderNames.Remove("Pumpkin");
        }
        if (cornCount == 0)
        {
            orderNames.Remove("Corn");
        }
        if (grapeCount == 0)
        {
            orderNames.Remove("Grapes");
        }

    }

}
