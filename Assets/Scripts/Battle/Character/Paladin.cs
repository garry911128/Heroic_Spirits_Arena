using System.Collections.Generic;

public class Paladin : Character
{
    public Paladin() : base(
        "Paladin",
        130,   // Base HP
        15,    // Base ATK
        new List<Skill>
        {
            // 小招: 下一回合減少受到的傷害 100%
            new MinorSkill(
                name: "Defend",
                cooldown: 3,
                skillTypes: new List<SkillType> { SkillType.Buff },
                buffs: new List<Buff> { new Buff("Damage Reduction", 1, BuffType.DamageReduction, 100) }
            )
        },
        // 大招: 接下來兩回合防禦係數為100%(完全防禦)，並固定反擊15傷害
        new Ultimate(
            name: "Perfect Defense",
            cooldown: 5,
            skillTypes: new List<SkillType> { SkillType.Buff },
            buffs: new List<Buff>
            {
                new Buff("Perfect Defense", 2, BuffType.DamageReduction, 100)
            },
            value: 15,  // 固定反擊傷害
            cost: 3     // 大招消耗3點數
        )
    )
    { }
}
