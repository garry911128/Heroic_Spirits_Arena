using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour, IGameObserver
{
    public Text winnerText;
    void Start()
    {
        GameManager.instance.AddObserver(this);
        Debug.Log("EndScene Start");
    }

    public void OnPlayerWin(List<int> playerWins)
    {
        Debug.Log("EndScene OnPlayerWin");
        UpdateWinnerUI(playerWins);
    }

    public void UpdateWinnerUI(List<int> playersWins)
    {
        winnerText.text = $"  {playersWins[0]}:{playersWins[1]}\n";
        if (playersWins[0] > playersWins[1])
        {
            //winnerImage.sprite = GameManager.instance.player1.characterImage;
            winnerText.text += "Player 1 Wins!";
        }
        else
        {
            //winnerImage.sprite = GameManager.instance.player2.characterImage;
            winnerText.text += "Player 2 Wins!";
        }
        Debug.Log("winner text: " + winnerText.text);
    }

    public void OnPlayerStatsChanged(List<Character> players)
    {

    }

    public void OnDestroy()
    {
        GameManager.instance.RemoveObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        // �ˬd�ƹ������I��
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            //Debug.Log("mouse click on End scene");
            GameManager.instance.LoadStartScene();
        }
    }
}