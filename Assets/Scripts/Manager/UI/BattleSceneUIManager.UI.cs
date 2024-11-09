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
    public Image scoreBoard; // the wins of each player
    public Text scoreBoardText; // the wins of each player
    public Image randomEventBoard; // the random event board
    public Text randomEventText; // the hint text
    public List<CharacterAnimator> characterAnimators; // displays character sprites
    public List<Image> hpBars; // the hp bars of each player
    public Image mapBackgroundImage; // the background of map
    public Image turnOwnerHint; // the hint of turn owner

    void Start()
    {
        turnOwnerHint.gameObject.SetActive(false);
        GameManager.instance.AddObserver(this);
        scoreBoard.gameObject.SetActive(true);
        randomEventBoard.gameObject.SetActive(false);
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

    public void OnTurnStart(Character player, int playerNumber, int currentTurn)
    {
        turnOwnerHint.gameObject.SetActive(true);
        Vector3 hpBarCenter = (hpBars[playerNumber].transform.position);
        turnOwnerHint.transform.position = new Vector3(hpBarCenter.x, hpBarCenter.y + 150, hpBarCenter.z);
        actionPanel.ShowActionPanel(player, playerNumber, currentTurn);
        PlayCharacterAnimation(playerNumber, CharacterAction.IDLE);
    }

    public void OnPlayerWin(List<int> playerWins)
    {
        scoreBoardText.gameObject.SetActive(true);
        scoreBoardText.text = $"{playerWins[0]}:{playerWins[1]}";
    }

    public void OnTriggerEvent(RandomEvent randomEvent, int playerNumber)
    {
        _ = OnTriggerEventAsync(randomEvent, playerNumber);
    }

    public async Task OnTriggerEventAsync(RandomEvent randomEvent, int playerNumber)
    {
        randomEventBoard.gameObject.SetActive(true);
        randomEventText.text = $"Player {playerNumber + 1} triggered {randomEvent.description}";
        await Task.Delay(3000);
        randomEventText.text = string.Empty;
        randomEventBoard.gameObject.SetActive(false);
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
