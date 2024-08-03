using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
    public GameObject victoryPanel;
    public TMP_Text victoryText;
    public Button restartButton;

    private void Start()
    {
        victoryPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowVictory(string message)
    {
        victoryPanel.SetActive(true);
        victoryText.text = message;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
