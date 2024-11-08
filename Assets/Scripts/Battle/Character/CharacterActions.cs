using System.Collections.Generic;

public abstract partial class Character
{

    public bool CanUseSkill(int minorSkillIndex, CharacterAction action)
    {
        switch (action)
        {
            case CharacterAction.USEMINORSKILL:
                return skills[minorSkillIndex].IsAvailable() && !minorSkillBlock;
            case CharacterAction.USEUlTIMATE:
                return ultimatePoints >= ultimate.ultimateCost;
            default:
                return false;
        }
    }

    public void ActivateSkill(int skillIndex, Character target)
    {
        currentAction = CharacterAction.USEMINORSKILL;
        skills[skillIndex].Activate(this, target);
    }

    public void ActivateUltimate(Character target)
    {
        currentAction = CharacterAction.USEUlTIMATE;
        ultimate.Activate(this, target);
    }

    public List<string> GetDescriptions()
    {
        List<string> descriptions = new List<string>
        {
            GetActionDescription(CharacterAction.ATTACK),
            GetActionDescription(CharacterAction.DEFENSE),
            GetActionDescription(CharacterAction.USEMINORSKILL),
            GetActionDescription(CharacterAction.USEUlTIMATE)
        };
        return descriptions;
    }

    private string GetActionDescription(CharacterAction action)
    {
        return action switch
        {
            CharacterAction.ATTACK => $"Attack: {atk}",
            CharacterAction.DEFENSE => "Defense: Reduces damage taken by 50% for 2 turns.(include this turn)",
            CharacterAction.USEMINORSKILL => "Minor Skills:\n" + string.Join("\n", skills.ConvertAll(s => $"{s.name}: {s.description}")),
            CharacterAction.USEUlTIMATE => $"ultpoint:{ultimatePoints}, cost:{ultimate.ultimateCost}\nUltimate:\n{ultimate.name}: {ultimate.description}",
            _ => "Idle",
        };
    }
}
