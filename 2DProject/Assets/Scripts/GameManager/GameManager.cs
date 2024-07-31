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
        player = GameObject.FindWithTag("Player");
        inventory = GameObject.FindWithTag("Inventory");
        setting = GameObject.FindWithTag("Setting");
        mainCamera = Camera.main.gameObject;
        camerafollow = GameObject.FindWithTag("CameraFollow");
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(inventory);
        DontDestroyOnLoad(setting);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(camerafollow);
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

    private void UpdateUIAfterSceneLoad()
    {
        // Update references to the canvas and camera if needed
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            if (canvas.worldCamera == null)
            {
                canvas.worldCamera = mainCamera.GetComponent<Camera>();
            }
        }

        // Ensure inventory and setting are reparented to the correct canvas
        Canvas mainCanvas = mainCamera.GetComponentInChildren<Canvas>();
        if (mainCanvas != null)
        {
            inventory.transform.SetParent(mainCanvas.transform, false);
            setting.transform.SetParent(mainCanvas.transform, false);
        }

        // Force update canvases to ensure UI elements are refreshed
        Canvas.ForceUpdateCanvases();

        // Re-enable EventSystem for the inventory and setting to ensure UI interaction
        EventSystem.current.SetSelectedGameObject(null);
    }
}
