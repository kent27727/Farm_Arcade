using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ExpandArea : MonoBehaviour
{
    public int cost;
    float unlockTime = 0.3f;
    public bool unlocked;
    float passedTime = 0;
    public Image fillerImage;
    public GameObject area, closedArea,unlockArea;
    public GameObject[] allArea;
    float X;
    public int y = 0;
    public TextMeshPro costCash;
    public GameObject episode2Fence;
    Tween tween;

    private void Start()
    {
        X = cost / 10f;
    }
   
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && !unlocked && y == allArea.Length)
        {
            GameObject player = other.gameObject;
            if (player.GetComponent<CharacterInfo>().Cash >= cost)
            {

                if (passedTime >= unlockTime)
                {
                    passedTime = 0;
                    player.GetComponent<CharacterInfo>().Cash -= 10;
                    cost -= 10;
                    unlockArea.transform.DOScale(new Vector3(0.65f,0.65f,0.65f),unlockTime/4).OnComplete(()=>{unlockArea.transform.DOScale(new Vector3(0.5f,0.5f,0.5f),unlockTime/4);});
                    Debug.Log(1f / (10f / cost));
                    tween = DOVirtual.Float(fillerImage.fillAmount, fillerImage.fillAmount + 1f / X, unlockTime / 2, v => fillerImage.fillAmount = v);


                }
                else if (passedTime < unlockTime)
                {
                    passedTime += Time.deltaTime;

                }
            }

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
        int x=0;
        foreach (var item in allArea)
        {
            if (!item.activeInHierarchy)
            {
                break;
            }
            else
            {
                x++;
            }

        }
        if (cost == 0)
        {
            UnlockArea();
        }
        if (this.isActiveAndEnabled)
        {
            costCash.text = cost.ToString();
        }
        
        y = x;
    }
    public void UnlockArea()
    {
        unlocked = true;
        area.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        fillerImage.gameObject.SetActive(false);
        if (episode2Fence !=null)
        {
            episode2Fence.SetActive(false);
        }
        this.enabled = false;
        

    }
}
