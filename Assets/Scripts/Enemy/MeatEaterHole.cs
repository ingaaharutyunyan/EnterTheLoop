using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MeatEaterHole : MonoBehaviour
{
    [SerializeField] private GameObject meatEater, player, buttonPrompt;
    [SerializeField] private InputAction inputAction;
    [SerializeField] private PlayerSO playerSO; 
    private MeatEaterManager meatEaterManager;
    void Start()
    {
        meatEaterManager.OnBeastInvoked += AddBeast;
        buttonPrompt.SetActive(false);
        if (meatEaterManager.InvokedBeast())
        {
            AddBeast();
        }
    }

    void OnEnable()
    {
        meatEaterManager = GameManager.instance.GetMeatEaterManager();
        inputAction.performed += FeedHole;
    }

    void OnDisable()
    {
        inputAction.performed -= FeedHole;
        meatEaterManager.OnBeastInvoked -= AddBeast;
    }


    private void AddBeast()
    {
        GameObject beast = Instantiate(
            meatEater,
            new Vector3(player.transform.position.x + 30f, player.transform.position.y, player.transform.position.z),
            Quaternion.identity
        );

        Scene targetScene = SceneManager.GetSceneByBuildIndex(1);
        SceneManager.MoveGameObjectToScene(beast, targetScene);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buttonPrompt.SetActive(true);
            inputAction.Enable();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buttonPrompt.SetActive(false);
            inputAction.Disable();
        }
    }

    private void FeedHole(InputAction.CallbackContext context)
    {
        if (!playerSO.HasMeat()) return; // have a notification that says "you havent harvested any meat"
        GameManager.instance.GetMeatEaterManager().ResetTimer();
        buttonPrompt.SetActive(false);
    }
}
