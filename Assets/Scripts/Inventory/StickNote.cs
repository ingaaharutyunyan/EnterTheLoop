using UnityEngine;
using UnityEngine.InputSystem;

public class StickNote : MonoBehaviour
{
    [SerializeField] private int noteIndex;
    [SerializeField] private GameObject buttonPrompt;
    [SerializeField] private InputAction inputAction;

    void OnEnable()
    {
        inputAction.performed += ShowNote;
        buttonPrompt.SetActive(false);
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
        GameManager.instance.GetSafeStickyNotes().ShowNotes(noteIndex);
    }
}
