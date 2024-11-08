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
            string player1Action = GameManager.instance.players[0] != null ? players[0].currentAction.ToString() : "無動作";
            debugInfoText.text = "Debug：\n" +
                                 "玩家 1 HP: " + (GameManager.instance.players[0] != null ? GameManager.instance.players[0].hp.ToString() : "角色未創建") +
                                 "當前動作: " + player1Action + "\n" +
                                 "玩家 2 HP: " + (GameManager.instance.players.Count > 1 && GameManager.instance.players[1] != null ? GameManager.instance.players[1].hp.ToString() : "角色未創建") +
                                 "當前動作: " + (GameManager.instance.players.Count > 1 && GameManager.instance.players[1] != null ? players[1].currentAction.ToString() : "無動作") + "\n" +
                                 "當前比分: " + wins[0] + " - " + wins[1];
        }
        else
        {
            debugInfoText.text = "Debug：\n角色未創建";
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
        // 按下 Tab 鍵切換調試模式
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isDebugModeActive = !isDebugModeActive;
            ToggleDebugUI(isDebugModeActive);
        }

        // 如果調試模式啟用，則顯示調試信息
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
            debugConsole = "Debug：\n" +
                                  "玩家 A HP: " + (GameManager.instance.players[0] != null ? GameManager.instance.players[0].hp.ToString() : "角色未創建") + "\n" +
                                  "玩家 B HP: " + (GameManager.instance.players.Count > 1 && GameManager.instance.players[1] != null ? GameManager.instance.players[1].hp.ToString() : "角色未創建") + "\n" +
                                  "當前比分: " + wins[0] + " - " + wins[1];
        }
        else
        {
            debugConsole = "Debug：\n角色未創建";
        }

        Debug.Log(debugConsole);
    }

}
