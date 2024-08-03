using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public GameObject endGamePanel;
    public TMP_Text endGameText;
    public Button restartButton;

    private void Start()
    {
        endGamePanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowEndGame(string message)
    {
        endGamePanel.SetActive(true);
        endGameText.text = message;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
