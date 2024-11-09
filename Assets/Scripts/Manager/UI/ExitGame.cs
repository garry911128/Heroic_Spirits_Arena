using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    public Button exitBtn;

    public void Start()
    {
        exitBtn.onClick.AddListener(OnExitButtonPressed);
    }

    public void OnExitButtonPressed()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
