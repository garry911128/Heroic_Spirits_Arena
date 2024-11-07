using System.Collections.Generic;

public class Saber : Character
{
    public Saber() : base(
        "Saber",
        110,   // Base HP
        25,    // Base ATK
        new List<Skill>
        {
            // 小招: 基本攻擊加穿透50%
            new MinorSkill(
                name: "Armor Piercing Attack",
                description: "Basic attack with 50% armor penetration with 2 turn",
                cooldown: 3,
                skillTypes: new List<SkillType> { SkillType.Buff },
                buffs: new List<Buff> { new Buff("Armor Penetration", 2, BuffType.ArmorPenetration, 50) }
            )
        },
        // 大招: 傷害對方40點
        new Ultimate(
            name: "Excalibur",
            description: "Deal 30 damage to the opponent",
            cooldown: 5,
            skillTypes: new List<SkillType> { SkillType.Attack },
            buffs: new List<Buff>(),  // 大招不附帶 buff 效果
            value: 30,  // 固定40點傷害
            cost: 3     // 大招消耗3點數
        )
    )
    { }
}
