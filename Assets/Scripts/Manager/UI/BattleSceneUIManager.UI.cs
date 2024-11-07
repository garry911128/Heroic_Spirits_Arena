using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BattleSceneUIManager : MonoBehaviour, IGameObserver
{
    private MapManager mapManager = new MapManager();
    public ActionPanel actionPanel;
    public GameObject battleScreen;
    public GameObject vsImage; // "VS" UI component
    public Text scoreBoard; // the wins of each player
    public Text HintText; // the hint text
    public List<CharacterAnimator> characterAnimators; // displays character sprites
    public List<Image> hpBars; // the hp bars of each player
    public Image mapBackgroundImage; // the background of map

    void Start()
    {
        GameManager.instance.AddObserver(this);
        scoreBoard.gameObject.SetActive(true);
        mapManager.LoadMapSprites();
        mapBackgroundImage.sprite = mapManager.SelectRandomMap();
        StartCoroutine(ShowVSImageCoroutine());
        DisplayPlayersCharacterImages();
    }

    public void OnPlayerStatsChanged(List<Character> players)
    {
        UpdatePlayerStatsUI(players);
    }

    private void UpdatePlayerStatsUI(List<Character> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            hpBars[i].fillAmount = (float)players[i].hp / players[i].maxHp;
        }
    }

    public void OnTurnStart(Character player, int playerNumber)
    {
        actionPanel.ShowActionPanel(player, playerNumber);
        PlayCharacterAnimation(playerNumber, CharacterAction.IDLE);
    }

    public void OnPlayerWin(List<int> playerWins)
    {
        scoreBoard.gameObject.SetActive(true);
        scoreBoard.text = $"Player 1: {playerWins[0]}, Player 2: {playerWins[1]}";
    }

    public void OnTriggerEvent(RandomEvent randomEvent, int playerNumber)
    {
        _ = OnTriggerEventAsync(randomEvent, playerNumber);
    }

    public async Task OnTriggerEventAsync(RandomEvent randomEvent, int playerNumber)
    {
        HintText.text = $"Player {playerNumber + 1} triggered {randomEvent.description}";
        await Task.Delay(2000);
        HintText.text = string.Empty;
    }

    private void DisplayPlayersCharacterImages()
    {
        List<Character> players = GameManager.instance.players;
        for (int i = 0; i < characterAnimators.Count; i++)
        {
            characterAnimators[i].LoadSprite(players[i]);
            characterAnimators[i].gameObject.SetActive(true);
        }
    }

    IEnumerator ShowVSImageCoroutine()
    {
        vsImage.SetActive(true);
        yield return new WaitForSeconds(3);
        vsImage.SetActive(false);
    }

    public void PlayCharacterAnimation(int playerIndex, CharacterAction action)
    {
        StopAllCoroutines();  // 停止目前正在撥放的動畫
        StartCoroutine(characterAnimators[playerIndex].PlayAnimationCoroutine(action));
    }

    void OnDestroy()
    {
        GameManager.instance.RemoveObserver(this);
    }
}
