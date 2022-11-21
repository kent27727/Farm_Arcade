using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OrderManager : MonoBehaviour
{
    [Header("TEXTS")]
    public Text[] mainSlidertext;
    public Text[] buttonText;

    [Header("ELEMENTS")]
    public int[] theNumber;

    [Header("SLIDER")]
    public Slider[] mainSlider;

    [Header("BUTTON")]
    public Button[] buttons;
    public CharacterInfo characterInfo;
    public UpgradeSystem upgradeSystem;

    public int unlcokedAreasInt = 0;

    public bool isEP2;



    
    private void Update()
    {


        checkButton(buttons[0], unlcokedAreasInt);
        if (!isEP2)
        {
            for (int i = 0; i < mainSlidertext.Length; i++)
            {
                PlayerPrefs.SetInt("UpgradeEpOne" + i.ToString(), int.Parse(mainSlidertext[i].text));
            }
        }
        else
        {
            for (int i = 0; i < mainSlidertext.Length; i++)
            {
                if (i !=3&& i != 4 && i != 5)
                {
                    PlayerPrefs.SetInt("UpgradeEpTwo" + i.ToString(), int.Parse(mainSlidertext[i].text));
                }
                
            }
        }
        

    }
    public void checkButton(Button checkButton, int unlockINT)
    {
        if (!isEP2)
        {
            for (int i = 0; i < 4; i++)
            {
                bool x = characterInfo.unlockedAreas[i];
                if (x)
                {
                    unlockINT += 1;
                }
            }
        }
        else if (isEP2)
        {
            for (int i = 0; i < 4; i++)
            {
                bool x = characterInfo.unlockedAreasEP2[i];
                if (x)
                {
                    unlockINT += 1;
                }
            }
        }

        switch (unlockINT)
        {
            case 0:
                if (int.Parse(mainSlidertext[0].text) < 1)
                {
                    checkButton.interactable = true;
                    break;
                }
                else
                {
                    checkButton.interactable = false;
                    break;
                }
            case 1:
                if (int.Parse(mainSlidertext[0].text) < 2)
                {
                    checkButton.interactable = true;
                    break;
                }
                else
                {
                    checkButton.interactable = false;
                    break;
                }
            case 2:
                if (int.Parse(mainSlidertext[0].text) < 3)
                {
                    checkButton.interactable = true;
                    break;
                }
                else
                {
                    checkButton.interactable = false;
                    break;
                }
            case 3:
                if (int.Parse(mainSlidertext[0].text) < 4)
                {
                    checkButton.interactable = true;
                    break;
                }
                else
                {
                    checkButton.interactable = false;
                    break;
                }
            case 4:
                if (int.Parse(mainSlidertext[0].text) < 5)
                {
                    checkButton.interactable = true;
                    break;
                }
                else
                {
                    checkButton.interactable = false;
                    break;
                }
            default:
                break;
        }

    }
    void Start()
    {
        characterInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfo>();
        foreach (int value in theNumber)
        {
            print(value);
        }
        theNumber = new int[7];
        theNumber[0] = 20;
        theNumber[1] = 40;
        theNumber[2] = 60;
        theNumber[3] = 80;
        theNumber[4] = 100;
        theNumber[5] = 120;
        theNumber[6] = 140;

        if (!isEP2)
        {
            for (int i = 0; i < mainSlidertext.Length; i++)
            {
                mainSlidertext[i].text = PlayerPrefs.GetInt("UpgradeEpOne" + i.ToString()).ToString();
            }
            upgradeSystem.upgradeCountListConverter();
        }
        else
        {
            for (int i = 0; i < mainSlidertext.Length; i++)
            {
                if (i != 3 && i != 4 && i != 5)
                {
                    mainSlidertext[i].text = PlayerPrefs.GetInt("UpgradeEpTwo" + i.ToString()).ToString();
                }
                
            }
            upgradeSystem.upgradeCountListConverterEP2();
        }

    }

    public void upgradeOne(int val)
    {
        if (int.Parse(buttonText[0].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[0].text);

                    buttonText[0].text = theNumber[2].ToString();
                    mainSlider[0].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[0].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }

                    buttons[0].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[0].onClick.AddListener(delegate { upgradeOne(2); });
                    break;
                case 2:
                    characterInfo.Cash -= int.Parse(buttonText[0].text);
                    Debug.Log(int.Parse(buttonText[0].text));
                    buttonText[0].text = theNumber[4].ToString();
                    mainSlider[0].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[0].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    Debug.Log("2.Çalıştı");
                    buttons[0].interactable = false;
                    break;
            }
        }

    }

    public void upgradeTwo(int val)
    {
        if (int.Parse(buttonText[0].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[0].text);
                    checkButton(buttons[0], unlcokedAreasInt);
                    buttonText[0].text = theNumber[0].ToString();
                    mainSlider[0].DOValue(20, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[0].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();

                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[0].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[0].onClick.AddListener(delegate { upgradeTwo(2); });
                    break;
                case 2:
                    checkButton(buttons[0], unlcokedAreasInt);
                    characterInfo.Cash -= int.Parse(buttonText[0].text);
                    buttonText[0].text = theNumber[1].ToString();
                    mainSlider[0].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[0].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[0].onClick.RemoveAllListeners();
                    buttons[0].onClick.AddListener(delegate { upgradeTwo(3); });
                    break;
                case 3:
                    characterInfo.Cash -= int.Parse(buttonText[0].text);
                    checkButton(buttons[0], unlcokedAreasInt);
                    buttonText[0].text = theNumber[2].ToString();
                    mainSlider[0].DOValue(60, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[0].text = "3";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[0].onClick.RemoveAllListeners();
                    buttons[0].onClick.AddListener(delegate { upgradeTwo(4); });
                    break;
                case 4:
                    characterInfo.Cash -= int.Parse(buttonText[0].text);
                    checkButton(buttons[0], unlcokedAreasInt);
                    buttonText[0].text = theNumber[3].ToString();
                    mainSlider[0].DOValue(80, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[0].text = "4";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[0].onClick.RemoveAllListeners();
                    buttons[0].onClick.AddListener(delegate { upgradeTwo(5); });
                    break;
                case 5:
                    checkButton(buttons[0], unlcokedAreasInt);
                    characterInfo.Cash -= int.Parse(buttonText[0].text);
                    buttonText[0].text = theNumber[4].ToString();
                    mainSlider[0].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[0].text = "5";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[0].interactable = false;
                    break;
            }
        }

    }
    public void upgradeThree(int val)
    {
        if (int.Parse(buttonText[1].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[1].text);
                    buttonText[1].text = theNumber[0].ToString();
                    mainSlider[1].DOValue(20, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[1].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[1].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[1].onClick.AddListener(delegate { upgradeThree(2); });
                    break;
                case 2:
                    characterInfo.Cash -= int.Parse(buttonText[1].text);
                    buttonText[1].text = theNumber[1].ToString();
                    mainSlider[1].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[1].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[1].onClick.RemoveAllListeners();
                    buttons[1].onClick.AddListener(delegate { upgradeThree(3); });
                    break;
                case 3:
                    characterInfo.Cash -= int.Parse(buttonText[1].text);
                    buttonText[1].text = theNumber[2].ToString();
                    mainSlider[1].DOValue(60, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[1].text = "3";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[1].onClick.RemoveAllListeners();
                    buttons[1].onClick.AddListener(delegate { upgradeThree(4); });
                    break;
                case 4:
                    characterInfo.Cash -= int.Parse(buttonText[1].text);
                    buttonText[1].text = theNumber[3].ToString();
                    mainSlider[1].DOValue(80, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[1].text = "4";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[1].onClick.RemoveAllListeners();
                    buttons[1].onClick.AddListener(delegate { upgradeThree(5); });
                    break;
                case 5:
                    characterInfo.Cash -= int.Parse(buttonText[1].text);
                    buttonText[1].text = theNumber[4].ToString();
                    mainSlider[1].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[1].text = "5";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[1].interactable = false;
                    break;
            }
        }

    }

    public void upgradeFourth(int val)
    {
        if (int.Parse(buttonText[2].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[2].text);
                    buttonText[2].text = theNumber[2].ToString();
                    mainSlider[2].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[2].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[2].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[2].onClick.AddListener(delegate { upgradeFourth(2); });
                    break;
                case 2:
                    characterInfo.Cash -= int.Parse(buttonText[2].text);
                    buttonText[2].text = theNumber[4].ToString();
                    mainSlider[2].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[2].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    else if (isEP2)
                    {
                        upgradeSystem.upgradeCountListConverterEP2();
                    }
                    buttons[2].interactable = false;
                    break;
            }
        }

    }
    public void upgradeFifth(int val)
    {
        if (int.Parse(buttonText[3].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[3].text);
                    buttonText[3].text = theNumber[0].ToString();
                    mainSlider[3].DOValue(10, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[3].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[3].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[3].onClick.AddListener(delegate { upgradeFifth(2); });
                    break;
                case 2:
                    characterInfo.Cash -= int.Parse(buttonText[3].text);
                    buttonText[3].text = theNumber[1].ToString();
                    mainSlider[3].DOValue(30, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[3].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[3].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[3].onClick.AddListener(delegate { upgradeFifth(3); });
                    break;
                case 3:
                    characterInfo.Cash -= int.Parse(buttonText[3].text);
                    buttonText[3].text = theNumber[2].ToString();
                    mainSlider[3].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[3].text = "3";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[3].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[3].onClick.AddListener(delegate { upgradeFifth(4); });
                    break;
                case 4:
                    characterInfo.Cash -= int.Parse(buttonText[3].text);
                    buttonText[3].text = theNumber[3].ToString();
                    mainSlider[3].DOValue(50, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[3].text = "4";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[3].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[3].onClick.AddListener(delegate { upgradeFifth(5); });
                    break;
                case 5:
                    characterInfo.Cash -= int.Parse(buttonText[3].text);
                    buttonText[3].text = theNumber[4].ToString();
                    mainSlider[3].DOValue(70, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[3].text = "5";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[3].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[3].onClick.AddListener(delegate { upgradeFifth(6); });
                    break;
                case 6:
                    characterInfo.Cash -= int.Parse(buttonText[3].text);
                    buttonText[3].text = theNumber[5].ToString();
                    mainSlider[3].DOValue(80, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[3].text = "6";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[3].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[3].onClick.AddListener(delegate { upgradeFifth(7); });
                    break;
                case 7:
                    characterInfo.Cash -= int.Parse(buttonText[3].text);
                    buttonText[3].text = theNumber[6].ToString();
                    mainSlider[3].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[3].text = "7";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[3].interactable = false;
                    break;
            }
        }

    }

    public void upgradeSixth(int val)
    {
        if (int.Parse(buttonText[4].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[4].text);
                    buttonText[4].text = theNumber[4].ToString();
                    mainSlider[4].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[4].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[4].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[4].onClick.AddListener(delegate { upgradeSixth(2); });
                    break;
                case 2:
                    characterInfo.Cash -= int.Parse(buttonText[4].text);
                    buttonText[4].text = theNumber[5].ToString();
                    mainSlider[4].DOValue(60, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[4].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[4].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[4].onClick.AddListener(delegate { upgradeSixth(3); });
                    break;
                case 3:
                    characterInfo.Cash -= int.Parse(buttonText[4].text);
                    buttonText[4].text = theNumber[6].ToString();
                    mainSlider[4].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[4].text = "3";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[4].interactable = false;
                    break;
            }
        }

    }

    public void upgradeSeventh(int val)
    {
        if (int.Parse(buttonText[5].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[5].text);
                    buttonText[5].text = theNumber[2].ToString();
                    mainSlider[5].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[5].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }

                    buttons[5].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[5].onClick.AddListener(delegate { upgradeSeventh(2); });
                    break;
                case 2:
                    characterInfo.Cash -= int.Parse(buttonText[5].text);
                    buttonText[5].text = theNumber[3].ToString();
                    mainSlider[5].DOValue(60, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[5].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }

                    buttons[5].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[5].onClick.AddListener(delegate { upgradeSeventh(3); });
                    break;
                case 3:
                    characterInfo.Cash -= int.Parse(buttonText[5].text);
                    buttonText[5].text = theNumber[4].ToString();
                    mainSlider[5].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[5].text = "3";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }

                    buttons[5].interactable = false;
                    break;
            }
        }

    }

    public void upgradeEight(int val)
    {
        if (int.Parse(buttonText[6].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[6].text);
                    buttonText[6].text = theNumber[2].ToString();
                    mainSlider[6].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[6].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }

                    buttons[6].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[6].onClick.AddListener(delegate { upgradeEight(2); });
                    break;
                case 2:
                    characterInfo.Cash -= int.Parse(buttonText[6].text);
                    buttonText[6].text = theNumber[4].ToString();
                    mainSlider[6].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[6].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }

                    buttons[6].interactable = false;
                    break;
            }
        }

    }

    public void upgradeNinth(int val)
    {
        if (int.Parse(buttonText[7].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[7].text);
                    buttonText[7].text = theNumber[2].ToString();
                    mainSlider[7].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[7].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[7].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[7].onClick.AddListener(delegate { upgradeNinth(2); });
                    break;
                case 2:
                    characterInfo.Cash -= int.Parse(buttonText[7].text);
                    buttonText[7].text = theNumber[4].ToString();
                    mainSlider[7].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[7].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[7].interactable = false;
                    break;
            }
        }

    }


    public void upgradTten(int val)
    {
        if (int.Parse(buttonText[8].text) <= characterInfo.Cash)
        {
            switch (val)
            {
                case 1:
                    characterInfo.Cash -= int.Parse(buttonText[8].text);
                    buttonText[8].text = theNumber[2].ToString();
                    mainSlider[8].DOValue(40, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[8].text = "1";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[8].onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                    buttons[8].onClick.AddListener(delegate { upgradTten(2); });
                    break;
                case 2:
                    characterInfo.Cash -= int.Parse(buttonText[8].text);
                    buttonText[8].text = theNumber[4].ToString();
                    mainSlider[8].DOValue(100, 2).SetEase(Ease.OutElastic);
                    mainSlidertext[8].text = "2";
                    if (!isEP2)
                    {
                        upgradeSystem.upgradeCountListConverter();
                    }
                    buttons[8].interactable = false;
                    break;
            }
        }

    }

}
