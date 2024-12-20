using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameObserver
{
    void OnPlayerWin(List<int> playerWins);
    void OnPlayerStatsChanged(List<Character> players);
    void OnTurnStart(Character player, int playerNumber, int currentTurn);
    void OnTriggerEvent(RandomEvent randomEvent, int playerNumber);
    void PlayCharacterAnimation(int playerNumber, CharacterAction action);
}
