using UnityEngine;
public enum BuffType
{
    ArmorPenetration,
    CriticalChance,
    MinorSkillBlock,
    IncreaseDefense,
    DamageReduction,
    AttackBoost,        // 新增攻擊加成
    HpRefill            // 新增生命補充
}

public class Buff
{
    public string name;
    public int duration; // buff duration(my buff turn)
    public BuffType effectType; // effect type
    public int value; // effect
    public bool isApplied; // if the buff is applied

    public Buff(string name, int duration, BuffType effectType, int value)
    {
        this.name = name;
        this.duration = duration;
        this.effectType = effectType;
        this.value = value;
        isApplied = false;
    }

    public void Apply(Character character)
    {
        if (!isApplied)
        {
            isApplied = true;
            switch (effectType)
            {
                case BuffType.DamageReduction:
                    character.damageReduction += value;
                    break;
                case BuffType.CriticalChance:
                    character.critChance += value;
                    break;
                case BuffType.ArmorPenetration:
                    character.armorPenetration += value;
                    break;
                case BuffType.MinorSkillBlock:
                    character.minorSkillBlock = true;
                    break;
                case BuffType.HpRefill:
                    character.Heal(value);
                    break;
            }
        }
    }

    public void Remove(Character character)
    {
        if (isApplied)
        {
            isApplied = false;
            switch (effectType)
            {
                case BuffType.DamageReduction:
                    character.damageReduction -= value;
                    break;
                case BuffType.CriticalChance:
                    character.critChance -= value;
                    break;
                case BuffType.ArmorPenetration:
                    character.armorPenetration -= value;
                    break;
                case BuffType.MinorSkillBlock:
                    character.minorSkillBlock = false;
                    break;
            }
        }
    }

    public void ReduceDuration()
    {
        if (duration > 0)
        {
            duration--;
        }
    }

    public bool IsExpired()
    {
        return duration <= 0;
    }
}

