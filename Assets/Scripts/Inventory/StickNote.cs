using UnityEngine;
using UnityEngine.InputSystem;

public class StickNote : MonoBehaviour
{
    [SerializeField] private int noteIndex;
    [SerializeField] private GameObject buttonPrompt;
    [SerializeField] private InputAction inputAction;
    [SerializeField] private ItemGUID itemGUID;
    private ItemsManager itemManager;

    void OnEnable()
    {
        int chance = Random.Range(0, 100);
        if (chance < 50)
        {
            this.gameObject.SetActive(false);
            return;
        }
        inputAction.performed += ShowNote;
        buttonPrompt.SetActive(false);
        itemManager = GameManager.instance.GetItemsManager();
    }

    void OnDisable()
    {
        inputAction.performed -= ShowNote;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inputAction.Enable();
            buttonPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inputAction.Disable();
            buttonPrompt.SetActive(false);
        }
    }

    public void ShowNote(InputAction.CallbackContext context)
    {
        itemManager.AddItem(itemGUID.GetGUID());
        GameManager.instance.GetSafeStickyNotes().ShowNotes(noteIndex);
        this.gameObject.SetActive(false);
    }
}
