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
    /*     storyManager = new StoryManager(x,y);
        storyCount = storyManager.GetCounter();
        subStoryCount = storyManager.GetSubCounter();
        battleGUID = new BattleGUID(); */
        //ShowMenuScreen();
        StartGame();
    }

    void ShowMenuScreen()
    {
        pSO.SetMovementAllowed(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public void StartGame()
    {
        menuScreen.SetActive(false);
        pSO.SetMovementAllowed(true);
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
        UnloadAllOtherScenes();
    }

    public void LoseGame()
    {
        loseScreen.SetActive(true);
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
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        StartGame();
    }

    public void SceneFade()
    {

    }

    public void SetPlayerMovement(bool b)
    {
        pSO.SetMovementAllowed(b);
    }

    public static GameManager instance { get; private set; }
    public ScenesManager GetScenesManager() { return sm; }
    [SerializeField] private PlayerSO pSO;
    [SerializeField] private GameObject menuScreen, winScreen, loseScreen;

    [SerializeField] private ScenesManager sm;
}
