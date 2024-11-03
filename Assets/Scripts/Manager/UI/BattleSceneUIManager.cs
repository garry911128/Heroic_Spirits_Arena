using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class BattleSceneUIManager : MonoBehaviour, IGameObserver
{
    private MapManager mapManager = new MapManager();
    public GameObject battleScreen;
    public GameObject vsImage; // "VS" UI component
    public Text scoreBoard; // the wins of each player
    public List<Image> playersImage;
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
            //Debug.Log("hp bar fill amount: " + hpBars[i].fillAmount);
        }
    }

    public void OnPlayerWin(List<int> playerWins)
    {
        //Debug.Log("subScriber OnPlayerWin");
        UpdateScoreBoardUI(playerWins);
    }

    public void UpdateScoreBoardUI(List<int> playersWins)
    {
        scoreBoard.gameObject.SetActive(true);
        scoreBoard.text = $"Player A: {playersWins[0]}:" + $"Player B: {playersWins[1]}";
        //Debug.Log("score board in update" + scoreBoard.text);
    }

    private void DisplayPlayersCharacterImages()
    {
        List<Character> players = new List<Character> { GameManager.instance.players[0], GameManager.instance.players[1] };

        for (int i = 0;i < playersImage.Count; i++)
        {
            playersImage[i].sprite = players[i].sprite;
            playersImage[i].gameObject.SetActive(true);
        }
    }

    IEnumerator ShowVSImageCoroutine()
    {
        // 顯示 "VS" 圖案
        vsImage.SetActive(true);

        // 等待3秒
        yield return new WaitForSeconds(3);

        // 隱藏 "VS" 圖案
        vsImage.SetActive(false);
    }

    void OnDestroy()
    {
        GameManager.instance.RemoveObserver(this);
    }

}
