using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<Character> players;
    public List<RandomEvent> randomEvents = new List<RandomEvent>();
    private Character currentAttacker;
    private Character currentDefender;
    private int turn;
    private int maxTurns = 10;
    ActionPanel actionPanel;

    private void Start()
    {
        LoadRandomEvent();
    }

    public void LoadRandomEvent()
    {
        randomEvents = RandomEvent.LoadEventsFromCSV("Scene/BattleScene/RandomEvents/RandomEvent");
    }

    public void StartBattle(List<Character> assignedPlayers)
    {
        players = assignedPlayers;
        turn = 0;
        GameManager.instance.NotifyObserversPlayersState();
        GameManager.instance.NotifyObserversPlayersWin();
        StartCoroutine(BattleCoroutine());
    }

    public void LoadActionPanel(ActionPanel panel)
    {
        actionPanel = panel;
    }

    public void PerformAction(CharacterAction action, Character character)
    {
        switch (action)
        {
            case CharacterAction.ATTACK:
                character.Attack(currentDefender);
                GameManager.instance.NotifyObserverPlayAnimation(turn % 2, CharacterAction.ATTACK);
                break;
            case CharacterAction.DEFENSE:
                character.Defense();
                GameManager.instance.NotifyObserverPlayAnimation(turn % 2, CharacterAction.DEFENSE);
                break;
            case CharacterAction.USEMINORSKILL:
                character.ActivateSkill(0, currentDefender);
                GameManager.instance.NotifyObserverPlayAnimation(turn % 2, CharacterAction.ATTACK);
                break;
            case CharacterAction.USEUlTIMATE:
                character.ActivateUltimate(currentDefender);
                GameManager.instance.NotifyObserverPlayAnimation(turn % 2, CharacterAction.ATTACK);
                break;
            default:
                break;
        }
    }

    private IEnumerator BattleCoroutine()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.NotifyObserversPlayersWin();
        while (players[0].hp > 0 && players[1].hp > 0 && turn < maxTurns)
        {
            currentAttacker = players[turn % 2];
            currentDefender = players[(turn + 1) % 2];

            TriggerRandomEvent(currentAttacker);
            yield return new WaitForSeconds(1f);

            GameManager.instance.NotifyObserverOnTurnStart(turn%2, turn);
            yield return new WaitUntil(() => actionPanel.IsActionSelected);
            CharacterAction action = actionPanel.GetSelectedAction();
            PerformAction(action, currentAttacker);
            currentAttacker.Update();
            GameManager.instance.NotifyObserversPlayersState();
            turn++;
        }
        if (turn == maxTurns)
        {
            GameManager.instance.MatchDraw();
        }
        else
        {
            if (players[0].hp <= 0) PlayerWins(1);
            if (players[1].hp <= 0) PlayerWins(0);
        }
    }

    public void TriggerRandomEvent(Character character)
    {
        foreach (var randomEvent in randomEvents)
        {
            if (randomEvent.TryApplyEvent(character))
            {
                GameManager.instance.NotifyObserverOnTriggerEvent(randomEvent, turn%2);
                Debug.Log("Triggered event: " + randomEvent.name);
                break;
            }
        }
    }

    public void PlayerWins(int winnerIndex)
    {
        GameManager.instance.PlayerWins(winnerIndex);
        GameManager.instance.NotifyObserversPlayersWin();
    }


}
