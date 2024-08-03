using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject inventory;
    public GameObject setting;
    public GameObject camerafollow;
    public GameObject mainCamera;
    private Vector3 playerPosition;
    public GameObject eventSystem;
    public GameObject panelManager;
    public PanelManager panelManagerScript;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (player == null) player = GameObject.FindWithTag("Player");
        if (inventory == null) inventory = GameObject.FindWithTag("Inventory");
        if (setting == null) setting = GameObject.FindWithTag("Setting");
        if (mainCamera == null) mainCamera = Camera.main.gameObject;
        if (camerafollow == null) camerafollow = GameObject.FindWithTag("CameraFollow");
        if (eventSystem == null) eventSystem = GameObject.FindWithTag("EventSystem");
        if (panelManager == null) panelManager = GameObject.FindWithTag("PanelScreen");
        if (panelManagerScript == null) panelManagerScript = panelManager.GetComponent<PanelManager>();

        DontDestroyOnLoad(player);
        DontDestroyOnLoad(inventory);
        DontDestroyOnLoad(setting);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(eventSystem);
        DontDestroyOnLoad(camerafollow);
        DontDestroyOnLoad(panelManager);
    }

    public void SavePlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }

    public void LoadPlayerPosition()
    {
        player.transform.position = playerPosition;
    }

    public void ChangeScene(string sceneName)
    {
        SavePlayerPosition(player.transform.position);
        SceneManager.LoadScene(sceneName);
        Invoke("UpdateUIAfterSceneLoad", 0.1f); // Invoke after a short delay to ensure the scene is fully loaded
    }

    public void EndGame(string message)
    {
        panelManagerScript.ShowEndGame(message);
    }

    public void Victory(string message)
    {
        panelManagerScript.ShowVictory(message);
    }

    public void Restart()
    {
        Destroy(player);
        Destroy(inventory);
        Destroy(setting);
        Destroy(mainCamera);
        Destroy(eventSystem);
        Destroy(camerafollow);
        Destroy(panelManager);

        SceneManager.LoadScene("Slime");

        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindWithTag("Player");
        inventory = GameObject.FindWithTag("Inventory");
        setting = GameObject.FindWithTag("Setting");
        mainCamera = Camera.main.gameObject;
        camerafollow = GameObject.FindWithTag("CameraFollow");
        eventSystem = GameObject.FindWithTag("EventSystem");
        panelManager = GameObject.FindWithTag("PanelScreen");
        panelManagerScript = panelManager.GetComponent<PanelManager>();

        DontDestroyOnLoad(player);
        DontDestroyOnLoad(inventory);
        DontDestroyOnLoad(setting);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(eventSystem);
        DontDestroyOnLoad(camerafollow);
        DontDestroyOnLoad(panelManager);
    }
}
