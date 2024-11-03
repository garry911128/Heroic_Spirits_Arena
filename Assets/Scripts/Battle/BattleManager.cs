using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    private List<RandomEvent> randomEvents;

    public void StartBattle(List<Character> players)
    {
        StartCoroutine(BattleCoroutine(players));
    }

    private IEnumerator BattleCoroutine(List<Character> players)
    {
        yield return new WaitForSeconds(3);

        Character player1 = players[0];
        Character player2 = players[1];
        Character[] turnOrder = { player1, player2 };

        // 輪流攻擊，直到其中一方的 hp <= 0
        for (int turn = 0; player1.hp > 0 && player2.hp > 0; turn++)
        {
            Character attacker = turnOrder[turn % 2];
            Character defender = turnOrder[(turn + 1) % 2];
            TriggerRandomEvent(attacker);

            attacker.attack(defender); // Execute the attack
            yield return new WaitForSeconds(2);

            if (defender.hp <= 0)
            {
                GameManager.instance.PlayerWins(turn % 2);
                break;
            }
        }
    }

    public void TriggerRandomEvent(Character character) // Method to trigger a random event for a player
    {
        for (int i = 0; i < randomEvents.Count; i++)
        {
            if (randomEvents[i].TryApplyEvent(character))
            {
                Debug.Log("Triggered event: " + randomEvents[i].name);
                break;
            }
        }
    }

    public void LoadRandomEvents(string path)
    {
        randomEvents = RandomEvent.LoadEventsFromCSV(path); // Load events
    }
}

