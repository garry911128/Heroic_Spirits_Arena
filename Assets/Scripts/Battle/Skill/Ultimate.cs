using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ultimate : Skill
{
    public int ultimateCost { get; set; } // the cost of the ultimate skill()
    public int value;

    public Ultimate(string name, string description, int cooldown, List<SkillType> skillTypes, List<Buff> buffs, int value, int cost)
        : base(name, description, cooldown, skillTypes, buffs)
    {
        this.value = value;
        ultimateCost = cost;
    }


    public override void Activate(Character user, Character target)
    {
        if(user.ultimatePoints >= ultimateCost)
        {
            user.ultimatePoints -= ultimateCost;
            Debug.Log($"use{this.name}:{this.description}");
            foreach (SkillType skillType in skillTypes)
            {
                switch (skillType)
                {
                    case SkillType.Attack:
                        target.TakeDamage(value);
                        break;
                    case SkillType.Buff:
                        foreach (Buff buff in buffs)
                        {
                            buff.Apply(user);
                        }
                        break;
                }
            }
        }
    }
}
