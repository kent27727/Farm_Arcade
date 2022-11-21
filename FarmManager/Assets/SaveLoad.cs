using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{

    //Upgradelerin de�erleri , UI daki de�erleri, 
    public List<UnlockAreas> unlockAreas;
    public List<ExpandArea> expandAreas;
    public int unlockedIndex;
    public List<UpgradeSystem> upgradeSystems;
    void Start()
    {
        unlockedIndex = PlayerPrefs.GetInt("UnlockedIndex");
        Debug.Log(unlockedIndex);
        loadAreas();

    }
    void loadAreas()
    {
        int x = 0;
        foreach (var item in unlockAreas)
        {
            if (unlockedIndex > x)
            {
                Debug.Log(item.gameObject.name + "    " + x);
                item.player = GameObject.FindGameObjectWithTag("Player");
                item.characterInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfo>();
                item.UnlockArea();
                x++;
            }
            else
            {
                break;
            }
            if (x == 4)
            {
                expandAreas[0].UnlockArea();
            }
            else if (x == 6)
            {
                expandAreas[1].UnlockArea();
            }
            else if (x == 10)
            {
                expandAreas[2].UnlockArea();
            }
        }
    }
}
