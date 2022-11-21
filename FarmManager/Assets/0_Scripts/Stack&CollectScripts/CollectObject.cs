using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CollectObject : MonoBehaviour
{
    public List<GameObject> stackList = new List<GameObject>();
    public Transform collectPoint;
    public int collectLimit;
    public float collectSpeed, dropSpeed, craftingSpeed;
    public bool isCollecting, isDropping, isCrafting, isUpgrading;
    public Color craftingColor;
    Tweener endTweener = null;
    GameObject currentOBJ;
    public UpgradeSystem upgradeSystem;
    public Image progressUI, innerCircle;
    public Sprite upgradeSprite;
    public Tween tween;
    private void Start()
    {
        progressUI.color = Color.red;
    }
    public bool isOnEPT()
    {
        if (stackList.Count > 0)
        {
            switch (stackList[stackList.Count - 1].name)
            {
                case "Milk(Clone)":
                    return true;
                case "Egg(Clone)":
                    return true;
                case "Sausage(Clone)":
                    return true;
                case "Cheese(Clone)":
                    return true;
                case "Meat(Clone)":
                    return true;
                case "Grapes(Clone)":
                    return false;
                case "Corn(Clone)":
                    return false;
                case "Carrot(Clone)":
                    return false;
                case "Pumpkin(Clone)":
                    return false;
                case "Tomato(Clone)":
                    return false;
                default:
                    return true;
            }
        }
        else
        {
            return true;
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CollectArea"))
        {

            if (stackList.Count < collectLimit)
            {
                StackItem stackTemp = other.gameObject.GetComponent<StackItem>();
                if (stackTemp.stackList.Count != 0)
                {
                    GameObject objTemp = stackTemp.stackList[stackTemp.stackList.Count - 1];
                    if (stackTemp.stackList.Contains(objTemp))
                    {
                        StartCoroutine(collectItem(objTemp, stackTemp));
                    }
                }
            }
        }
        else if (other.CompareTag("DropArea"))
        {
            if (other.gameObject.GetComponent<DropItem>() != null && isOnEPT())
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
            }
            else if (other.gameObject.GetComponent<DropItemEPT>() != null && !isOnEPT())
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
            }

        }
        else if (other.CompareTag("ThrashArea"))
        {

            if (stackList.Count != 0)
            {
                GameObject objTemp = stackList[stackList.Count - 1];
                GameObject thrashCan = other.gameObject;
                if (stackList.Contains(objTemp))
                {
                    deleteItem(objTemp, thrashCan);

                }
            }
        }
        else if (other.CompareTag("CraftArea"))
        {

            if (!isCrafting && stackList.Count < collectLimit)
            {

                craftingSpeed = other.GetComponent<CraftItem>().craftTime;
                isCrafting = true;
                innerCircle.gameObject.SetActive(true);
                innerCircle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = other.GetComponent<CraftItem>().itemIMG;
                progressUI.DOColor(Color.green, craftingSpeed);
                tween = DOVirtual.Float(0, 1f, craftingSpeed, v => progressUI.fillAmount = v).OnComplete(() =>
                {
                    if (isCrafting)
                    {
                        progressUI.fillAmount = 0f;
                        GameObject tempPrefab = Instantiate(other.GetComponent<CraftItem>().prefab);

                        if (!stackList.Contains(tempPrefab))
                        {
                            tempPrefab.transform.position = other.transform.position;
                            float stacklistCount = stackList.Count;
                            stackList.Add(tempPrefab);

                            StartCoroutine(craftItem(tempPrefab));
                            tempPrefab.transform.DORotate(collectPoint.rotation.eulerAngles, collectSpeed).OnComplete(() => { tempPrefab.transform.rotation = collectPoint.rotation; });

                        }

                        innerCircle.gameObject.SetActive(false);
                        isCrafting = false;
                    }

                });

            }
        }
        else if (other.CompareTag("UpgradeArea"))
        {
            if (!other.GetComponent<UpgradeSystem>().upgradePanel.activeInHierarchy && !isUpgrading)
            {

                isUpgrading = true;
                innerCircle.gameObject.SetActive(true);
                innerCircle.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = upgradeSprite;
                progressUI.DOColor(Color.green, craftingSpeed);
                tween = DOVirtual.Float(0, 1f, craftingSpeed, v => progressUI.fillAmount = v).OnComplete(() =>
                {
                    other.GetComponent<UpgradeSystem>().openPanel();
                    innerCircle.gameObject.SetActive(false);
                    isUpgrading = false;
                });
                other.GetComponent<UpgradeSystem>().UpgradePanelSystem.checkButton(other.GetComponent<UpgradeSystem>().UpgradePanelSystem.buttons[1], other.GetComponent<UpgradeSystem>().UpgradePanelSystem.unlcokedAreasInt);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CollectArea"))
        {
            isCollecting = false;
        }
        else if (other.CompareTag("DropArea"))
        {
            isDropping = false;
        }
        else if (other.CompareTag("ThrashArea"))
        {

            isDropping = false;

        }
        else if (other.CompareTag("CraftArea"))
        {
            isCrafting = false;
            tween.Kill();
            progressUI.DOColor(Color.red, craftingSpeed);
            DOVirtual.Float(progressUI.fillAmount, 0f, craftingSpeed / 4, v => progressUI.fillAmount = v).OnComplete(() => { innerCircle.gameObject.SetActive(false); });
        }
        else if (other.CompareTag("UpgradeArea"))
        {
            isUpgrading = false;
            tween.Kill();
            progressUI.DOColor(Color.red, craftingSpeed);
            DOVirtual.Float(progressUI.fillAmount, 0f, craftingSpeed / 4, v => progressUI.fillAmount = v).OnComplete(() => { innerCircle.gameObject.SetActive(false); });
            other.GetComponent<UpgradeSystem>().upgradePanel.SetActive(false);
        }
    }
    IEnumerator craftItem(GameObject obj)
    {
        currentOBJ = obj;
        yield return new WaitForSeconds(collectSpeed);
        obj.transform.SetParent(collectPoint);
        obj.transform.position = new Vector3(collectPoint.position.x, collectPoint.position.y + (float)stackList.Count / 10, collectPoint.position.z);
        obj.transform.rotation = collectPoint.rotation;
        currentOBJ = null;
    }
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
                stackItem.isSomeoneIn = true;
                stackItem.stackList.Add(obj);
                stackList.Remove(obj);
                obj.transform.DOMove(stackItem.localList[indexofList].transform.position, dropSpeed).OnComplete(() =>
            {
                obj.transform.position = stackItem.localList[indexofList].transform.position;
                obj.transform.SetParent(stackItem.localList[indexofList].transform);
                isDropping = false;

            });
                obj.transform.DORotate(stackItem.localList[indexofList].transform.rotation.eulerAngles, dropSpeed);
            }


        }

    }
    void dropItemEPT(GameObject obj, DropItemEPT stackItem)
    {
        if (!isDropping)
        {
            isDropping = true;
            bool ishaveSpace = false;
            stackItem.stackList.Add(obj);
            stackList.Remove(obj);
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
                Debug.Log(item.transform.childCount + " " + item);

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
                stackItem.isSomeoneIn = true;
                obj.transform.DOMove(stackItem.localList[indexofList].transform.position, dropSpeed).OnComplete(() =>
                {
                    obj.transform.position = stackItem.localList[indexofList].transform.position;
                    obj.transform.SetParent(stackItem.localList[indexofList].transform);
                    isDropping = false;

                });
                obj.transform.DORotate(stackItem.localList[indexofList].transform.rotation.eulerAngles, dropSpeed);


            }


        }

    }
    void deleteItem(GameObject obj, GameObject thrash)
    {
        if (!isDropping)
        {
            stackList.Remove(obj);
            isDropping = true;
            obj.transform.DOMove(thrash.transform.GetChild(1).transform.position, dropSpeed).OnComplete(() =>
            {
                Destroy(obj);

                isDropping = false;

            });


        }
    }

}
