using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    public GameObject actionPanel;
    public Button[] buttons;
    public Image selector;
    public Text descriptionText;
    public Text turnHintText;
    private int selectedIndex = 0;
    private List<string> descriptionString;
    private CharacterAction selectedAction;
    private bool isActionSelected = false;

    void Start()
    {
        GameManager.instance.battleManager.LoadActionPanel(this);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => SelectAction(button));
        }

        actionPanel.SetActive(false);
        descriptionString = new List<string> { "", "", "", ""};
        UpdateButtonSelection();
    }

    private void EnableActionButton(Character player)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if(i == 0 || i == 1)
            {
                buttons[i].interactable = true;
            }
            else if(i == 2)
            {
                buttons[i].interactable = player.CanUseSkill(0, CharacterAction.USEMINORSKILL);
            }
            else
            {
                buttons[i].interactable = player.CanUseSkill(0, CharacterAction.USEUlTIMATE);
            }
        }
    }

    public void ShowActionPanel(Character player, int playerNumber, int currentTurn)
    {
        isActionSelected = false;
        actionPanel.SetActive(true);
        EnableActionButton(player);
        turnHintText.text = $"Turn {currentTurn+1} Player{playerNumber+1}'s Turn"; //Turn 10, Player1's Turn
        descriptionString = player.GetDescriptions();
        UpdateButtonSelection();
    }

    public void HideActionPanel()
    {
        actionPanel.SetActive(false);
    }

    public bool IsActionSelected => isActionSelected;

    public CharacterAction GetSelectedAction()
    {
        isActionSelected = false;
        return selectedAction;
    }

    private void SelectAction(Button button)
    {
        // 檢查按鈕是否可用
        if (!button.interactable)
        {
            return; // 按鈕不可用，直接返回不執行任何操作
        }

        // 設定選擇的動作
        if (button == buttons[0])
        {
            selectedAction = CharacterAction.ATTACK;
        }
        else if (button == buttons[1])
        {
            selectedAction = CharacterAction.DEFENSE;
        }
        else if (button == buttons[2])
        {
            selectedAction = CharacterAction.USEMINORSKILL;
        }
        else if (button == buttons[3])
        {
            selectedAction = CharacterAction.USEUlTIMATE;
        }

        isActionSelected = true;
        HideActionPanel();
    }


    void Update()
    {
        if (actionPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;
                UpdateButtonSelection();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedIndex = (selectedIndex + 1) % buttons.Length;
                UpdateButtonSelection();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SelectAction(buttons[selectedIndex]);
            }
        }
    }

    private void UpdateButtonSelection()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            ColorBlock colors = buttons[i].colors;
            if (i == selectedIndex) 
            {
                selector.rectTransform.anchoredPosition = buttons[i].GetComponent<RectTransform>().anchoredPosition;
                descriptionText.text = descriptionString[i];
            }
        }
    }
}
