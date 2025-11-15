using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using Persistence;

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
        itemsManager = new ItemsManager();
        safeCode = new SafeCode();
        mEaterManager = new MeatEaterManager(timerClass);
        ShowMenuScreen();
        safeStickyNotes.SetupCode(safeCode.GetCode());

    }

    public IEnumerator TimerRoutine()
    {
        while (!mEaterManager.InvokedBeast())
        {
            mEaterManager.SetTimer(Time.deltaTime);
            yield return null;
        }
    }

    void ShowMenuScreen()
    {
        pSO.SetMovementAllowed(false);
        mainMenu.ShowScreen(0);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    public IEnumerator StartGameRoutine()
    {   RemoveOtherScenes();
        StopCoroutine(TimerRoutine());
        pSO.ResetHallwayData();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(TimerRoutine());
        mainMenu.ShowScreen(100);
        pSO.SetXPosition(0f);
        pSO.InstantiateHallwayData();
        pSO.SetMovementAllowed(true);
        sm.AddSomeScene(1);
    }

    public void WinGame()
    {
        safeStickyNotes.HideAllNotes();
        mainMenu.ShowScreen(1);
        sm.UnloadScene(1);
    }

    public void LoseGame()
    {
        safeStickyNotes.HideAllNotes();
        mainMenu.ShowScreen(2);
        sm.UnloadScene(1);
    }

    public void TryGameAgain()
    {
        ResetGame?.Invoke();
        itemsManager.ResetItems();
        ShowMenuScreen();
    }

    public void SetPlayerMovement(bool b)
    {
        pSO.SetMovementAllowed(b);
    }

    private void RemoveOtherScenes()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex != 0)
            {
                sm.UnloadScene(scene.buildIndex);
            }
        }
    }

    public static GameManager instance { get; private set; }
    public ScenesManager GetScenesManager() { return sm; }
    [SerializeField] private PlayerSO pSO;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private ScenesManager sm;
    [SerializeField] private Timer timerClass;
    private ItemsManager itemsManager;
    public ItemsManager GetItemsManager() { return itemsManager; }
    public static event Action ResetGame;
    private SafeCode safeCode;
    public SafeCode GetSafeCode() { return safeCode; }
    [SerializeField] private SafeStickyNotes safeStickyNotes;
    public SafeStickyNotes GetSafeStickyNotes() { return safeStickyNotes; }
    private MeatEaterManager mEaterManager;
    public MeatEaterManager GetMeatEaterManager() { return mEaterManager; }
}
