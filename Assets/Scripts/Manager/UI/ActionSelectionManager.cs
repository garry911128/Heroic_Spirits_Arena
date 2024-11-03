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
        // 註冊按鈕事件
        attackButton.onClick.AddListener(() => SelectAction(ActionType.Attack));
        defendButton.onClick.AddListener(() => SelectAction(ActionType.Defend()));

        // 隱藏行動面板
        actionPanel.SetActive(false);
    }

    // 顯示行動面板
    public void ShowActionPanel()
    {
        actionPanel.SetActive(true);
    }

    // 隱藏行動面板
    public void HideActionPanel()
    {
        actionPanel.SetActive(false);
    }

    // 當選擇了行動時的處理
    private void SelectAction(ActionType action)
    {
        OnActionSelected?.Invoke(action); // 觸發事件
        HideActionPanel(); // 隱藏面板
    }
}

