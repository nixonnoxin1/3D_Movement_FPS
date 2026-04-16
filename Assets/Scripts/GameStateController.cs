using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    // The Singleton Instance
    public static GameStateController Instance { get; private set; }

    private bool isPaused = false;
    public string mainMenuSceneName = "MainMenu";

    // The key name for our save file
    private const string SAVE_KEY = "SavedLevelName";

    void Awake()
    {
        // Ensure only one instance of this script exists
        if (Instance == null)
        {
            Instance = this;

            // This ensures the manager survives when we load a new scene!
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Pressing M opens the menu, but only if not already paused
        if (Input.GetKeyDown(KeyCode.M) && !isPaused)
        {
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.UnloadSceneAsync(mainMenuSceneName);
    }

    // --- NEW SAVE/LOAD METHODS ---

    public void SaveGame()
    {
        // 1. Get the name of the current active level
        string currentLevel = SceneManager.GetActiveScene().name;

        // 2. Save that string to PlayerPrefs
        PlayerPrefs.SetString(SAVE_KEY, currentLevel);
        PlayerPrefs.Save(); // Writes the data to the disk

        Debug.Log("Game Saved at: " + currentLevel);
    }

    public void LoadGame()
    {
        // 1. Check if a save exists
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string levelToLoad = PlayerPrefs.GetString(SAVE_KEY);

            // 2. Make sure the game is un-paused before loading!
            isPaused = false;
            Time.timeScale = 1f;

            // 3. Load the saved scene
            SceneManager.LoadScene(levelToLoad);
            Debug.Log("Loading Saved Game: " + levelToLoad);
        }
        else
        {
            Debug.LogWarning("No save data found!");
        }
    }
}