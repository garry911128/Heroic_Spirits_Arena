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
                description: "Disable opponent's skill for 2 turn",
                cooldown: 3,
                skillTypes: new List<SkillType> { SkillType.Buff },
                buffs: new List<Buff> { new Buff("Skill Block", 2, BuffType.MinorSkillBlock, 2) }
            )
        },
        // �j��: ���ۤv�[�W�����v50%��buff�A����2�^�X�A�åB�h�@�^�X���
        new Ultimate(
            name: "Critical Strike Boost",
            description: "Boost critical strike chance by 50% for 2 turns",
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