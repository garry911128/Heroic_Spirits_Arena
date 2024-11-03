using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameObserver
{
    void OnPlayerWin(List<int> playerWins);
    void OnPlayerStatsChanged(List<Character> players);
}
