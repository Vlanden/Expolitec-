using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public CreateStats Base { get; set; }
    public int Level { get; set; }
    
    public int HP { get; set; }

    public List<Skill> Skills { get; set; }

    public Creature(CreateStats pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        HP = MaxHp;

        Skills = new List<Skill>();
        foreach (var skill in Base.LearnableSkills)
        {
            if(skill.Level <= Level){
                Skills.Add(new Skill(skill.Base));
            }
        }
    }

    public int Attack{
        get { return Mathf.FloorToInt(Base.Attack + Level); }
    }

    public int Defense{
        get { return Mathf.FloorToInt(Base.Defense + Level); }
    }

    public int MaxHp{
        get { return Mathf.FloorToInt(Base.MaxHp + (Level*2)); }
    }

    public int Sp{
        get { return Mathf.FloorToInt(Base.Sp + Level); }
    }

    public bool TakeDamage(Skill skill, Creature attacker){
        float d = (skill.Base.Power * attacker.Attack) - Defense/2;
        int damage = Mathf.FloorToInt(d);

        if(damage < 0){
            damage = 0;
        }

        HP -= damage;
        if(HP <= 0){
            HP = 0;
            return true;
        }else{
            return false;
        }
    }

    public Skill GetRandomSkill(){
        int r = Random.Range(0,Skills.Count);
        return Skills[r];
    }
}
