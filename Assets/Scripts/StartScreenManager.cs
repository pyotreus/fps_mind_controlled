using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreenPanel;
    [SerializeField] private GameObject bciSelectionPanel;

    public void StartNewGame()
    {
        startScreenPanel.SetActive(false);
        bciSelectionPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetBCI(bool hasBCI)
    {
        GlobalReferences.Instance.BCI = hasBCI;

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
