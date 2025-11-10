using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerUnimportantDialogue : MonoBehaviour
{
    [SerializeField] private NPCTextSpeakingOrder speakingOrders;
    [SerializeField] private GameObject playerButton;
    [SerializeField] private InputAction inputAction;
    [SerializeField] private BoxCollider2D coll;

    void OnEnable()
    {
        speakingOrders.OnConversationOver += ConversationOver;
    }

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    void OnDisable()
    {
        inputAction.Disable();
        inputAction.performed -= StartTalking;
    }
    private void ConversationOver(int x)
    {
        coll.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerButton.SetActive(true);
            inputAction.Enable();
            inputAction.performed += StartTalking;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            playerButton.SetActive(false);
            inputAction.Disable();
            inputAction.performed -= StartTalking;
        }
    }

    private void StartTalking(InputAction.CallbackContext context)
    {
        playerButton.SetActive(false);
        speakingOrders.StartTalking();
        inputAction.performed -= StartTalking;
    }

    private void StartTalking()
    {
        speakingOrders.StartTalking();
    }
}
