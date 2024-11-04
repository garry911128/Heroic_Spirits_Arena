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
    ActionPanel actionPanel;

    private void Start()
    {
        LoadRandomEvent();
    }

    public void LoadRandomEvent()
    {
        randomEvents = RandomEvent.LoadEventsFromCSV("Assets/Resources/Scene/BattleScene/RandomEvents/RandomEvent.csv");
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
                break;
            case CharacterAction.DEFENSE:
                character.Defense();
                break;
            default:
                break;
        }
    }

    private IEnumerator BattleCoroutine()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.NotifyObserversPlayersWin();
        while (players[0].hp > 0 && players[1].hp > 0)
        {
            currentAttacker = players[turn % 2];
            currentDefender = players[(turn + 1) % 2];

            TriggerRandomEvent(currentAttacker);
            yield return new WaitForSeconds(1f);

            GameManager.instance.NotifyObserverOnTurnStart(turn%2);
            yield return new WaitUntil(() => actionPanel.IsActionSelected);
            CharacterAction action = actionPanel.GetSelectedAction();
            PerformAction(action, currentAttacker);

            GameManager.instance.NotifyObserversPlayersState();
            turn++;
        }
        if (players[0].hp <= 0) PlayerWins(1);
        if (players[1].hp <= 0) PlayerWins(0);
    }

    public void TriggerRandomEvent(Character character)
    {
        foreach (var randomEvent in randomEvents)
        {
            if (randomEvent.TryApplyEvent(character))
            {
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
