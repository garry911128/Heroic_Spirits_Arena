using System.Collections.Generic;

public class Paladin : Character
{
    public Paladin() : base(
        "Paladin",
        130,   // Base HP
        20,    // Base ATK
        new List<Skill>
        {
            // �p��: �U�@�^�X��֨��쪺�ˮ` 100%
            new MinorSkill(
                name: "Defend",
                description: "Reduce damage taken by 100% for 2 turn",
                cooldown: 3,
                skillTypes: new List<SkillType> { SkillType.Buff },
                buffs: new List<Buff> { new Buff("Damage Reduction", 2, BuffType.DamageReduction, 100) }
            )
        },
        // �j��: ���U�Ө�^�X���m�Y�Ƭ�100%(�������m)�A�éT�w����15�ˮ`
        new Ultimate(
            name: "Perfect Defense",
            description: "Reduce all damage taken by 100% for 2 turns and deal 15 damage to the attacker",
            cooldown: 5,
            skillTypes: new List<SkillType> { SkillType.Buff },
            buffs: new List<Buff>
            {
                new Buff("Perfect Defense", 2, BuffType.DamageReduction, 100)
            },
            value: 15,  // �T�w�����ˮ`
            cost: 3     // �j�ۮ���3�I��
        )
    )
    { }
}
