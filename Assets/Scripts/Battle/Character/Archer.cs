using System.Collections.Generic;

public class Archer : Character
{
    public Archer() : base(
        "Archer",
        100,   // Base HP
        20,    // Base ATK
        new List<Skill>
        {
            // 小招: 對手下一回合無法使用招式
            new MinorSkill(
                name: "Disable Opponent's Skill",
                cooldown: 3,
                skillTypes: new List<SkillType> { SkillType.Buff },
                buffs: new List<Buff> { new Buff("Skill Block", 1, BuffType.MinorSkillBlock, 1) }
            )
        },
        // 大招: 給自己加上暴擊率50%的buff，持續2回合，並且多一回合行動
        new Ultimate(
            name: "Critical Strike Boost",
            cooldown: 5,
            skillTypes: new List<SkillType> { SkillType.Buff },
            buffs: new List<Buff>
            {
                new Buff("Critical Chance", 2, BuffType.CriticalChance, 50)  // 50% 暴擊率，持續2回合
            },
            value: 1,  // 額外回合數
            cost: 3    // 大招消耗3點數
        )
    )
    { 
    }
}