using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorSkill : Skill
{
    public MinorSkill(string name, string description, int cooldown, List<SkillType> skillTypes, List<Buff> buffs)
        : base(name, description, cooldown, skillTypes, buffs)
    {
    }

    public override void Activate(Character user, Character target)
    {
        if(IsAvailable())
        {
            foreach(SkillType skillType in skillTypes)
            {
                switch (skillType)
                {
                    case SkillType.Attack:
                        user.Attack(target);
                        break;
                    case SkillType.Buff:
                        foreach (Buff buff in buffs)
                        {
                            buff.Apply(user, target);
                        }
                        break;
                }
            }
            StartCooldown();
        }
    }
}

