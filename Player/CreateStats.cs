using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Creature", menuName = "Creature/Create Creature")]

public class CreateStats : ScriptableObject
{
    [SerializeField] string critName;

    [SerializeField] Sprite face;

    //Stats
    [SerializeField] int maxHp;
    [SerializeField] int hp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int sp;

    [SerializeField] List<LearnableSkill> learnableSkills;

    public string Name{
        get { return critName; }
    }
    public Sprite Face{
        get { return face; }
    }
    public int MaxHp{
        get { return maxHp; }
    }
    public int Hp{
        get { return hp; }
    }
    public int Attack{
        get { return attack; }
    }
    public int Defense{
        get { return defense; }
    }
    public int Sp{
        get { return sp; }
    }
    public List<LearnableSkill> LearnableSkills{
        get { return learnableSkills; }
    }
}

[System.Serializable]
public class LearnableSkill{
    [SerializeField] SkillBase skillBase;
    [SerializeField] int level;

    public SkillBase Base{
        get { return skillBase; }
    }
    public int Level{
        get { return level; }
    }
}