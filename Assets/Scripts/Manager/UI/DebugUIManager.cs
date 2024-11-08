using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIManager : MonoBehaviour, IGameObserver
{
    public static DebugUIManager instance; // Singleton instance
    public Text debugInfoText;
    private List<int> wins;
    private List<Character> players;
    private bool isDebugModeActive = false;


    void Awake()
    {
        // Ensure only one instance of DebugUIManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Keep this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        GameManager.instance.AddObserver(this);
        wins = new List<int> { 0, 0};
        players = new List<Character>();
        debugInfoText.gameObject.SetActive(false);
    }

    public void OnPlayerStatsChanged(List<Character> players)
    {
        UpdatePlayerStatsUI(players);
    }

    public void OnTriggerEvent(RandomEvent randomEvent, int playerNumber)
    {

    }

    public void PlayCharacterAnimation(int playerIndex, CharacterAction action)
    {
    }

    private void UpdatePlayerStatsUI(List<Character> players)
    {
        this.players = players;
        if (players.Count > 0)
        {
            string player1Action = GameManager.instance.players[0] != null ? players[0].currentAction.ToString() : "�L�ʧ@";
            debugInfoText.text = "Debug�G\n" +
                                 "���a 1 HP: " + (GameManager.instance.players[0] != null ? GameManager.instance.players[0].hp.ToString() : "���⥼�Ы�") +
                                 "��e�ʧ@: " + player1Action + "\n" +
                                 "���a 2 HP: " + (GameManager.instance.players.Count > 1 && GameManager.instance.players[1] != null ? GameManager.instance.players[1].hp.ToString() : "���⥼�Ы�") +
                                 "��e�ʧ@: " + (GameManager.instance.players.Count > 1 && GameManager.instance.players[1] != null ? players[1].currentAction.ToString() : "�L�ʧ@") + "\n" +
                                 "��e���: " + wins[0] + " - " + wins[1];
        }
        else
        {
            debugInfoText.text = "Debug�G\n���⥼�Ы�";
        }
    }


    public void OnPlayerWin(List<int> playerWins)
    {
        wins = playerWins;
    }

    public void OnTurnStart(Character player, int playerNumber)
    {
    }

    void Update()
    {
        // ���U Tab ������ոռҦ�
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isDebugModeActive = !isDebugModeActive;
            ToggleDebugUI(isDebugModeActive);
        }

        // �p�G�ոռҦ��ҥΡA�h��ܽոիH��
        if (isDebugModeActive)
        {
            ShowDebugInfo();
        }
    }

    private void ToggleDebugUI(bool active)
    {
        Debug.Log(active ? "Debug Mode Activated" : "Debug Mode Deactivated");
        debugInfoText.gameObject.SetActive(active);
    }

    private void ShowDebugInfo()
    {
        string debugConsole = ""; 
        if (GameManager.instance.players.Count > 0)
        {
            debugConsole = "Debug�G\n" +
                                  "���a A HP: " + (GameManager.instance.players[0] != null ? GameManager.instance.players[0].hp.ToString() : "���⥼�Ы�") + "\n" +
                                  "���a B HP: " + (GameManager.instance.players.Count > 1 && GameManager.instance.players[1] != null ? GameManager.instance.players[1].hp.ToString() : "���⥼�Ы�") + "\n" +
                                  "��e���: " + wins[0] + " - " + wins[1];
        }
        else
        {
            debugConsole = "Debug�G\n���⥼�Ы�";
        }

        Debug.Log(debugConsole);
    }

}
