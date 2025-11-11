using System.Collections;
using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class NPCTextSpeakingOrder : MonoBehaviour
{
    [SerializeField] private InputAction inputAction;
    [SerializeField] private NPCTextAnim[] textAnim;
    [SerializeField] private GameObject[] textboxes;
    [SerializeField] private int[] speakingOrder;
    [SerializeField] private int[] stopTalk;
    [SerializeField] private PlayerInputHandler inputHandler;
    //private bool convoDone;
    //public bool GetConvoDone() { return convoDone; }
    private int index, stopTalkIndex = 0;
    // private float buttonHeldDown = 0f;
    //[SerializeField] private PlayerState pState;

    void OnEnable()
    {
        foreach (GameObject t in textboxes) t.SetActive(false);
    }

    public void DisableInput()
    {
        inputAction.Disable();
    }
    public void EnableInput()
    {
        inputAction.Enable();
    }

    public void AllowRepeat()
    {
        index = 0;
        stopTalkIndex = 0;
        foreach (NPCTextAnim t in textAnim) t.ResetIndex();
    }

    public void StartTalking()
    {
        inputAction.Enable();
        inputAction.performed += NextLineCheck;
        //inputAction.performed += StopTalkingCheck;
        ExecuteNextLine();
    }

    // private void StopTalkingCheck(InputAction.CallbackContext context)
    // {
    //     while (!context.canceled)
    //     {
    //         buttonHeldDown = Time.time;
    //         if (buttonHeldDown > 2.5f) 
    //         {
    //             StopTalking();
    //             index = stopTalkIndex;
    //             int linesLeft = 0;
    //             for (int i = 0; i < textAnim.Length; i++)
    //             {
    //                 for (int j = 0; j < speakingOrder.Length - speakingOrderIndex; j++)
    //                 {
    //                     if (i == speakingOrder[index + j]) linesLeft++;
    //                 }
    //                 textAnim[i].SkipLines(linesLeft);
    //             }
    //             buttonHeldDown = 0f;
    //             break;
    //         }
    //     }
    // }

    public void StopTalking()
    {
        OnConversationOver?.Invoke(stopTalkIndex);
        stopTalkIndex++;
        if (stopTalkIndex >= stopTalk.Length)
        {
            OnDisable();
        } 
        GameManager.instance.SetPlayerMovement(true);
        foreach (GameObject t in textboxes) t.SetActive(false);
        inputAction.Disable();
    }

    private void SetAllTextBoxesInactive()
    {
        foreach (GameObject tb in textboxes)
        {
            if (tb != textboxes[speakingOrder[index] - 1]) tb.SetActive(false); // if the current talking order doesnt correspond with the texboxes index, deactivate it
            else  tb.SetActive(true);
        }
    }

    public void NextLineCheck(InputAction.CallbackContext context)
    {
        bool ok = false;
        foreach (int x in stopTalk) 
        {
            if (index == x)
            {
                StopTalking();
                ok = false;
                break;
            }
            else ok = true;
        }
        if (ok) ExecuteNextLine();    
    }

    public void ExecuteNextLine()
    {
        // int i = 0;
        //     foreach (NPCTextAnim tAnim in textAnim)
        //     {
        //         if (!tAnim.GetFinished()) i++;
        //     }
            GameManager.instance.SetPlayerMovement(false);
            if (inputHandler != null) GameManager.instance.SetPlayerMovement(false);
            if (index == 0 || textAnim[speakingOrder[index - 1] - 1].GetFinished())
            {
                SetAllTextBoxesInactive();
                OnSpeakingOrderChanged?.Invoke(speakingOrder[index]); // for the NPC animation state
                textAnim[speakingOrder[index] - 1].NextLine(); // the appropriate person starts talking
                index++; // increase the speaking order index to the next person thats supposed to speak.
           } 
           else 
           {
                textAnim[speakingOrder[index] - 1].speed = 0.0001f;
           }

    }

    void OnDisable()
    {
        foreach (GameObject t in textboxes) 
        {
            if (t != null && t.activeSelf)
            {
                t.SetActive(false);
            }
        }
        inputAction.Disable();
        inputAction.performed -= NextLineCheck;
    }

    public event Action<int> OnSpeakingOrderChanged;
    public event Action<int> OnConversationOver;
}
