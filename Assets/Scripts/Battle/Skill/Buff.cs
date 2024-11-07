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

    public void Apply(Character user, Character target)
    {
        if (!isApplied)
        {
            isApplied = true;
            switch (effectType)
            {
                case BuffType.DamageReduction:
                    user.buffs.Add(this);
                    user.damageReduction += value;
                    break;
                case BuffType.CriticalChance:
                    user.buffs.Add(this);
                    user.critChance += value;
                    break;
                case BuffType.ArmorPenetration:
                    user.buffs.Add(this);
                    user.armorPenetration += value;
                    break;
                case BuffType.MinorSkillBlock:
                    target.buffs.Add(this);
                    target.minorSkillBlock = true;
                    break;
                case BuffType.HpRefill:
                    user.buffs.Add(this);
                    user.Heal(value);
                    break;
                case BuffType.AttackBoost:
                    user.buffs.Add(this);
                    user.atk += value;
                    break;
            }
        }
    }

    public void Remove(Character owner)
    {
        if (isApplied)
        {
            isApplied = false;
            switch (effectType)
            {
                case BuffType.DamageReduction:
                    owner.damageReduction -= value;
                    break;
                case BuffType.CriticalChance:
                    owner.critChance -= value;
                    break;
                case BuffType.ArmorPenetration:
                    owner.armorPenetration -= value;
                    break;
                case BuffType.MinorSkillBlock:
                    owner.minorSkillBlock = false;
                    break;
                case BuffType.AttackBoost:
                    owner.atk -= value;
                    break;
            }
        }
    }

    public void ReduceDuration(Character owner)
    {
        if (duration > 0)
        {
            duration--;
        }
        if (IsExpired())
        {
            Remove(owner);
        }
    }

    public bool IsExpired()
    {
        return duration <= 0;
    }
}

