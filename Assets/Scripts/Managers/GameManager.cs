using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

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
        while (true)
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
        StartCoroutine(TimerRoutine());
        mainMenu.ShowScreen(100);
        pSO.SetMovementAllowed(true);
        sm.AddSomeScene(1);
    }

    public void WinGame()
    {
        mainMenu.ShowScreen(1);
        sm.UnloadScene(1);
    }

    public void LoseGame()
    {
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
