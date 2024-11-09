using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    private List<IGameObserver> gameObservers = new List<IGameObserver>();

    public void AddObserver(IGameObserver observer)
    {
        if (!gameObservers.Contains(observer))
        {
            gameObservers.Add(observer);
        }
    }

    public void RemoveObserver(IGameObserver observer)
    {
        if (gameObservers.Contains(observer))
        {
            gameObservers.Remove(observer);
        }
    }

    public void NotifyObserversPlayersState()
    {
        foreach (IGameObserver observer in gameObservers)
        {
            observer.OnPlayerStatsChanged(players);
        }
    }

    public void NotifyObserversPlayersWin()
    {
        foreach (IGameObserver observer in gameObservers)
        {
            observer.OnPlayerWin(playersWins);
        }
    }

    public void NotifyObserverOnTurnStart(int playerNumber, int currentTurn)
    {
        foreach (IGameObserver observer in gameObservers)
        {
            observer.OnTurnStart(players[playerNumber], playerNumber, currentTurn);
        }
    }

    public void NotifyObserverOnTriggerEvent(RandomEvent triggerEvent, int playerNumber)
    {
        foreach (IGameObserver observer in gameObservers)
        {
            observer.OnTriggerEvent(triggerEvent, playerNumber);
        }
    }

    public void NotifyObserverPlayAnimation(int playerIndex, CharacterAction action)
    {
        foreach (var observer in gameObservers)
        {
            if (observer is BattleSceneUIManager uiManager)
            {
                uiManager.PlayCharacterAnimation(playerIndex, action);
            }
        }
    }

}
