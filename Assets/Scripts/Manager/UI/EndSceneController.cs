using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour
{
    public Text winnerText;
    void Start()
    {
        Debug.Log("EndScene Start");
    }

    public void UpdateWinnerUI()
    {
        List<int> playersWins = GameManager.instance.playersWins; 
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

    // Update is called once per frame
    void Update()
    {
        // ÀË¬d·Æ¹«¥ªÁäÂIÀ»
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            //Debug.Log("mouse click on End scene");
            GameManager.instance.LoadStartScene();
        }
    }
}
