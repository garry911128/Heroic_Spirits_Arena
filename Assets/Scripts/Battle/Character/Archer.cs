using System.Collections.Generic;

public class Archer : Character
{
    public Archer() : base(
        "Archer",
        100,   // Base HP
        20,    // Base ATK
        new List<Skill>
        {
            // �p��: ���U�@�^�X�L�k�ϥΩۦ�
            new MinorSkill(
                name: "Disable Opponent's Skill",
                cooldown: 3,
                skillTypes: new List<SkillType> { SkillType.Buff },
                buffs: new List<Buff> { new Buff("Skill Block", 1, BuffType.MinorSkillBlock, 1) }
            )
        },
        // �j��: ���ۤv�[�W�����v50%��buff�A����2�^�X�A�åB�h�@�^�X���
        new Ultimate(
            name: "Critical Strike Boost",
            cooldown: 5,
            skillTypes: new List<SkillType> { SkillType.Buff },
            buffs: new List<Buff>
            {
                new Buff("Critical Chance", 2, BuffType.CriticalChance, 50)  // 50% �����v�A����2�^�X
            },
            value: 1,  // �B�~�^�X��
            cost: 3    // �j�ۮ���3�I��
        )
    )
    { 
    }
}