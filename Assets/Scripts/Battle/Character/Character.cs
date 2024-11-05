using System;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public enum CharacterType { Saber, Archer, Paladin};
public enum CharacterAction { IDEL, ATTACK, DEFENSE, USEMINORSKILL, USEUlTIMATE, DEAD}

public abstract class Character
{
    public string name { get; set; }
    public int maxHp { get; set; }
    public int hp { get; set; }
    public int atk { get; set; }
    public int defendTurnsCount { get; set; }
    public int ultimatePoints { get; set; }
    public double defense { get; set; }
    public double armorPenetration { get; set; }
    public double critChance { get; set; }
    public double damageReduction { get; set; }
    public bool minorSkillBlock { get; set; }
    public bool isStunned { get; set; }
    public List<Buff> buffs; //buff list, which is used to store the buff that the character has or is affected by
    public List<Skill> skills;
    public Ultimate ultimate;
    public Sprite sprite;
    public Animator animator;
    public Character(string name, int hp, int atk, List<Skill> skills, Ultimate ultimate)
    {
        this.name = name;
        maxHp = hp;
        this.hp = hp;
        this.atk = atk;
        buffs = new List<Buff>();
        defendTurnsCount = 0;
        ultimatePoints = 0;
        this.skills = skills;
        this.ultimate = ultimate;
        isStunned = false;
    }

    public void Attack(Character target)
    {
        target.TakeDamage(atk);
    }

    public void Defense()
    {
        defendTurnsCount = 2;
    }

    public void TakeDamage(int baseDamage)
    {
        double effectiveDamage = baseDamage;
        if (defendTurnsCount > 0)
        {
            effectiveDamage *= 0.5;
        }
        effectiveDamage *= (1 - damageReduction);
        effectiveDamage *= (1 - defense);
        int finalDamage = Mathf.Max(1, (int)Math.Round(effectiveDamage));
        hp -= finalDamage;
        if (hp < 0)
        {
            hp = 0;
        }
        Debug.Log($"{name} 受到了 {finalDamage} 點傷害，目前 HP: {hp}/{maxHp}");
    }

    public void Heal(int value)
    {
        hp += value;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void ActivateSkill(int skillIndex, Character target)
    {
        skills[skillIndex].Activate(this, target);
    }
    public void ActivateUltimate(Character target)
    {
        ultimate.Activate(this, target);
    }

    public List<string> GetDescriptions()
    {
        List<string> desciptions = new List<string>();
        desciptions.Add(GetActionDescription(CharacterAction.ATTACK));
        desciptions.Add(GetActionDescription(CharacterAction.DEFENSE));
        desciptions.Add(GetActionDescription(CharacterAction.USEMINORSKILL));
        desciptions.Add(GetActionDescription(CharacterAction.USEUlTIMATE));
        return desciptions;
    }

    public string GetActionDescription(CharacterAction action)
    {
        string description;
        switch (action)
        {
            case CharacterAction.ATTACK:
                description = $"Attack: {atk}";
                break;

            case CharacterAction.DEFENSE:
                description = "Defense: Reduces damage taken by 50% for 2 turns.(include this turn)";
                break;

            case CharacterAction.USEMINORSKILL:
                description = "Minor Skills:\n";
                foreach (Skill skill in skills)
                {
                    description += $"{skill.name}: {skill.description}\n";
                }
                break;

            case CharacterAction.USEUlTIMATE:
                description = $"ultpoint:{ultimatePoints}, cost:{ultimate.ultimateCost}\nUltimate:\n{ultimate.name}: {ultimate.description}";
                break;

            default:
                description = "Idle";
                break;
        }
        return description;
    }

    public void update()
    {
        defendTurnsCount--;
        ultimatePoints ++;
        foreach (Buff buff in buffs)
        {
            buff.ReduceDuration(this);
        }
    }
}
