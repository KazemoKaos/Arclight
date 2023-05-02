using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> achievementList;

    void Start()
    {
        initAchievement();
    }

    void initAchievement()
    {
        achievementList[0].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
            AchievementManager._achieve01.ToString() + "/5";
        achievementList[1].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
            AchievementManager._achieve02.ToString() + "/1";
        achievementList[2].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
            AchievementManager._achieve03.ToString() + "/5000";
        achievementList[3].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
            AchievementManager._achieve04.ToString() + "/5000";
        achievementList[4].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
            AchievementManager._achieve05.ToString() + "/30";
        achievementList[5].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
            AchievementManager._achieve06.ToString() + "/30";
    }
}
