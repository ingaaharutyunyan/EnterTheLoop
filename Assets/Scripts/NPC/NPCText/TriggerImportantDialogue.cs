using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerImportantDialogue : MonoBehaviour // controls when a cutscene happens, or more talking
{
    [SerializeField] private NPCTextSpeakingOrder speakingOrders;
    [SerializeField] private GameObject playerButton;
    [SerializeField] private InputAction inputAction;
    private bool speakImmedietely;
    
    private void OnEnable()
    {
        if (playerButton == null) speakImmedietely = true;
        else speakImmedietely = false;

        speakingOrders.OnConversationOver += ConversationOver;
    }

    private void OnDisable()
    {
        speakingOrders.OnConversationOver -= ConversationOver;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (speakImmedietely)
        {
            StartTalking();
        }
        else
        {
            playerButton.SetActive(true);
            inputAction.Enable();
            inputAction.performed += StartTalking;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (!speakImmedietely) playerButton.SetActive(false);
        if (inputAction != null)
        {
            inputAction.performed -= StartTalking;
            inputAction.Disable();
        }
    }

    public void StartTalking(InputAction.CallbackContext context)
    {
        playerButton.SetActive(false);
        speakingOrders.StartTalking();
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        inputAction.performed -= StartTalking;
    }

    public void StartTalking()
    {
        speakingOrders.StartTalking();
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }

    public void ConversationOver(int x)
    {
    }
}