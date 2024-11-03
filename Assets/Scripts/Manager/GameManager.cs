using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Character> players = new List<Character>(new Character[2]);
    public CharacterFactory characterFactory = new CharacterFactory();
    private List<int> playersWins = new List<int> {0, 0};
    private List<RandomEvent> randomEvents; // List to hold random events
    private List<IGameObserver> gameObservers = new List<IGameObserver>();
    private int maxWins = 2; // 三戰兩勝
    private int currentMatch = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    private void NotifyObserversPlayersState()
    {
        foreach (IGameObserver observer in gameObservers)
        {
            observer.OnPlayerStatsChanged(players);
        }
    }

    private void NotifyObserversPlayersWin()
    {
        Debug.Log("NotifyObserversPlayersWin" + playersWins[0] + " " + playersWins[1]);
        foreach (IGameObserver observer in gameObservers)
        {
            observer.OnPlayerWin(playersWins);
        }
    }

    public void ClearData()
    {
        playersWins = new List<int> { 0, 0 };
        players = new List<Character>(new Character[2]);
        currentMatch = 0;
        NotifyObserversPlayersState();
        NotifyObserversPlayersWin();
    }

    void Start()
    {
        LoadStartScene();
        randomEvents = RandomEvent.LoadEventsFromCSV("Assets/Resources/Scene/BattleScene/RandomEvents/RandomEvent.csv"); // Load events
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

    private List<CharacterType> RandomSelectTwoCharacter()
    {
        List<CharacterType> characterTypes = new List<CharacterType>();
        int random1 = Random.Range(0, 3);
        int random2 = Random.Range(0, 3);
        while (random1 == random2)
        {
            random2 = Random.Range(0, 3);
        }
        characterTypes.Add((CharacterType)random1);
        characterTypes.Add((CharacterType)random2);
        return characterTypes;
    }

    private void AssignCharacter(int currentMatch)
    {
        List<CharacterType> randomTypes =  RandomSelectTwoCharacter();
        players[0] = characterFactory.CreateCharacter(randomTypes[0]);
        players[1] = characterFactory.CreateCharacter(randomTypes[1]);
        if (currentMatch % 2 == 0)
        {
            Character temp = players[0];
            players[0] = players[1];
            players[1] = temp;
        }
    }

    public void StartNewMatch()
    {
        currentMatch++;
        if (playersWins[0] < maxWins && playersWins[1] < maxWins)
        {
            Debug.Log("StartNewMatch");
            AssignCharacter(currentMatch);
            SceneManager.LoadScene("BattleScene");
            NotifyObserversPlayersState();
            NotifyObserversPlayersWin();
            StartBattle();
        }
    }

    public void StartBattle()
    {
        NotifyObserversPlayersWin();
        StartCoroutine(BattleCoroutine());
    }

    private IEnumerator BattleCoroutine()
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

            // Wait for the player to press Enter to attack
            //bool attackExecuted = false;
            //while (!attackExecuted)
            //{
            //    if (Input.GetKeyDown("a"))
            //    {
            //        attacker.attack(defender); // Execute the attack
            //        attackExecuted = true; // Mark the attack as executed
            //    }
            //    yield return null; // Wait until the next frame
            //}
            attacker.attack(defender); // Execute the attack
            yield return new WaitForSeconds(2);

            if (defender.hp <= 0)
            {
                //Debug.Log(attacker.name + " wins!");
                PlayerWins(turn % 2);
                break;
            }
            NotifyObserversPlayersState();
            NotifyObserversPlayersWin();
        }
        NotifyObserversPlayersState();
        NotifyObserversPlayersWin();

    }

    public void TriggerRandomEvent(Character character)// Method to trigger a random event for a player
    {
        for(int i = 0; i < randomEvents.Count; i++)
        {
            if (randomEvents[i].TryApplyEvent(character))
            {
                Debug.Log("Triggered event: " + randomEvents[i].name);
                break;
            }
        }

    }

    public void PlayerWins(int index) // 0: playerA, 1: playerB
    {
        playersWins[index]++;
        NotifyObserversPlayersWin();
        CheckForSeriesWinner();
    }

    private void CheckForSeriesWinner()
    {
        if (playersWins[0] >= maxWins || playersWins[1] >= maxWins)
        {
            NotifyObserversPlayersWin();
            StartCoroutine(NotifyAndLoadEndScene());
            NotifyObserversPlayersWin();
        }
        else
        {
            StartNewMatch();
        }
    }

    private IEnumerator NotifyAndLoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
        yield return new WaitForSeconds(1);
        NotifyObserversPlayersWin();
    }

    public List<string> GetPlayersInfo()
    {
        List<string> info = new List<string>();
        for (int i = 0; i < players.Count; i++)
        {
            string playerInfo = players[i].name + "\n";
            playerInfo += "HP: " + players[i].hp + "\n";
            playerInfo += "ATK: " + players[i].atk + "\n";
            info.Add(playerInfo);
        }
        return info;
    }

}
