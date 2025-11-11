using UnityEngine;
using System;
using System.Runtime.Serialization;

public class Wizard : MonoBehaviour, iNPC
{
    [SerializeField] private Sprite wizardOpen, wizardClosed;
    [SerializeField] private PlayerSO playerSO;
    private NPCTextSpeakingOrder speakingOrder;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private ItemsManager itemManager;
    private ItemGUID itemGUID;
    void OnEnable()
    {
        itemGUID = GetComponent<ItemGUID>();
        itemManager = GameManager.instance.GetItemsManager();
        speakingOrder.OnConversationOver += FinishInteraction;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        spriteRenderer.sprite = wizardOpen;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        spriteRenderer.sprite = wizardClosed;
    }
    public void FinishInteraction(int x)
    {
        if (x == 0 && playerSO.HasAlmonds()) speakingOrder.ExecuteNextLine();
        else if (x == 0) speakingOrder.AllowRepeat();
        else
        {
            itemManager.AddItem(itemGUID.GetGUID());
            playerSO.EnableDuck();
        }    
    }
}