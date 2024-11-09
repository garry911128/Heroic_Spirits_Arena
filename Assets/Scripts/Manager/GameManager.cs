using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BattleManager battleManager;
    public CharacterFactory characterFactory = new CharacterFactory();
    public List<Character> players = new List<Character>(new Character[2]);
    public List<int> playersWins = new List<int> { 0, 0 };
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

    public void ClearData()
    {
        playersWins = new List<int> { 0, 0 };
        currentMatch = 0;
    }

    void Start()
    {
        LoadStartScene();
        battleManager.LoadRandomEvent();
    }
    public void LoadStartScene()
    {
        ClearData();
        NotifyObserversPlayersState();
        NotifyObserversPlayersWin();
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
        List<CharacterType> randomTypes = RandomSelectTwoCharacter();
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
            battleManager.StartBattle(players);
        }
    }

    public void MatchDraw()
    {
        Debug.Log("Match ended in a draw!");
        NotifyObserversPlayersState();
        NotifyObserversPlayersWin();
        StartNewMatch();
    }

    public void PlayerWins(int index) // 0: playerA, 1: playerB
    {
        playersWins[index]++;
        CheckForSeriesWinner();
        NotifyObserversPlayersWin();
        NotifyObserversPlayersState();
    }

    private void CheckForSeriesWinner()
    {
        if (playersWins[0] >= maxWins || playersWins[1] >= maxWins)
        {
            SceneManager.LoadScene("EndScene");  
        }
        else
        {
            StartNewMatch();
        }
    }
    
}