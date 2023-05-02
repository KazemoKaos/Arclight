using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AchievementSO : ScriptableObject
{
    public string achievementName;
    [TextArea()]public string achievementDescription;
    public int achievementGoal;
}
