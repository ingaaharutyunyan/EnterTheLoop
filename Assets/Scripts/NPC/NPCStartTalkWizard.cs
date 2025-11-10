using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStartTalkWizard : MonoBehaviour
{
    [SerializeField] private NPCTextSpeakingOrder speakingOrder;
    private BoxCollider2D boxCollider;
    [SerializeField] private Notif notif;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite wizardOpen;
    [SerializeField] private PlayerSO pSO;
 
    void OnEnable()
    {
        speakingOrder.OnConversationOver += UpdateProgress;
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            speakingOrder.StartTalking();
            boxCollider.enabled = false;
            spriteRenderer.sprite = wizardOpen;
        }
    }

    void OnDisable()
    {
        speakingOrder.OnConversationOver -= UpdateProgress;
    }

    private void UpdateProgress(int x) 
    {
        notif.StartAnimation();
        pSO.EnableDuck();
    }
}
