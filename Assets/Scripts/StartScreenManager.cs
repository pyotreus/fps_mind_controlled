using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreenPanel;
    [SerializeField] private GameObject bciSelectionPanel;
    [SerializeField] private GameObject noisetagManager;

    public void StartNewGame()
    {
        Debug.Log("test");
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

        if (hasBCI && noisetagManager != null)
        {
            noisetagManager.SetActive(true);
        }
        else
        {
            Debug.Log("BCI not used. NoisetagManager remains inactive.");
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
