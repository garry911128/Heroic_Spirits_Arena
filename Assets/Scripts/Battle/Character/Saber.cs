using System.Collections.Generic;

public class Saber : Character
{
    public Saber() : base(
        "Saber",
        110,   // Base HP
        25,    // Base ATK
        new List<Skill>
        {
            // �p��: �򥻧����[��z50%
            new MinorSkill(
                name: "Armor Piercing Attack",
                cooldown: 3,
                skillTypes: new List<SkillType> { SkillType.Attack },
                buffs: new List<Buff> { new Buff("Armor Penetration", 1, BuffType.ArmorPenetration, 50) }
            )
        },
        // �j��: �ˮ`���40�I
        new Ultimate(
            name: "Excalibur",
            cooldown: 5,
            skillTypes: new List<SkillType> { SkillType.Attack },
            buffs: new List<Buff>(),  // �j�ۤ����a buff �ĪG
            value: 40,  // �T�w40�I�ˮ`
            cost: 3     // �j�ۮ���3�I��
        )
    )
    { }
}
