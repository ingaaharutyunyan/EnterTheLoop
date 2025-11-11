using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
        ShowMenuScreen();
    }

    void ShowMenuScreen()
    {
        pSO.SetMovementAllowed(false);
        mainMenu.ShowScreen(0);
    }

    public void StartGame()
    {
        mainMenu.ShowScreen(100);
        pSO.SetMovementAllowed(true);
    }

    public void WinGame()
    {
        mainMenu.ShowScreen(1);
        UnloadAllOtherScenes();
    }

    public void LoseGame()
    {
        mainMenu.ShowScreen(2);
        UnloadAllOtherScenes();
    }
    private void UnloadAllOtherScenes()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene loadedScene = SceneManager.GetSceneAt(i);

            if (loadedScene.isLoaded && loadedScene != currentScene)
            {
                SceneManager.UnloadSceneAsync(loadedScene);
            }
        }
    }

    public void TryGameAgain()
    {
        mainMenu.ShowScreen(0);
        StartGame();
    }

    public void SetPlayerMovement(bool b)
    {
        pSO.SetMovementAllowed(b);
    }

    public static GameManager instance { get; private set; }
    public ScenesManager GetScenesManager() { return sm; }
    [SerializeField] private PlayerSO pSO;
    [SerializeField] private MainMenu mainMenu;

    [SerializeField] private ScenesManager sm;
}
