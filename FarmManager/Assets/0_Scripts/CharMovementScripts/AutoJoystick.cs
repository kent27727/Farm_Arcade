using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class AutoJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform joyBg;
    public ManagerJoystick joy;

    public void OnPointerDown(PointerEventData ped)
    {
        joyBg.gameObject.SetActive(true);
        Vector2 diff = ped.position - (Vector2)GetComponent<RectTransform>().position;
        Vector2 modifedDiff = diff * (1f / GetComponentInParent<Canvas>().scaleFactor);
        joyBg.localPosition = modifedDiff;
    }
     public void OnDrag(PointerEventData ped)
    {
        joy.OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        joyBg.gameObject.SetActive(false);
        joy.ResetJoy();
    }
}
