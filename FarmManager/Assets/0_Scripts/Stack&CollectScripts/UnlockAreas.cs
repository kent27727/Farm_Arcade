using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UnlockAreas : MonoBehaviour
{
    public int cost;
    float unlockTime = 0.3f;
    public bool unlocked;
    float passedTime = 0;
    public Image fillerImage;
    public GameObject area, closedArea;
    float X;
    public string nameOfArea;
    public bool unlockable;
    public CharacterInfo characterInfo;
    public GameObject unlockArea;
    TextMeshPro costCash;
    public UnlockAreas nextArea;
    public GameObject player;
    Tween tween;

    private void Start()
    {
        Debug.Log(unlockArea.transform.GetChild(2).gameObject);
        costCash = unlockArea.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
        characterInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfo>();
        X = cost / 10f;
    }
    Tween myTween;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && !unlocked && unlockable)
        {
            player = other.gameObject;


            if (passedTime >= unlockTime && player.GetComponent<CharacterInfo>().Cash >= 10)
            {
                passedTime = 0;
                player.GetComponent<CharacterInfo>().Cash -= 10;
                cost -= 10;
                unlockArea.transform.DOScale(new Vector3(0.65f, 0.65f, 0.65f), unlockTime / 4).OnComplete(() => { unlockArea.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), unlockTime / 4); });
                Debug.Log(1f / (10f / cost));
                tween = DOVirtual.Float(fillerImage.fillAmount, fillerImage.fillAmount + 1f / X, unlockTime / 2, v => fillerImage.fillAmount = v);
            }
            else if (passedTime < unlockTime)
            {
                passedTime += Time.deltaTime;

            }


        }

    }
    void UnlockBool(string STR)
    {
        switch (STR)
        {
            case "Cow":
                characterInfo.unlockedAreas[0] = true;  
                break;
            case "Chicken":
                characterInfo.unlockedAreas[1] = true;
                break;
            case "Goat":
                characterInfo.unlockedAreas[2] = true;
                break;
            case "Sheep":
                characterInfo.unlockedAreas[3] = true;
                break;
            case "Pizza":
                characterInfo.unlockedAreas[4] = true;
                break;
            case "HotDog":
                characterInfo.unlockedAreas[5] = true;
                break;
            case "Corn":
                characterInfo.unlockedAreasEP2[0] = true;
                break;
            case "Carrot":
                characterInfo.unlockedAreasEP2[1] = true;
                break;
            case "Tomato":
                characterInfo.unlockedAreasEP2[2] = true;
                break;
            case "Pumpkin":
                characterInfo.unlockedAreasEP2[3] = true;
                break;
            case "Cake":
                characterInfo.unlockedAreasEP2[4] = true;
                break;
            case "Juice":
                characterInfo.unlockedAreasEP2[5] = true;
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !unlocked)
        {
            tween.Kill();
            passedTime = 0;
        }
    }
    private void Update()
    {
        if (cost == 0)
        {
            UnlockArea();
        }
        costCash.text = cost.ToString();
    }
    public void UnlockArea()
    {
        unlocked = true;
        area.SetActive(true);
        UnlockBool(nameOfArea);
        characterInfo.unlockLocks();
        characterInfo.addUnlockedItem();
        
        if (nextArea != null)
        {
            nextArea.unlockable = true;
            nextArea.unlockArea.SetActive(true);
        }
        characterInfo.SaveUnlockBool();
        closedArea.SetActive(false);
        fillerImage.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        unlockArea.SetActive(false);
        this.gameObject.GetComponent<UnlockAreas>().enabled = false;


    }
    
}
