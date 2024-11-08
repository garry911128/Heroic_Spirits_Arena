using System;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public enum CharacterType { Saber, Archer, Paladin};
public enum CharacterAction { IDLE, ATTACK, DEFENSE, USEMINORSKILL, USEUlTIMATE, DEAD }

public abstract partial class Character
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
    public List<Buff> buffs;
    public List<Skill> skills;
    public Ultimate ultimate;
    public Sprite[] idleSprites;
    public Sprite[] attackSprites;
    public Sprite[] defendSprites;
    public CharacterAction currentAction = CharacterAction.IDLE; // ��e���⪬�A

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
        defense = 0;
        armorPenetration = 0;
        critChance = 0;
        damageReduction = 0;
        minorSkillBlock = false;
    }

    public void Attack(Character target)
    {
        bool isCriticalHit = UnityEngine.Random.value < critChance;
        double baseDamage = atk;
        if (isCriticalHit)
        {
            baseDamage *= 1.5;
            Debug.Log($"{name} �i��F�����I");
        }
        double effectiveDefense = Mathf.Max(0, (float)target.defense - (float)armorPenetration);
        double damageMultiplier = 1 - effectiveDefense;
        double effectiveDamage = baseDamage * damageMultiplier * (1 - target.damageReduction);
        int finalDamage = Mathf.Max(1, (int)Mathf.Round((float)effectiveDamage));
        Debug.Log($"{name} �� {target.name} �o�X�F {finalDamage} �I�ˮ`�]����: {isCriticalHit}�^�A{target.name} �{�b�� HP: {target.hp}/{target.maxHp}");
        currentAction = CharacterAction.ATTACK;
        target.TakeDamage(finalDamage);
    }


    public void Defense()
    {
        currentAction = CharacterAction.DEFENSE;
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
        int finalDamage = Mathf.Max(1, (int)Mathf.Round((float)effectiveDamage));
        hp -= finalDamage;
        if (hp < 0)
        {
            hp = 0;
        }
        Debug.Log($"{name} ����F {finalDamage} �I�ˮ`(���m:{defendTurnsCount > 0})�A�ثe HP: {hp}/{maxHp}");
    }

    public void Heal(int value)
    {
        hp += value;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void Update()
    {
        defendTurnsCount--;
        ultimatePoints++;
        foreach (Buff buff in buffs)
        {
            buff.ReduceDuration(this);
        }
    }
}

