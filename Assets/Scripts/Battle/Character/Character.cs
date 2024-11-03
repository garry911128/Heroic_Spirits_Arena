using System;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    public List<Buff> buff; //buff list, which is used to store the buff that the character has or is affected by
    public List<Skill> skills;
    public Ultimate ultimate;
    public Sprite sprite;
    public Animation animation;
    public Character(string name, int hp, int atk, List<Skill> skills, Ultimate ultimate)
    {
        this.name = name;
        maxHp = hp;
        this.hp = hp;
        this.atk = atk;
        buff = new List<Buff>();
        defendTurnsCount = 0;
        ultimatePoints = 0;
        this.ultimate = ultimate;
        isStunned = false;
    }

    public void attack(Character target)
    {
        target.takeDamage(atk);
    }

    public void takeDamage(int damage)
    {
        if (defendTurnsCount > 0)
        {
            hp -= (int)Math.Round(damage * 0.5);
        }
        else
        {
            hp -= damage;
        }
    }

    public void heal(int value)
    {
        hp += value;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void activateUltimate(Character target)
    {
        
    }

    public void update()
    {
        defendTurnsCount--;

    }
}
