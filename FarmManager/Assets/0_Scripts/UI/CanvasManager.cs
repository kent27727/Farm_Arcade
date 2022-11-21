using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject[] canvasManager;


    public void Worker2Player()
    {
        canvasManager[0].SetActive(true);
        canvasManager[1].SetActive(false);
    }

    public void Player2Worker()
    {
        canvasManager[0].SetActive(false);
        canvasManager[1].SetActive(true);
    }

    public void closeAll()
    {
        canvasManager[2].SetActive(false);
    }
}