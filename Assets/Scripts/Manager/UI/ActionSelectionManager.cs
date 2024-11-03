using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    public GameObject actionPanel;
    public Button attackButton;
    public Button defendButton;

    public delegate void ActionSelected(Character action);
    public event ActionSelected OnActionSelected;

    void Start()
    {
        // ���U���s�ƥ�
        attackButton.onClick.AddListener(() => SelectAction(ActionType.Attack));
        defendButton.onClick.AddListener(() => SelectAction(ActionType.Defend()));

        // ���æ�ʭ��O
        actionPanel.SetActive(false);
    }

    // ��ܦ�ʭ��O
    public void ShowActionPanel()
    {
        actionPanel.SetActive(true);
    }

    // ���æ�ʭ��O
    public void HideActionPanel()
    {
        actionPanel.SetActive(false);
    }

    // ���ܤF��ʮɪ��B�z
    private void SelectAction(ActionType action)
    {
        OnActionSelected?.Invoke(action); // Ĳ�o�ƥ�
        HideActionPanel(); // ���í��O
    }
}

