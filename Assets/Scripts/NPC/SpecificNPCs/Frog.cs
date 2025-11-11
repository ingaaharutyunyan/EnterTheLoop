using UnityEngine;

public class Frog : MonoBehaviour, iNPC
{
    [SerializeField] private PlayerSO playerSO;
    private NPCTextSpeakingOrder speakingOrder;

    void OnEnable()
    {
        speakingOrder = GetComponent<NPCTextSpeakingOrder>();
    }

    void Start()
    {
        speakingOrder.OnConversationOver += FinishInteraction;
    }

    public void FinishInteraction(int x)
    {
        playerSO.SetAlmonds(true);
    }
}