using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Skill", menuName = "Creature/Create Skill")]

public class SkillBase : ScriptableObject
{
    [SerializeField] string skillName;

    [SerializeField] int power;
    [SerializeField] int spCost;

    public string Name{
        get { return skillName; }
    }
    public int Power{
        get { return power; }
    }
    public int SpCost{
        get { return spCost; }
    }

}
