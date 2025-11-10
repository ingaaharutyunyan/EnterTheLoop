using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStartTalk : MonoBehaviour
{
    [SerializeField] private NPCTextSpeakingOrder speakingOrder;
    private BoxCollider2D boxCollider;
    [SerializeField] private Notif notif;
 
    void OnEnable()
    {
        speakingOrder.OnConversationOver += UpdateProgress;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            speakingOrder.StartTalking();
            boxCollider.enabled = false;
        }
    }

    void OnDisable()
    {
        speakingOrder.OnConversationOver -= UpdateProgress;
    }

    private void UpdateProgress(int x) 
    {
        notif.StartAnimation();
    }
}
