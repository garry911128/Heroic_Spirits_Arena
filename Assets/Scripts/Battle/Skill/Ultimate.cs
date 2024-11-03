using System.Collections.Generic;

public class Ultimate : Skill
{
    public int ultimateCost { get; set; } // the cost of the ultimate skill()
    public int value;

    public Ultimate(string name, int cooldown, List<SkillType> skillTypes, List<Buff> buffs, int value, int cost)
        : base(name, cooldown, skillTypes, buffs)
    {
        this.value = value;
        ultimateCost = cost;
    }


    public override void Activate(Character user, Character target)
    {
        if(user.ultimatePoints >= ultimateCost)
        {
            foreach (SkillType skillType in skillTypes)
            {
                switch (skillType)
                {
                    case SkillType.Attack:
                        target.takeDamage(value);
                        break;
                    case SkillType.Buff:
                        foreach (Buff buff in buffs)
                        {
                            buff.Apply(target);
                        }
                        break;
                }
            }
        }
    }
}
