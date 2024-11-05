using UnityEngine;
using System.Collections.Generic;

public enum SkillType { Attack, Buff};
public abstract class Skill
{
    public string name { get; set; }
    public string description { get; set; }
    public int cooldown { get; set; } // cooldown time(turn)
    public int currentCooldown { get; set; } // current cooldown turn
    public List<SkillType> skillTypes;// skill types
    public List<Buff> buffs; // the buff of the skill (if any)

    public Skill(string name, string description, int cooldown, List<SkillType> skillTypes, List<Buff> buffs)
    {
        this.name = name;
        this.description = description;
        this.cooldown = cooldown;
        currentCooldown = 0;
        this.skillTypes = skillTypes;
        this.buffs = buffs;
    }

    public abstract void Activate(Character user, Character target);

    public void StartCooldown()
    {
        currentCooldown = cooldown;
    }

    public void ReduceCooldown()
    {
        if (currentCooldown > 0)
        {
            currentCooldown--;
        }
    }

    public bool IsAvailable()
    {
        return currentCooldown <= 0;
    }
}
