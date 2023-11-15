using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public SkillBase Base { get; set;}

    public int Sp { get; set; }

    public Skill(SkillBase pBase){
        Base = pBase;
        Sp = pBase.SpCost;
    }
}
