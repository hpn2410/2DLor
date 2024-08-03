using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public GameObject victoryPanel;
    public TMP_Text endGameText;
    public TMP_Text victoryText;
    public Button restartButton;
    public GameObject inventory;
    public GameObject setting;
    public Button quitButton;

    private void Start()
    {
        endGamePanel.SetActive(false);
        victoryPanel.SetActive(false);
        inventory = GameObject.FindWithTag("Inventory");
        setting = GameObject.FindWithTag("Setting");
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        endGamePanel.SetActive(false);
        victoryPanel.SetActive(false);
        Time.timeScale = 1f;
        //Destroy(inventory);
        //Destroy(setting);
    }

    public void ShowEndGame(string message)
    {
        endGamePanel.SetActive(true);
        endGameText.text = message;
        Time.timeScale = 0f;
    }

    public void ShowVictory(string message)
    {
        victoryPanel.SetActive(true);
        victoryText.text = message;
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        GameManager.instance.Restart();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
